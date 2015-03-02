using System;
using System.Runtime.InteropServices;

namespace KeepRDPAlive
{
    class WindowHelper
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

/*        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
*/

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("USER32.DLL")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        public static IntPtr FindTerminalServicesWindowByHost(string remoteHost)
        {
            return WindowHelper.FindWindow("TscShellContainerClass", string.Format("{0} - Remote Desktop Connection", remoteHost));
        }
    }
}
