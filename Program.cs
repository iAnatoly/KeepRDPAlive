using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeepRDPAlive
{
    static class Program
    {


        private static bool RefreshRDPWindow(string remoteHost)
        {
            var window = WindowHelper.FindTerminalServicesWindowByHost(remoteHost);
            if (window == IntPtr.Zero) return false;

            bool isMinimized = WindowHelper.IsIconic(window);

            

            var fwwindow = WindowHelper.GetForegroundWindow();

            if (fwwindow != window) WindowHelper.SetForegroundWindow(window);
            if (isMinimized) SendMessageHelper.RestoreWindow(window);

            KeyboardHelper.SendNoise();

            if (isMinimized) SendMessageHelper.MinimizeWindow(window);

            if (fwwindow != window) WindowHelper.SetForegroundWindow(fwwindow);

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
