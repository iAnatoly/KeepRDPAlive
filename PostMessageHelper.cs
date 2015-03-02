using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeepRDPAlive
{
    class PostMessageHelper
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_CHAR                       = 0x0102;
        private const int WM_KEYUP                      = 0x0101;
        private const int WM_KEYDOWN                    = 0x0100;
        
        private static uint GetLParam(uint scanCode, uint repeatCount = 1, uint extended = 0, uint context = 0,
            uint previousState = 0, uint transition = 0)
        {
            return repeatCount
                    | (scanCode << 16)
                    | (extended << 24)
                    | (context << 29)
                    | (previousState << 30)
                    | (transition << 31);
        }

        [Obsolete("does not work")]
        public static void sendkey(IntPtr hWnd)
        {
            bool retVal = PostMessage(hWnd, WM_KEYDOWN, (IntPtr)'A', (IntPtr)GetLParam((uint)ScanCodeShort.KEY_A, previousState:0, transition:0));
            if (!retVal) throw new Exception("PostMessage");

            System.Threading.Thread.Sleep(100);

            retVal = PostMessage(hWnd, WM_KEYUP, (IntPtr)'A', (IntPtr)GetLParam((uint)ScanCodeShort.KEY_A, previousState: 1, transition: 1));
            if (!retVal) throw new Exception("PostMessage");

            retVal = PostMessage(hWnd, WM_CHAR, (IntPtr)'A', (IntPtr)GetLParam((uint)ScanCodeShort.KEY_A, previousState: 0, transition: 0));
            if (!retVal) throw new Exception("PostMessage");

            System.Threading.Thread.Sleep(100);

            retVal = PostMessage(hWnd, WM_CHAR, (IntPtr)'A', (IntPtr)GetLParam((uint)ScanCodeShort.KEY_A, previousState: 1, transition: 1));
            if (!retVal) throw new Exception("PostMessage");

       }
    }
}
