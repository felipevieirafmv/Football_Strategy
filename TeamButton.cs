using System;
using System.Drawing;

namespace Views;

public class TeamButton : BaseButton
{
    public RectangleF Rect { get; set; }
    public Image Image { get; set; }
    public string Name { get; set; }
    public bool Selected { get; set; } = false;

    public TeamButton(Graphics g, Image image, float X, float Y, float width, float height, string Name)
    {
        this.Image = image;
        this.Rect = new RectangleF(X, Y, width, height);
        this.Name = Name;
    }

    public override void DrawTeam(Graphics g)
    {
        Font font= new Font("Copperplate Gothic Bold", this.Rect.Width*0.05f);
        SizeF textSize = g.MeasureString(Name, font);

        if(this.Selected)
            g.FillRectangle(Brushes.Orange, this.Rect);
        else
            g.FillRectangle(Brushes.Gray, this.Rect);
        g.DrawImage(this.Image, new RectangleF((this.Rect.X + (this.Rect.Width/2 - this.Rect.Width*0.1285f)), this.Rect.Y + (this.Rect.Height/2 - (this.Rect.Height*0.6f)/1.65f), this.Rect.Width*0.257f, this.Rect.Height*0.6f));
        g.DrawString(Name, font, Brushes.White, new PointF(this.Rect.X + (this.Rect.Width/2 - textSize.Width/2), this.Rect.Y + this.Rect.Height*0.825f));
    }
}