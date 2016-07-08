using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingAgent
{
    /// <summary>
    /// This is a recognised object in a single frame, which could be actually an object
    /// or just a false positive. Therefore, it is consider only as tentative and confirmed only
    /// until the object tracking process. Based on the history of frames (for a given window),
    /// if this object can be tracked back, then it is confirmed as a real object of interest,
    /// becoming an object of class 'TrackedObject'
    /// </summary>
    class RecognisedObject
    {
        private int _id;
        private string _type = "UNDEFINED";
        private Rectangle _rectangle;
        private double _frame;
        private int _centerX;
        private int _centerY;
        private Boolean _isNoise; 
        public static int nextID = 0;


        /*** START: VITERBI REQUIRED PROPERTIES ***/
        private int _x;
        private int _y;
        public int cost;
        public RecognisedObject closestSource;
        public bool taken = false;
        public int _ith;

        public RecognisedObject(int x, int y)
        {
            this._id = nextID++;
            _x = x;
            _y = y;
        }
        public RecognisedObject(int x, int y, int ith)
        {
            this._id = nextID++;
            _x = x;
            _y = y;
            _ith = ith;
        }
        /*
        public int x
        {
            get { return _x; }
        }

        public int y
        {
            get { return _y; }
        }
        */
        public int ith
        {
            get { return _ith; }
        }
        /*** END: VITERBI REQUIRED PROPERTIES ***/



        // this is a static list of recognised objects, just to export for analyais
        public static List<RecognisedObject> recognisedObjects = new List<RecognisedObject>();

        // constructor
        /*
        public RecognisedObject()
        {
            this._id = nextID++;
        }
        */

        public RecognisedObject(double frame, Rectangle rectangle, string type)
        {
            this._frame = frame;
            this._rectangle = rectangle;
            this._type = type;
            this._id = nextID++;
            this.updateCenter();
            addRecognisedObject(this);
        }

        private void updateCenter()
        {
            this._centerX = this._rectangle.X + (this._rectangle.Width / 2);
            this._centerY = this._rectangle.Y + (this._rectangle.Height / 2);
        }


        public int ID
        {
            get { return _id; }
        }

        public Rectangle rectangle
        {
            get { return _rectangle; }
            set
            {
                _rectangle = rectangle;
                this.updateCenter();
            }
        }

        public string type
        {
            get { return _type; }
            set { _type = type; }
        }

        public double frame
        {
            get { return _frame; }
            set { _frame = frame; }
        }

        public int centerX
        {
            get { return _centerX; }
        }
        
        public int x
        {
            get { return _centerX; }
        }
        
        public int centerY
        {
            get { return _centerY; }
        }
        
        public int y
        {
            get { return _centerY; }
        }
        
        public Boolean isNoise
        {
            get { return _isNoise; }
            set { _isNoise = isNoise; }
        }

        /// <summary>
        /// Creates a static list of recognised objects, just to export as csv for analysis
        /// </summary>
        /// <param name="recognisedObject">RecognisedObject to be added</param>
        /// <returns>Updated list</returns>
        public static List<RecognisedObject> addRecognisedObject(RecognisedObject recognisedObject)
        {
            recognisedObjects.Add(recognisedObject);
            return recognisedObjects;
        }
    }
}