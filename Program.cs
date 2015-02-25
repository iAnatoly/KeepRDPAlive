using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeepRDPAlive
{
    static class Program
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;

        private static void ShakeMouse(IntPtr hwnd)
        {
            mouse_event(MOUSEEVENTF_MOVE, 1, 1, 0, 0);
            Thread.Sleep(1);
            mouse_event(MOUSEEVENTF_MOVE, -1, -1, 0, 0);
        }

        private static bool RefreshRDPWindow(string remoteHost)
        {
            var window = FindWindow("TscShellContainerClass", string.Format("{0} - Remote Desktop Connection", remoteHost));

            if (window.ToInt32() == 0) return false;

            var foreWindow = GetForegroundWindow();

            if (window != foreWindow) SetForegroundWindow(window);

            ShakeMouse(window);

            if (window != foreWindow) SetForegroundWindow(foreWindow);
            
            return true;
        }

        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: KeepRDPAlive <hostname> [<hostname> ...]");
                return -1;
            }
            while (true)
            {
                foreach (var hostname in args)
                {
                    Console.Write("{0}: Shaking {1}...", DateTime.Now, hostname);
                    
                    Console.WriteLine( RefreshRDPWindow(hostname) ? "Success." : "Windows not found." );

                    Thread.Sleep(1000);
                }
                Thread.Sleep(5*60*1000);
            }
        }
    }
}
