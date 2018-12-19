using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackSpaceInvaders
{
    public static class TrackIR
    {
        public static int X
        {
            get
            {
                return trackIR_X();
            }
        }
        public static int Y
        {
            get
            {
                return trackIR_Y();
            }
        }
        public static int Z
        {
            get
            {
                return trackIR_Z();
            }
        }

        [DllImport("TrackIR.dll")]
        public static extern int trackIR_X();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_Y();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_Z();
    }
}
