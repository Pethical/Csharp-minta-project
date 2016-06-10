using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Pethical.Framework
{

    [StructLayout(LayoutKind.Sequential)]
    struct Win32Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;


        public int Width
        {
            get { return Right - Left; }
        }

        public int Height
        {
            get { return Bottom - Top; }
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    struct Win32Point
    {
        public int X;
        public int Y;

        public Win32Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }


    class User32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Win32Rect rect);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Win32Rect rect);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Win32Point lpPoint);

        [DllImportAttribute("user32.dll")]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Win32Point pt);
    }
}
