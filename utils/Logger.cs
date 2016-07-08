using System;
using System.Collections.Generic;

namespace MeetingAgent.utils
{
    abstract class Logger
    {

        // from http://stackoverflow.com/questions/5057567/how-to-do-logging-in-c
        public static void log(String lines)
        {

            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log

            //System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\temp\\test.txt", true);
            System.IO.StreamWriter file = new System.IO.StreamWriter("log_file.txt", true);
            file.WriteLine(lines);

            file.Close();

        }
    }

    abstract class DataExport
    {
        public static void exportCSV(List<RecognisedObject> data)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("trackedObjects.csv", true);

            // header
            string csvLine = "ID,Frame,Type,X1, Y1, X2, Y2,";
            file.WriteLine(csvLine);


            // data
            foreach (RecognisedObject trackedObject in data)
            {
                csvLine = trackedObject.ID + "," + trackedObject.frame + "," + trackedObject.type + "," + trackedObject.rectangle.X.ToString() + "," + trackedObject.rectangle.Y.ToString() + "," + (trackedObject.rectangle.X + trackedObject.rectangle.Width).ToString() + "," + (trackedObject.rectangle.Y + trackedObject.rectangle.Height).ToString() + ",";
                file.WriteLine(csvLine);
            }

            file.Close();

        }
    }

}
