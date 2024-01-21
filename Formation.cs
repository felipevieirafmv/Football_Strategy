using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using Views;


namespace Extra;
public class Formation
{

    public Formation() { }

   public void Tactical_4_3_3(LineUp lineUp)
    {
        Position t = new Position();

        lineUp.DrawEmptyPosition(t.GoalKeeper(new PointF(522, 800))); //GL
        lineUp.DrawEmptyPosition(t.LeftBack(new PointF(246, 640))); //LE
        lineUp.DrawEmptyPosition(t.Defender(new PointF(422, 680))); //ZC
        lineUp.DrawEmptyPosition(t.Defender(new PointF(621, 680))); //ZC
        lineUp.DrawEmptyPosition(t.RightBack(new PointF(800, 640))); //LD
        lineUp.DrawEmptyPosition(t.Midfield(new PointF(521, 500))); //VOL
        lineUp.DrawEmptyPosition(t.Midfield(new PointF(382, 400))); //MC
        lineUp.DrawEmptyPosition(t.Midfield(new PointF(662, 400))); //MC
        lineUp.DrawEmptyPosition(t.LeftWinger(new PointF(246, 200))); //PE
        lineUp.DrawEmptyPosition(t.RightWinger(new PointF(800, 200))); //PD
        lineUp.DrawEmptyPosition(t.Striker(new PointF(522, 150))); //ATA
    }
}