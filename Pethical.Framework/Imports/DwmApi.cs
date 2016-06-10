using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Pethical.Framework
{
    class DwmApi
    {
        [DllImport("DwmApi.dll")]
        public static extern int DwmGetWindowAttribute(
            IntPtr hwnd,
            uint dwAttributeToGet,
            IntPtr pvAttributeValue,
            uint cbAttribute);

        public const int DWMNCRP_USEWINDOWSTYLE = 0;
        public const int DWMNCRP_DISABLED = 1;
        public const int DWMNCRP_ENABLED = 2;

        public const int DWMWA_NCRENDERING_ENABLED = 1;
        public const int DWMWA_NCRENDERING_POLICY = 2;
        public const int DWMWA_TRANSITIONS_FORCEDISABLED = 3;

    }
}
