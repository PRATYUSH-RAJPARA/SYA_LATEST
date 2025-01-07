using QRCoder;
using System.Drawing;
using System.Drawing.Printing;
namespace SYA
{
    public static class TagPrintHelper
    {
        static Font font = new Font("Arial Black", 8, FontStyle.Bold);
        static SolidBrush brush = new SolidBrush(Color.Black);
        static float a = -4;
        public static void onlyGross(string value, PrintPageEventArgs e)
        {
        //    e.Graphics.DrawRectangle(Pens.Red, 4,-2, (float)200,45);
            e.Graphics.DrawString("G: " + value, new Font("Arial", 12, FontStyle.Bold), brush, new RectangleF(4, 4+a, 75, 45), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void gross(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("G: " + value, new Font("Arial", (float)9.5, FontStyle.Bold), brush, new RectangleF(4, 4 + a, 75, (float)22.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void net(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("N: " + value, new Font("Arial", (float)9.5, FontStyle.Bold), brush, new RectangleF(4, (float)26.5+a, 75, (float)22.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void image(PrintPageEventArgs e)
        {//hello
            Pen pen = new Pen(Color.Black); // Define a pen for the rectangle
                                            //  e.Graphics.DrawRectangle(pen, 83, 4, (float)22.5, (float)22.5);
                                            //   e.Graphics.DrawLine(pen, 83, (float)28.5, (float)105.5, (float)28.5);
            Image logoImage = Image.FromFile(helper.ImageFolder + "\\logo.jpg"); e.Graphics.DrawImage(logoImage, new RectangleF(83, 4 + a, (float)22.5, (float)22.5));
        }
        public static void sizeBelowLogo(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 6, FontStyle.Bold), brush, new RectangleF(79, 28 + a, (float)30.5, (float)11.25), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void yamuna(PrintPageEventArgs e)
        {
            e.Graphics.DrawString("YAMUNA", new Font("Arial", (float)4.5, FontStyle.Bold), brush, new RectangleF(79, (float)26.5 + a, (float)30.5, (float)11.25), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void quality(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 6, FontStyle.Bold), brush, new RectangleF(79, (float)37.75 + a, (float)30.5, (float)11.25), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void QR(string value, PrintPageEventArgs e)
        {
            RectangleF qrCodeRect = new RectangleF(178, 0 + a, 34, 34);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeBitmap = qrCode.GetGraphic((int)qrCodeRect.Width, Color.Black, Color.White, true);
                e.Graphics.DrawImage(qrCodeBitmap, qrCodeRect);
            }
        }
        public static void labour(string value, PrintPageEventArgs e)
        {
            //pratyushchange
            e.Graphics.DrawString("L: " + value, new Font("Arial", 7, FontStyle.Bold), brush, new RectangleF((float)113.5, 4 + a, (float)56.5, 11), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void wholeLabour(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString("TL: " + value, new Font("Arial", 7, FontStyle.Bold), brush, new RectangleF((float)113.5, 4 + a, (float)56.5, 11), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void other(string value, PrintPageEventArgs e)
        { e.Graphics.DrawString("O: " + value, new Font("Arial", 7, FontStyle.Bold), brush, new RectangleF((float)113.5, 16 + a, (float)56.5, 11), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }); }
        public static void tagNumberFirstPart(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 6, FontStyle.Bold), brush, new RectangleF(164, 31 + a, 47, 7), new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
        }
        public static void tagNumberSecondPart(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", (float)6.25
                               , FontStyle.Bold), brush, new RectangleF(164, 41 + a, 47, 7), new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
        }
        public static void tagNumberSingle(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", (float)6.25, FontStyle.Bold), brush, new RectangleF(164, 33 + a, 47, 13), new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });
        }
        public static void belowLabour1(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 7, FontStyle.Bold), brush, new RectangleF((float)116.5, 29 + a, (float)56.5, 10), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
        }
        public static void belowLabour2(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 7, FontStyle.Bold), brush, new RectangleF((float)116.5, 38 + a, (float)56.5, 12), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
        }
        public static void silverMainUpper(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 12, FontStyle.Bold), brush, new RectangleF(4, 4 + a, 75, (float)22.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void silverSecondUpper(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 10, FontStyle.Bold), brush, new RectangleF((float)113.5, 4 + a, (float)56.5, 27), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void textBelowSilverMainUpper(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 10, FontStyle.Bold), brush, new RectangleF(4, (float)26.5 + a, 75, (float)22.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void textBelowSilverSecondUpper(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 10, FontStyle.Bold), brush, new RectangleF((float)113.5, 31 + a, (float)56.5, 20), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void FramesName(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", (float)17.5, FontStyle.Bold), brush, new RectangleF(8, 4+a, 185, (float)45), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
        public static void FramesPrice(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", 15, FontStyle.Bold), brush, new RectangleF(8, 26+a, 165, (float)22.5), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
        }
        public static void FrametagNumberSecondPart(string value, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(value, new Font("Arial", (float)7.25
                               , FontStyle.Bold), brush, new RectangleF(170, 38+a, 41, 9), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }
    }
}
