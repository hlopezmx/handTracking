using System;
using System.Collections.Generic;
using System.Drawing;

using Emgu.CV;                  //
using Emgu.CV.CvEnum;           // usual Emgu CV imports
using Emgu.CV.Structure;        //
using Emgu.CV.UI;               //
using Emgu.CV.Util;
using System.Collections;

namespace MeetingAgent
{
    /// <summary>
    /// This class includes the methods to recognise tentative objects 
    /// </summary>
    class ObjectRecogniser
    {        
        byte minH = 255, maxH = 0;
        byte minS = 255, maxS = 0;
        byte minV = 255, maxV = 0;
        

        /// <summary>
        /// Find faces in a given image and extract the HSV colour ranges to be used for 
        /// recognising hands. This process is executed at the very beginning of the agent.        
        /// </summary>
        /// <param name="imgOriginal">image where faces are looked for</param>
        /// <param name="imgFaces">image containing rectangles marking the faces area</param>
        /// <returns>Returns a string containing messages generated during the process</returns>
        public String findFace(Mat imgOriginal, ref Mat imgFaces)
        {
            String msg = "";
            imgFaces = imgOriginal.Clone();
            long detectionTime;
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();

            Image<Bgr, Byte> currentBgrFrame = imgOriginal.ToImage<Bgr, Byte>();
            Image<Hsv, Byte> currentHsvFrame = currentBgrFrame.Convert<Hsv, Byte>();
            int x, y;

            //The cuda cascade classifier doesn't seem to be able to load "haarcascade_frontalface_default.xml" file in this release
            //disabling CUDA module for now
            bool tryUseCuda = false;
            bool tryUseOpenCL = true;

            FaceDetection.Detect(
              imgOriginal, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",
              faces, eyes,
              tryUseCuda,
              tryUseOpenCL,
              out detectionTime);
            

            foreach (Rectangle face in faces)
            {
                //msg += face.ToString();
                //msg += "\n";
                CvInvoke.Rectangle(imgFaces, face, new Bgr(Color.Red).MCvScalar, 2);


                // we go through the pixels to get hsv limits
                // we use a reduced square, to avoid noise added by pixels surounding the face (clothes, background)
                for (x = face.X + face.Width / 4; x < face.X + face.Width * 3 / 4; x++)
                {
                    for (y = face.Y + face.Height / 4; y < face.Y + face.Height * 3 / 4; y++)
                    {
                        byte pxH = currentHsvFrame.Data[y, x, 0];
                        byte pxS = currentHsvFrame.Data[y, x, 1];
                        byte pxV = currentHsvFrame.Data[y, x, 2];
                        //msg += "[" + pxH + "," + pxS + "," + pxV +  "]";
                        //msg += "\n";

                        // our max threshold is heuristic
                        if ((pxH >= 0 && pxH <= 10) && (pxS >= 45 && pxS <= 255) && (pxV >= 0 && pxV <= 255))
                        {

                            if (minH > pxH) minH = pxH;
                            if (maxH < pxH) maxH = pxH;
                            if (minS > pxS) minS = pxS;
                            if (maxS < pxS) maxS = pxS;
                            if (minV > pxV) minV = pxV;
                            if (maxV < pxV) maxV = pxV;
                        }
                    }
                }

            }

            msg += "[" + minH + "-" + maxH + "," + minS + "-" + maxS + "," + minV + "-" + maxV + "]";

            return msg;

        }

        

        /// <summary>
        /// Find objects of a given defintion on an image
        /// </summary>
        /// <param name="matOriginal">the input image</param>
        /// <param name="objectDefinition">the FindableObject definition, used as instructions for detection</param>
        /// <returns>RecognisedInfo object, including the bounding boxes and the images for each step in the process</returns>
        public RecognisedInfo findObjects(Mat matOriginal, FindableObject objDefinition) {
            
            Image<Bgr, Byte> currentFrameCopy = matOriginal.ToImage<Bgr, Byte>();

            CvInvoke.GaussianBlur(currentFrameCopy, currentFrameCopy, new Size(3, 3), 0);

            Image<Hsv, Byte> currentHsvFrame = currentFrameCopy.Convert<Hsv, Byte>();
            Image<Gray, byte> imgSkin = new Image<Gray, byte>(currentFrameCopy.Width, currentFrameCopy.Height);
            Image<Gray, byte> imgSkinArea = new Image<Gray, byte>(currentFrameCopy.Width, currentFrameCopy.Height);
            Image<Gray, byte> imgSkinAreaMax = new Image<Gray, byte>(currentFrameCopy.Width, currentFrameCopy.Height);
            imgSkinArea = currentHsvFrame.InRange((Hsv)objDefinition.hsv_min, (Hsv)objDefinition.hsv_max);

            // clears region where hands are not usually present (top 70%, bottom 5%)
            imgSkinArea = clearArea(imgSkinArea, objDefinition.removePercentageTop, objDefinition.removePercentageBottom);
            Image<Gray, byte> mask = new Image<Gray, byte>(imgSkinArea.Width, imgSkinArea.Height);
            mask.SetValue(1);
            CvInvoke.cvCopy(imgSkinArea, imgSkinAreaMax, mask);

            // gaussian blur
            //CvInvoke.GaussianBlur(imgSkin, imgSkin, new Size(5, 5), 0);

            Mat structuringElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));

            // We erode the image a few times to remove noise and small elements
            for (int iErosion = 0; iErosion < objDefinition.erosionIterations; iErosion++)
            {
                CvInvoke.Erode(imgSkinAreaMax, imgSkinAreaMax, structuringElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
            }

            // Then we dilate the image back the to restore the orignal size of relevant objects
            for (int iErosion = 0; iErosion < objDefinition.erosionIterations; iErosion++)
            {
                CvInvoke.Dilate(imgSkinAreaMax, imgSkinAreaMax, structuringElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
            }

            // We now remove low intensity areas (we are in gray)
            imgSkin = imgSkinAreaMax.ThresholdBinary(new Gray(125), new Gray(255));
            

            // Find rectangles based on the contours
            ArrayList rectangles = ObjectRecogniser.FindRectangles(imgSkin, imgSkin, objDefinition);


            Dictionary<string, Image<Gray, byte>> processingStages = new Dictionary<string, Image<Gray, byte>>();
            processingStages.Add("SkinArea", imgSkinArea);
            processingStages.Add("SkinAreaMax", imgSkinAreaMax);
            processingStages.Add("SkinBinary", imgSkin);
            //processingStages.Add("SkinBoxes", currentFrameCopy);
            
            RecognisedInfo recognisedInfo = new RecognisedInfo();
            recognisedInfo.stepImages = processingStages;
            recognisedInfo.rectangles = rectangles;
            
            return recognisedInfo;
        }


        /// <summary>
        /// Generate bounding boxes for contours in an image
        /// based in http://www.emgu.com/forum/viewtopic.php?t=5201
        /// </summary>
        /// <param name="cannyEdges">The image containing the contours</param>
        /// <param name="imgResult">returns the image displaying the rectangles</param>
        /// <returns>an ArrayList of the rectangle objects</returns>
        public static ArrayList FindRectangles(IInputOutputArray cannyEdges, IInputOutputArray imgResult, FindableObject objDefinition)
        {
            VectorOfVectorOfPoint contours = null;
            ArrayList rectangles = new ArrayList();

            using (Mat hierachy = new Mat())
            using (contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(cannyEdges, contours, hierachy, RetrType.Tree, ChainApproxMethod.ChainApproxNone);
                for (int i = 0; i < contours.Size; i++)
                {
                    MCvScalar color = new MCvScalar(0, 0, 255);
                    double area = CvInvoke.ContourArea(contours[i], false);  //  Find the area of contour

                    //if (area > cMIN_HAND_AREA)
                    if ((area > objDefinition.minArea) && (area < objDefinition.maxArea))
                        {
                        CvInvoke.DrawContours(imgResult, contours, i, new MCvScalar(255, 0, 0));
                        Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
                        rectangles.Add(rect);
                        CvInvoke.Rectangle(imgResult, rect, new Gray(255).MCvScalar, 2, LineType.EightConnected, 0);
                        CvInvoke.PutText(imgResult, area.ToString(), new Point(rect.X, rect.Y - 30), Emgu.CV.CvEnum.FontFace.HersheyPlain, 2.5, new Gray(255).MCvScalar);

                        //RotatedRect rotRect = CvInvoke.MinAreaRect(contours[i]);
                        /*
                        Logger.log("---------------");
                        foreach (var item in rectangles)
                            Logger.log(item.ToString());
                            */
                    }
                }
            }
            return rectangles;
        }

        
        /// <summary>
        /// Clears upper and lower parts of an image, given the provided percentages 
        /// </summary>
        /// <param name="imgOrig">the original image</param>
        /// <param name="percentageUp">percentage of image to be removed from top</param>
        /// <param name="percentageDown">percentage of image to be removed from bottom</param>
        /// <returns>the resultin image after clearing top and bottom</returns>
        private Image<Gray, byte> clearArea(Image<Gray, byte> imgOrig, double percentageUp, double percentageDown)
        {
            percentageDown = 1 - percentageUp - percentageDown;
            percentageUp = 1 - percentageUp;

            if (percentageDown < 0) percentageDown = 0;
            if (percentageUp < 0) percentageUp = 0;

            // calculate the height of image we will keep based on the percentage of the image size
            int maskHeightUp = (int)(imgOrig.Height * percentageUp);
            int maskHeightDown = (int)(imgOrig.Height * percentageDown);

            // our resulting image will be stored here
            Image<Gray, byte> imgResult = new Image<Gray, byte>(imgOrig.Width, imgOrig.Height);

            // our mask, initialised as zeroes
            Image<Gray, byte> mask = new Image<Gray, byte>(imgOrig.Width, imgOrig.Height);

            // set the region of interest for the bottom area we want to keep, setting it to 1
            CvInvoke.cvSetImageROI(mask, new Rectangle(0, mask.Height - maskHeightUp, mask.Width, maskHeightDown));
            mask.SetValue(1);

            // reset the region of interest to use the whole image as the mask
            CvInvoke.cvResetImageROI(mask);

            // copy the image using the mask to dismiss the upper part
            CvInvoke.cvCopy(imgOrig, imgResult, mask);

            return imgResult;
        }
        
    }
}