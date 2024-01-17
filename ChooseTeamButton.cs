using System;
using System.Drawing;
using System.Windows.Forms;
using Views;

public class ChooseTeamButton : BaseButton
{
    public RectangleF Rect { get; set; }
    public bool Selected { get; set; } = false;
    public ChooseTeamButton(Graphics g, float X, float Y, float width, float height)
    {
        this.Rect = new RectangleF(X, Y, width, height);
    }

    public override void DrawChooseTeam(Graphics g)
    {
        Font font= new Font("Copperplate Gothic Bold", this.Rect.Width*0.1f);
        SizeF textSize = g.MeasureString("New Game -->", font);

        if(this.Selected)
            g.FillRectangle(Brushes.Orange, this.Rect);
        else
            g.FillRectangle(Brushes.Gray, this.Rect);
        g.DrawString("New Game -->", font, Brushes.White, new PointF(this.Rect.X, this.Rect.Y*1.01f));
    }
}