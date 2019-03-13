using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TrackSpaceInvaders
{
    public static class TrackIR
    {
        private const string FILE_PATH = @"http://127.0.0.1/test.txt";

        private static Timer _aTimer;// the timer interval
        private static int _x = 0;
        private static int _y = 0;
        private static string[] _lines;
        public static void Init()
        {
            // Create a timer with a ten ms interval.
            ATimer = new Timer(10);
            // Hook up the Elapsed event for the timer. 
            ATimer.Elapsed += ReadTrackIRData;
            ATimer.AutoReset = true;
            ATimer.Enabled = true;
        }

        public static Timer ATimer { get => _aTimer; set => _aTimer = value; }
        public static int X { get => _x; private set => _x = value; }
        public static int Y { get => _y; private set => _y = value; }
        public static string[] Lines { get => _lines; set => _lines = value; }

        /// <summary>
        /// Gets the data from the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ReadTrackIRData(object sender, ElapsedEventArgs e)
        {
            try
            {
                string table;
                using (WebClient client = new WebClient())
                {
                    table = client.DownloadString(FILE_PATH);
                }
                Lines = table.Split(',');// gets the data by spliting the result into x and y
                Int32.TryParse(Lines[0], out _x);
                Int32.TryParse(Lines[1], out _y);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
