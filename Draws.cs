using System;
using System.Drawing;
using System.Windows.Forms;
using Views;

namespace Extra;

public static class Draws
{
    public static Graphics Graphics { get; set; }
    private static Graphics g => Graphics;

    public static void DrawField(Image image)
        => g.DrawImage(image, new RectangleF(200, 10, image.Width, image.Height));

    public static void Menu()
        => g.FillRectangle(Brushes.Yellow, 1200, 0, 720, 1080);

    public static void MenuBorder()
        => g.DrawRectangle(Pens.Black, 1300, 40, 450, 800);
    
    public static void DrawPlayerShirt(Image image, RectangleF location)
        => g.DrawImage(image, new RectangleF(location.X, location.Y , image.Width, image.Height));

    public static void DrawText(string text, Color color, RectangleF location)
    {
        var format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
 
        var brush = new SolidBrush(color);
 
        g.DrawString(text, SystemFonts.MenuFont, brush, location, format);
    }

    /////////////////////////////////////////////////////////////////////////
    
}

