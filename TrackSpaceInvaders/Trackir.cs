/* 
 * Project : TrackSpaceInvaders
 * Authors : Fabian Troller / Guntram Juling / Raphaël Lopes
 * Description : Space invaders controlled with head tracking(TrackIR) technology
 * File : Trackir.cs
 * Date : 13.03.19
 */
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
        private const string DATA_PATH = @"http://127.0.0.1/data.txt";// link to the data of the server
        private static int _yaw = 0;
        private static int _pitch = 0;

        public static Timer ATimer { get; set; }
        public static int Yaw { get => _yaw; private set => _yaw = value; }
        public static int Pitch { get => _pitch; private set => _pitch = value; }
        public static string[] Lines { get; set; }

        /// <summary>
        /// Initializes the timer
        /// </summary>
        public static void Init()
        {
            // Create a timer with a ten ms interval.
            ATimer = new Timer(10);
            // Hook up the Elapsed event for the timer. 
            ATimer.Elapsed += ReadTrackIRData;
            ATimer.AutoReset = true;
            ATimer.Enabled = true;
        }

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
                    table = client.DownloadString(DATA_PATH);//store data from server into table
                    client.Dispose();
                }
                Lines = table.Split(',');// gets the data by spliting the result into Yaw and Pitch
                Int32.TryParse(Lines[0], out _yaw);
                Int32.TryParse(Lines[1], out _pitch);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
