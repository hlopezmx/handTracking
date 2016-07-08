using System;
using System.Windows.Forms;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using Emgu.CV.Util;
using System.IO;
using System.Collections;
using System.Drawing;
using MeetingAgent.utils;
using System.Collections.Generic;

namespace MeetingAgent
{
    public partial class frmMain : Form
    {
        // CONFIG PARAMETERS
        private static readonly Boolean FIND_FACE_FIRST = false;
        private static readonly Boolean TEXT_LABELS = false;
        private static readonly int FRAME_INTERVAL = 60; //29

        // VARIABLES
        private string strVideoFile;
        private Capture capture;
        private double iFrame = 0;
        private double nFrames = 0;

        Timer videoTimer = new Timer();
        ObjectRecogniser objRcgnsr = new ObjectRecogniser();

        public frmMain()
        {
            InitializeComponent();
        }

        // User select the video file
        private void btnLoadVideo_Click(object sender, EventArgs e)
        {
            txtMessages.AppendText("Loading video...\n");
            DialogResult drChosenFile;

            drChosenFile = ofdChooseVideo.ShowDialog();
            strVideoFile = ofdChooseVideo.FileName;

            if (drChosenFile != DialogResult.OK || strVideoFile == "")
            {
                txtMessages.AppendText("File not chosen\n");
                return;
            }

            txtMessages.AppendText("Selected file: " + strVideoFile + "\n");
            loadVideo();
        }

        // Starts the video
        private void loadVideo()
        {
            if (!File.Exists(strVideoFile))
            {
                txtMessages.AppendText("File doesn't exist: " + strVideoFile + "\n");
                return;
            }
            else
            {
                txtMessages.AppendText("File exist: " + strVideoFile + "\n");
            }

            try
            {
                capture = new Capture(strVideoFile);
                txtMessages.AppendText("opened file: " + strVideoFile + "\n");

                nFrames = capture.GetCaptureProperty(CapProp.FrameCount);
                tbVideoPosition.Maximum = (int)nFrames;

                videoTimer.Interval = 1; //29

                // First, we look for faces to get a good skin hsv range
                if (FIND_FACE_FIRST)
                {
                    videoTimer.Tick += new EventHandler(scanVideoForFaces);
                }
                else
                {
                    videoTimer.Tick += new EventHandler(RefreshVideoFrame);
                }

                videoTimer.Start();
            }
            catch (Exception ex)
            {
                txtMessages.AppendText("Error opening file: " + strVideoFile + "\n");
                return;
            }
        }

        private void restartVideo()
        {
            capture.SetCaptureProperty(CapProp.PosFrames, 1);
        }

        // look for faces to get a good skin hsv range
        private void scanVideoForFaces(object sender, EventArgs e)
        {
            Mat imgOriginal;
            Mat matProcessed = null;

            // we look for faces in the first second of video
            imgOriginal = capture.QueryFrame();

            Logger.log(objRcgnsr.findFace(imgOriginal, ref matProcessed));

            ibOriginal.Image = matProcessed;

            double iTime = capture.GetCaptureProperty(CapProp.PosMsec);
            iFrame = capture.GetCaptureProperty(CapProp.PosFrames);
            updateVideoPosition(iFrame, iTime);


            // after some frames, we continue to find skin
            if (iFrame >= 0) // 29
            {
                videoTimer.Stop();
                videoTimer = null;
                videoTimer = new Timer();
                videoTimer.Tick += new EventHandler(RefreshVideoFrame);
                videoTimer.Interval = FRAME_INTERVAL; // Frame Rate
                restartVideo();
                videoTimer.Start();
            }

        }

        // Play the video (updates frame on every videoTimer tick)
        private void RefreshVideoFrame(object sender, EventArgs e)
        {
            Mat imgOriginal;
            imgOriginal = capture.QueryFrame();

            // information about current position in video
            double iTime = capture.GetCaptureProperty(CapProp.PosMsec);
            iFrame = capture.GetCaptureProperty(CapProp.PosFrames);
            updateVideoPosition(iFrame, iTime);

            // FINDABLE OBJECTS and the parameters that helps finding them
            FindableObject HAND_DEFINITION = new FindableObject("HAND", new Hsv(0, 45, 37), new Hsv(10, 194, 218), 0.70, 0.05, 300, 1000, 4);
            FindableObject PAPER_DEFINITION = new FindableObject("PAPER", new Hsv(0, 0, 190), new Hsv(179, 50, 255), 0.70, 0, 300, 3000, 4);

            // find skin and get the set of images for each of the steps during the recognition process
            RecognisedInfo recognisedInfo = objRcgnsr.findObjects(imgOriginal, HAND_DEFINITION);
            Dictionary<string, Image<Gray, byte>> processingSkin = recognisedInfo.stepImages;

            // Split the processing step images from the recognition processrecognisedInfo 
            Image<Gray, byte> imgSkinArea = processingSkin["SkinArea"];
            Image<Gray, byte> imgSkinAreaMax = processingSkin["SkinAreaMax"];
            Image<Gray, byte> imgSkinBinary = processingSkin["SkinBinary"];
            Image<Gray, byte> imgSkinContours = new Image<Gray, byte>(imgSkinBinary.Width, imgSkinBinary.Height);
            Image<Bgr, byte> imgObjTrackingWindow = new Image<Bgr, byte>(imgSkinBinary.Width, imgSkinBinary.Height);

            // Put rectangles to reflect the identified objects
            int iObject = 0;
            foreach (Rectangle rect in recognisedInfo.rectangles)
            {
                CvInvoke.Rectangle(imgOriginal, rect, new Bgr(200, 100, 100).MCvScalar, 2, LineType.AntiAlias, 0);
                if (TEXT_LABELS) CvInvoke.PutText(imgOriginal, iObject.ToString(), new Point(rect.X, rect.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyDuplex, 1, new Rgb(255, 255, 0).MCvScalar);

                // create tracking object 
                RecognisedObject thisObject = new RecognisedObject(iFrame, rect, "HAND");

                // add the tracking object to the ObjectTracking module
                // it will handle the object across this and past frames
                ObjectTracking.addObject(thisObject);

                iObject++;
            }

            // Perform Object Tracking including the detected objects for this frame
            string matrix_output = "";
            List<List<RecognisedObject>> objectsByFrameList = ObjectTracking.track(imgOriginal.ToImage<Bgr, Byte>(), out imgObjTrackingWindow, out matrix_output, iFrame);


            if (objectsByFrameList.Count > 0)
            {
                string output = "";
                foreach (RecognisedObject trackedObj in objectsByFrameList[objectsByFrameList.Count - 1])
                {
                    RecognisedObject thisObject = trackedObj;
                    int iPoint = 0;
                    Point lastPoint = new Point();
                    while (thisObject != null)
                    {
                        if (iPoint > 0)
                        {
                            CvInvoke.Line(imgObjTrackingWindow, new Point(thisObject.x, thisObject.y), lastPoint, new Bgr(100, 100, 240).MCvScalar, 2, LineType.AntiAlias, 0);
                        }

                        output += "(" + thisObject.x + "," + thisObject.y + ") [" + thisObject.cost + "]";
                        if (thisObject.closestSource != null)
                        {
                            output += " -> ";
                        }
                        lastPoint = new Point(thisObject.x, thisObject.y);
                        thisObject = thisObject.closestSource;

                        iPoint++;
                    }
                    output += Environment.NewLine;
                }

                // display the matrix details
                Logger.log("==========================================================================");
                Logger.log("FRAME " + iFrame);
                Logger.log("=== DETAILS ==============================");
                Logger.log(matrix_output);

                Logger.log("Path string generated directly from the updated matrix with linked lists:");
                Logger.log("-------------------------------------------------");
                Logger.log(output);


            }


            // display frames
            ibProcessing1.Image = imgSkinArea;
            ibProcessing2.Image = imgSkinBinary;
            ibProcessing3.Image = imgSkinContours;

            // display object tracking
            ibProcessing4.Image = imgObjTrackingWindow;
            //ibProcessing4.Image = imgSkinContours;
            ibOriginal.Image = imgObjTrackingWindow;// imgOriginal;


            // if we reach the end of the video, we stop
            if (iFrame >= nFrames)
            {
                videoTimer.Stop();
                DataExport.exportCSV(RecognisedObject.recognisedObjects);
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            strVideoFile = "data/video1.avi";
            loadVideo();
        }

        private void tbVideoPosition_Scroll(object sender, EventArgs e)
        {

        }

        private void updateVideoPosition(double iFrame, double iTime)
        {
            lblTime.Text = "Time: " + TimeSpan.FromMilliseconds(iTime).ToString() + " Frame: " + iFrame;
            tbVideoPosition.Value = (int)iFrame;
        }


    }
}

