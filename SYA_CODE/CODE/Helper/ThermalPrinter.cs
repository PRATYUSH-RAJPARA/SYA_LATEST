using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

public class ThermalPrinter
{
    private List<string> repairData;

    public void PrintReceipt(List<string> data)
    {
        repairData = data;
        try
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = "Everycom-58-Series";
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error printing: " + ex.Message);
        }
    }

    private void PrintPage(object sender, PrintPageEventArgs e)
    {
        float width = 184;   // Print area width
        float height = 98;   // Print area height
        float x = 2;         // Left margin
        float y = 2;         // Top margin

        Pen blackPen = new Pen(Color.Black, 2);
       // e.Graphics.DrawRectangle(blackPen, x, y, width, height);

        // Set font
        Font font = new Font("Arial", 10, FontStyle.Regular);
        float textY = y ;
        float textX = x ;

        // Wrap text function
        void DrawWrappedText(Graphics g, string text, Font font, float x, ref float y, float maxWidth)
        {
            SizeF textSize = g.MeasureString(text, font);
            if (textSize.Width > maxWidth)
            {
                // If too wide, wrap within maxWidth
                RectangleF layoutRectangle = new RectangleF(x, y, maxWidth, textSize.Height * 2);
                g.DrawString(text, font, Brushes.Black, layoutRectangle);
                y += textSize.Height * 2; // Move cursor to next line
            }
            else
            {
                // Print normally
                g.DrawString(text, font, Brushes.Black, x, y);
                y += textSize.Height;
            }
        }

        // Print Name - Number (Wrap if too long)
        DrawWrappedText(e.Graphics, $"{repairData[0]}", font, textX, ref textY, width);
       e.Graphics.DrawLine(blackPen, x, textY , x + width, textY );
        textY += 5;

        e.Graphics.DrawString($"{repairData[1]} - {repairData[5]}", font, Brushes.Black, textX, textY); textY += 20;
        e.Graphics.DrawLine(blackPen, x, textY, x + width, textY);
        textY += 5;
        DrawWrappedText(e.Graphics, $"( {repairData[8]} ) - {repairData[11]}", font, textX, ref textY, width);
        DrawWrappedText(e.Graphics, $"RS : ₹ {repairData[3]}", font, textX, ref textY, width);
        DrawWrappedText(e.Graphics, $"Wt. : {repairData[2]}", font, textX, ref textY, width);

        e.Graphics.DrawLine(blackPen, x, textY, x + width, textY);

        // Print other fields
        //  e.Graphics.DrawString($"{repairData[8]} - {repairData[11]}", font, Brushes.Black, textX, textY); textY += 15;
        //  e.Graphics.DrawString($"{repairData[5]} - \u20B9"+$" {repairData[3]} - {repairData[2]}", font, Brushes.Black, textX, textY); textY += 15;
        textY += 5;
        DrawWrappedText(e.Graphics, $"Note: {repairData[13]}", font, textX, ref textY, width);
        e.Graphics.DrawLine(blackPen, x, textY, x + width, textY);

    }
}
