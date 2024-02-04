using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public static class Draws
{
    public static Image shirt = Bitmap.FromFile("./img/Shirts/Shirt.png");
    public static Graphics Graphics { get; set; }
    private static Graphics g => Graphics;

    static SolidBrush yellowLight = new SolidBrush(Color.FromArgb(255, 211, 250, 214));
    static SolidBrush green = new SolidBrush(Color.FromArgb(255, 8, 113, 8));


    public static void DrawField(Image image, PictureBox pb)
        => g.DrawImage(image, new RectangleF(pb.Width*0.104f, pb.Height*0.009f, pb.Width*0.38f, pb.Height*0.921f));

    public static void Menu(PictureBox pb)
        => g.FillRectangle(green, pb.Width*0.625f, 0, pb.Width*0.375f, pb.Height);

    public static void MenuBorder(PictureBox pb)
    {
        g.FillRectangle(yellowLight, pb.Width*0.677f, pb.Height*0.037f, pb.Width*0.234f, pb.Height*0.74f);
        g.DrawRectangle(Pens.Black, pb.Width*0.677f, pb.Height*0.037f, pb.Width*0.234f, pb.Height*0.74f);
    }
    
    public static void DrawPlayerShirt(PointF location, PictureBox pb, Image shirt)
        => g.DrawImage(shirt, new RectangleF(location.X, location.Y , pb.Width*0.044f, pb.Height*0.081f));
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

