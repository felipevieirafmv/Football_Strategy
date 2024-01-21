using System;
using System.Drawing;
using System.Windows.Forms;
using Views;

namespace Extra;

public class Draws
{
    public Draws() { }

    public void DrawField(Graphics g, Image image)
        => g.DrawImage(image, new RectangleF(200, 10, image.Width, image.Height));

    public void Menu(Graphics g)
        => g.FillRectangle(Brushes.DarkBlue, 1200, 0, 720, 1080);

    public void MenuBorder(Graphics g)
        => g.DrawRectangle(Pens.White, 1300, 40, 450, 800);
    
    public void DrawPlayerShirt(Graphics g, Image image, RectangleF location)
        => g.DrawImage(image, new RectangleF(location.X, location.Y , image.Width, image.Height));

    public void DrawText(Graphics g, string text, Color color, RectangleF location)
    {
        var format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
 
        var brush = new SolidBrush(color);
 
        g.DrawString(text, SystemFonts.MenuFont, brush, location, format);
    }

    /////////////////////////////////////////////////////////////////////////
    
}

