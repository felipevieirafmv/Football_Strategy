using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public class ChooseButton : BaseButton
{
    public RectangleF Rect { get; set; }
    public bool Selected { get; set; } = false;
    private string text { get; set; } 
    public ChooseButton(Graphics g, float X, float Y, float width, float height, string text)
    {
        this.Rect = new RectangleF(X, Y, width, height);
        this.text = text;
    }

    public override void DrawChooseButton(Graphics g)
    {
        Font font= new Font("Copperplate Gothic Bold", this.Rect.Width*0.1f);
        SizeF textSize = g.MeasureString(this.text, font);

        if(this.Selected)
            g.FillRectangle(Brushes.Orange, this.Rect);
        else
            g.FillRectangle(Brushes.Gray, this.Rect);
        g.DrawString(this.text, font, Brushes.White, new PointF(this.Rect.X + (this.Rect.Width/2-textSize.Width/2), this.Rect.Y + (this.Rect.Height/2-textSize.Height/2)));
    }
}