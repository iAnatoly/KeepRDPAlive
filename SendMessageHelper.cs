using System;
using System.Runtime.InteropServices;

namespace KeepRDPAlive
{
    class SendMessageHelper
    {
        private const UInt32 WM_SYSCOMMAND = 0x0112;
        private const UInt32 SC_MAXIMIZE = 0xF030;
        private const UInt32 SC_MINIMIZE = 0xF020;
        private const UInt32 SC_RESTORE = 0xF120;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public static void MaximizeWindow(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, (IntPtr)0);
        }
        public static void RestoreWindow(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, (IntPtr)0);
        }

        public static void MinimizeWindow(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_MINIMIZE, (IntPtr)0);
        }
    }
}
