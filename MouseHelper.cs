using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeepRDPAlive
{
    class MouseHelper
    {
        [DllImport("USER32.DLL")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;

        public static void ShakeMouse(IntPtr hwnd)
        {
            mouse_event(MOUSEEVENTF_MOVE, 1, 1, 0, 0);
            Thread.Sleep(1);
            mouse_event(MOUSEEVENTF_MOVE, -1, -1, 0, 0);
        }
    }
}
