using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Pethical.Framework.Utils
{
    public class Screen
    {
        /// <summary>
        /// Képernyőképet készít a teljes képernyőről, majd BitmapSource formában
        /// visszaadja azt
        /// </summary>
        /// <param name="addToClipboard">Nem használt, jövőbeni funkciónak fenntartott</param>
        /// <returns>BitmapSource amelyből a kép kinyerhető</returns>
        public static BitmapSource CaptureFullScreen(bool addToClipboard)
        {
            return CaptureRegion(
                User32.GetDesktopWindow(),
                (int)SystemParameters.VirtualScreenLeft,
                (int)SystemParameters.VirtualScreenTop,
                (int)SystemParameters.VirtualScreenWidth,
                (int)SystemParameters.VirtualScreenHeight,
                addToClipboard);
        }

        /// <summary>
        /// A képernyő egy adott régiójáról készít képernyőképet.
        /// </summary>
        /// <param name="hWnd">Az ablak Handle</param>
        /// <param name="x">x koordináta</param>
        /// <param name="y">y koordináta</param>
        /// <param name="width">A kép szélessége (A terület szélessége)</param>
        /// <param name="height">A kép/terület magassága</param>
        /// <param name="addToClipboard">Nem használt, jövőbeni funkciónak fenntartott</param>
        /// <returns>BitmapSource amelyből a kép kinyerhető</returns>
        /// <example>
        /// <code lang='cs'>
        ///   bitmapsource = CaptureRegion(((HwndSource)HwndSource.FromVisual(Application.Current.MainWindow)).Handle,
        ///   (int)Application.Current.MainWindow.Left, (int)Application.Current.MainWindow.Top,
        ///   (int)Application.Current.MainWindow.Width, (int)Application.Current.MainWindow.Height, false);
        /// </code>
        /// </example>
        public static BitmapSource CaptureRegion(
            IntPtr hWnd, int x, int y, int width, int height, bool addToClipboard)
        {
            IntPtr sourceDC = IntPtr.Zero;
            IntPtr targetDC = IntPtr.Zero;
            IntPtr compatibleBitmapHandle = IntPtr.Zero;
            BitmapSource bitmap = null;

            try
            {
                sourceDC = User32.GetDC(User32.GetDesktopWindow());
                targetDC = Gdi32.CreateCompatibleDC(sourceDC);
                compatibleBitmapHandle = Gdi32.CreateCompatibleBitmap(sourceDC, width, height);
                Gdi32.SelectObject(targetDC, compatibleBitmapHandle);
                Gdi32.BitBlt(targetDC, 0, 0, width, height, sourceDC, x, y, Gdi32.SRCCOPY);
                bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    compatibleBitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception ex)
            {
                throw new ScreenCaptureException(string.Format("Error capturing region {0},{1},{2},{3}", x, y, width, height), ex);
            }
            finally
            {
                Gdi32.DeleteObject(compatibleBitmapHandle);
                User32.ReleaseDC(IntPtr.Zero, sourceDC);
                User32.ReleaseDC(IntPtr.Zero, targetDC);
            }

            return bitmap;
        }           

    }
}
