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
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_X();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_Y();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_Z();

        [DllImport("TrackIR.dll")]
        public static extern int trackIR_NPStatus();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_Init();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_Update();
        [DllImport("TrackIR.dll")]
        public static extern int trackIR_End();
        [DllImport("NPClient.dll")]// can't access
        public static extern int NP_ReCenter();
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
        public static int NPStatus
        {
            get
            {
                return trackIR_NPStatus();
            }
        }
        public static int Init()
        {

            //return trackIR_Init();
            return ReCenter;
            //return 0;
        }
        public static int Update
        {
            get
            {
                return trackIR_Update();
            }
        }
        public static int ReCenter
        {
            get
            {
                return NP_ReCenter();
            }
        }
    }
}
