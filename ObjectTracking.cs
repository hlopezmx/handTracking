using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MeetingAgent.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MeetingAgent
{

    /// <summary>
    /// This abstract class will keep a record of the objects ('objectsByFrame') during 'WINDOW_FRAMES' Frames
    /// It accepts new objects by frame. 
    /// When it detects more than 'WINDOW_FRAMES', it dismisses the oldest to keep the size of the window.
    /// It recalculates the object tracking in the 'WINDOW_FRAMES' frames and assigns the tracking
    /// to the currentFrame (based on the history of previous 'WINDOW_FRAMES' frames.
    /// </summary>
    abstract class ObjectTracking
    {
        static int WINDOW_FRAMES = 10;
        static double currentFrame = -1;
        static Dictionary<int, List<RecognisedObject>> objectsByFrame = new Dictionary<int, List<RecognisedObject>>();
        static Queue<List<RecognisedObject>> objectsByFrameQueue = new Queue<List<RecognisedObject>>(WINDOW_FRAMES);
        static List<RecognisedObject> currentFrameObjects = new List<RecognisedObject>();

        /// <summary>
        /// Analyses the frame window's object to keep track of objects
        /// </summary>
        public static List<List<RecognisedObject>> track(Image<Bgr, byte> imgInput, out Image<Bgr, byte> imgObjTrackingWindow, out string matrix_output, double iFrame)
        {
            matrix_output = "";
            imgObjTrackingWindow = new Image<Bgr, byte>(imgInput.Width, imgInput.Height);
            Image<Gray, byte> mask = new Image<Gray, byte>(imgInput.Width, imgInput.Height);
            mask.SetValue(1);
            CvInvoke.cvCopy(imgInput, imgObjTrackingWindow, mask);

            // creates a dictionary of objects by frame
            List<List<RecognisedObject>> objectsByFrameList = new List<List<RecognisedObject>>(WINDOW_FRAMES);

            // if we have a complete window queue, we analize the objects to track them
            // otherwise, we just wait for it to fill in
            if (objectsByFrameQueue.Count == WINDOW_FRAMES)
            {
                objectsByFrameList = objectsByFrameQueue.ToList();

                // we will assign the tracked objects to the current frame
                int nCurrentFrame = Convert.ToInt32(getLastFrameInWindow(objectsByFrameList));

                // ANALYSIS:
                // viterbi analysis

                if (iFrame == 27) {
                    Boolean debugHere = true;
                }

                ViterbiTracking viterbi = new ViterbiTracking();
                string paths = viterbi.process(objectsByFrameList);
                matrix_output = viterbi.displayMatrix(objectsByFrameList);


                /*
                // how many objects do we have in average by frame?
                int nObjects = averageObjectsByFrame(objectsByFrameList);
                Logger.log("Frame " + nCurrentFrame + "|Avg Objects: " + nObjects);


                for (int iFrame = 0; iFrame < objectsByFrameList.Count; iFrame++)
                {
                    for (int iObject = 0; iObject < objectsByFrameList[iFrame].Count; iObject++)
                    {
                        RecognisedObject thisObj = objectsByFrameList[iFrame][iObject];
                        Rectangle rect = thisObj.rectangle;

                        
                        Rectangle smallRect = new Rectangle(new Point(thisObj.centerX, thisObj.centerY), new Size(2, 2));
                        CircleF circle = new CircleF(new Point(thisObj.centerX, thisObj.centerY), 2);

                        // Displays a filled circle over the object
                        CvInvoke.Circle(imgObjTrackingWindow, new Point(thisObj.centerX, thisObj.centerY), 10, new Bgr(15 * (iFrame + 1), 15 * (iFrame + 1), 200).MCvScalar, -1);
                        

                        
                    }
                }
                */

                // update trackedObjects
                // (pending)
            }


            return objectsByFrameList;
        }

        /// <summary>
        /// how many objects do we have in average by frame?
        /// </summary>
        /// <param name="objectsByFrameList">The list of objects by frame</param>
        /// <returns>number of objects in average</returns>
        private static int averageObjectsByFrame(List<List<RecognisedObject>> objectsByFrameList)
        {
            List<int> nObjectsByFrame = new List<int>();
            foreach (List<RecognisedObject> thisFrameObjects in objectsByFrameList)
            {
                nObjectsByFrame.Add(thisFrameObjects.Count);
            }

            return Convert.ToInt32(Math.Floor(nObjectsByFrame.Average()));
        }

        /// <summary>
        /// Gets the newest frame in the window, which will be considered as the actual current frame
        /// in the process
        /// </summary>
        /// <param name="objectsByFrameList"></param>
        /// <returns></returns>
        private static double getLastFrameInWindow(List<List<RecognisedObject>> objectsByFrameList)
        {
            return objectsByFrameList.Last().Last().frame;
        }

        /// <summary>
        /// add a new object to the frames window for tracking
        /// </summary>
        /// <param name="newObject">
        /// the RecognisedObject object to be added to the frame's list
        /// </param>
        public static void addObject(RecognisedObject newObject)
        {
            double thisFrame = newObject.frame;

            // if we get an object from a new frame (the frame comes within the newObject)
            if (currentFrame != thisFrame)
            {

                // add the frame's objects to the queue
                if (currentFrame >= 0)
                {
                    objectsByFrameQueue.Enqueue(currentFrameObjects);

                    // if we have passed the queue size, we dismiss oldest element
                    if (objectsByFrameQueue.Count > WINDOW_FRAMES)
                    {
                        objectsByFrameQueue.Dequeue();
                    }
                }
                currentFrame = thisFrame;

                // we create a new list of objects for this frame
                currentFrameObjects = new List<RecognisedObject>();

                // add the object to the new list
                currentFrameObjects.Add(newObject);
            }
            // if we get an object from the current frame
            else
            {
                // add the object to the current list
                currentFrameObjects.Add(newObject);
            }

            
        }

    }
}
