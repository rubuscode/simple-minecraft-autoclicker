using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace simple_minecraft_autoclicker
{
    internal class Program
    {
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        
        static int ms = 50;
        
        public static void Main(string[] args)
        {
            Console.Title = "autoclicker";
            Console.WriteLine("simple c# autoclicker for minecraft by rubus");
            Process mcproc = Process.GetProcessesByName("javaw")[0];
            if (mcproc == null) return;
            Console.Title = $"autoclicker attached to: {mcproc.MainWindowTitle}";
            Console.Write("ms: ");
            int.TryParse(Console.ReadLine(), out ms);
            while (true)
            {
                var hwnd = FindWindow(null, mcproc.MainWindowTitle);
                if ((GetAsyncKeyState(0x01) & 0x8000) != 0)
                    mouseClick(hwnd);
                Thread.Sleep(ms);
                
            }
        }

        private static void mouseClick(IntPtr hwnd)
        {
            PostMessage(hwnd, 0x201, 0, 0);
            Thread.Sleep(1);
            PostMessage(hwnd, 0x202, 0, 0);
        }
    }
}