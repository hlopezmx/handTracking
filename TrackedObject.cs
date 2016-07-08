using System.Collections.Generic;
using System.Drawing;

namespace MeetingAgent
{
    /// <summary>
    /// This class defines the objects that have been verified as True Positive from the
    /// object recognition process. The verification has taken place during the Object Tracking
    /// process. 
    /// </summary>
    class TrackedObject
    {
        private int _id;
        private string _type = "UNDEFINED";
        private Dictionary<double,Rectangle> _rectangleByFrame;
        public static int nextID = 0;

        /*** START: VITERBI REQUIRED PROPERTIES ***/
        private int _x;
        private int _y;
        public int cost;
        public TrackedObject closestSource;
        public bool taken = false;
        public int _ith;

        public TrackedObject(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public TrackedObject(int x, int y, int ith)
        {
            _x = x;
            _y = y;
            _ith = ith;
        }

        public int x
        {
            get { return _x; }
        }

        public int y
        {
            get { return _y; }
        }
        public int ith
        {
            get { return _ith; }
        }
        /*** END: VITERBI REQUIRED PROPERTIES ***/

        // this is a static list of tracked objects, just to export for analyais
        public static List<TrackedObject> trackedObjects = new List<TrackedObject>();

        public TrackedObject()
        {
            this._id = nextID++;
        }

        public TrackedObject(double frame, Rectangle rectangle, string type)
        {
            this._rectangleByFrame.Add(frame,rectangle);
            this._type = type;
            this._id = nextID++;
            addTrackedObject(this);
        }

        /// <summary>
        /// Creates a static list of tracked objects, just to export as csv for analysis
        /// </summary>
        /// <param name="trackedObject">TrackedObject to be added</param>
        /// <returns>Updated list</returns>
        public static List<TrackedObject> addTrackedObject(TrackedObject trackedObject)
        {
            trackedObjects.Add(trackedObject);
            return trackedObjects;
        }

    }
}
