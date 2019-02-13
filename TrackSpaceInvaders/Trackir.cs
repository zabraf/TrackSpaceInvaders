using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TrackSpaceInvaders
{
    public static class TrackIR
    {
        private static string _path = @"C:\Users\Administrateur\Desktop\test.txt";
        private static Timer _aTimer;
        private static int x = 0;
        private static int y = 0;
        public static void Init()
        {
            // Create a timer with a two second interval.
            _aTimer = new Timer(50);
            // Hook up the Elapsed event for the timer. 
            _aTimer.Elapsed += ReadTrackIRData;
            _aTimer.AutoReset = true;
            _aTimer.Enabled = true;
        }

        static string[] lines;

        public static int X { get => x; }
        public static int Y { get => y; }

        private static void ReadTrackIRData(object sender, ElapsedEventArgs e)
        {
            try
            {
                string table;
                using (StreamReader sr = new StreamReader(_path))
                {
                   table  = sr.ReadToEnd();
                    sr.Close();
                }
                lines = table.Split(',');
                Int32.TryParse(lines[0],out x);
                Int32.TryParse(lines[1],out y);
            }
            catch
            {

            }
            
        }
    }
}
