using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingAgent
{
    /// <summary>
    /// This class defines the structure for a findable object:
    ///  -ROI in the image (removing top and bottom %)
    ///  -HSV ranges
    ///  -area ranges
    ///  -number of erosion and dilate iterations
    /// </summary>
    class FindableObject
    {
        public string type = "UNDEFINED";
        public Hsv hsv_min = new Hsv(0, 0, 0);
        public Hsv hsv_max = new Hsv(255, 255, 255);
        public double removePercentageTop;
        public double removePercentageBottom;
        public int minArea;
        public int maxArea;
        public int erosionIterations;

        public FindableObject(string type, Hsv hsv_min, Hsv hsv_max, double removePercentageTop, double removePercentageBottom,int minArea, int maxArea, int erosionIterations) {
            this.type = type;
            this.hsv_min = hsv_min;
            this.hsv_max = hsv_max;
            this.removePercentageTop = removePercentageTop;
            this.removePercentageBottom = removePercentageBottom;
            this.minArea = minArea;
            this.maxArea = maxArea;
            this.erosionIterations = erosionIterations;
        }

    }
}
