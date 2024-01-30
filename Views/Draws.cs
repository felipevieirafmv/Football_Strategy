using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public static class Draws
{
    public static Image shirt = Bitmap.FromFile("./img/Shirt.png");
    public static Graphics Graphics { get; set; }
    private static Graphics g => Graphics;

    static SolidBrush yellowLight = new SolidBrush(Color.FromArgb(255, 211, 250, 214));
    static SolidBrush green = new SolidBrush(Color.FromArgb(255, 8, 113, 8));


    public static void DrawField(Image image)
        => g.DrawImage(image, new RectangleF(200, 10, image.Width, image.Height));

    public static void Menu()
        => g.FillRectangle(green, 1200, 0, 720, 1080);

    public static void MenuBorder()
    {
        g.FillRectangle(yellowLight, 1300, 40, 450, 800);
        g.DrawRectangle(Pens.Black, 1300, 40, 450, 800);
    }
    
    public static void DrawPlayerShirt(PointF location)
        => g.DrawImage(shirt, new RectangleF(location.X, location.Y , shirt.Width, shirt.Height));
    public static void DrawPlayerMenu(PointF location)
        => g.FillRectangle(Brushes.DarkBlue, new RectangleF(location.X, location.Y , 450, 40));

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

