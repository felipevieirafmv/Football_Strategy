using System;
using System.Drawing;

namespace Views;

public class TeamButton : BaseButton
{   
    public RectangleF Rect { get; set; }
    public string Label { get; set; }
    public Image image { get; set; }

    private PointF cursor = PointF.Empty;
    private bool inside = false;

    public TeamButton(Graphics g, Image image, float X, float Y, float width, float heigth)
    {
        this.image = image;
        this.Rect = new RectangleF(X, Y, width, heigth);
    }

    public override void DrawTeam(Graphics g, Image image, RectangleF Rect)
    {
        g.FillRectangle(Brushes.Gray, Rect);
        g.DrawImage(image, new RectangleF((Rect.X + (Rect.Width/2 - image.Width/6)), Rect.Y + (Rect.Height/2 - image.Height/5), image.Width/3, image.Height/3));
        g.DrawString("Teste 1", new Font("Comic Sans MS", 30), Brushes.Blue, new PointF(Rect.X, Rect.Y));
    }
    public override void DrawSelected(Graphics g, Image image, RectangleF Rect)
    {
        g.FillRectangle(Brushes.Orange, Rect);
        g.DrawImage(image, new RectangleF((Rect.X + (Rect.Width/2 - image.Width/6)), Rect.Y + (Rect.Height/2 - image.Height/5), image.Width/3, image.Height/3));
    }
}