using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
namespace SYA
{
    public class RawPrinterHelper
    {
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter")]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, IntPtr di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter")]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter")]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter")]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter")]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        public static bool SendStringToPrinter(string printerName, string data)
        {
            IntPtr hPrinter = IntPtr.Zero;
            if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero)) return false;

            IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(data);
            int dwCount = data.Length;
            int dwWritten;

            bool success = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
            Marshal.FreeCoTaskMem(pBytes);
            ClosePrinter(hPrinter);

            return success;
        }
    }
}