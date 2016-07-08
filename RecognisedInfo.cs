using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections;
using System.Collections.Generic;

namespace MeetingAgent
{
    /// <summary>
    /// This class holds the structure of the data that is recognised in a frame
    /// for a given object type. 
    /// It includes:
    ///  -the detected information (rectangles a.k.a. bounding boxes)
    ///  -the dictionary of images denoting each step in the process, useful for debug
    /// </summary>
    class RecognisedInfo
    {
        public Dictionary<string, Image<Gray, byte>> stepImages;
        public ArrayList rectangles;
    }
}
