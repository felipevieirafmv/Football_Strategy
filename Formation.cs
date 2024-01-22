using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using Views;


namespace Extra;
public class Formation
{
    Position t = new Position();

    public Formation() { }

   public void Tactical_4_3_3(LineUp lineUp)
    {
        PlacePlayer(lineUp, t.GoalKeeper(new PointF(522, 800))); //GL
        PlacePlayer(lineUp, t.LeftBack(new PointF(246, 640))); //LE
        PlacePlayer(lineUp, t.Defender(new PointF(422, 680))); //ZC
        PlacePlayer(lineUp, t.Defender(new PointF(621, 680))); //ZC
        PlacePlayer(lineUp, t.RightBack(new PointF(800, 640))); //LD
        PlacePlayer(lineUp, t.Midfield(new PointF(521, 500))); //VOL
        PlacePlayer(lineUp, t.Midfield(new PointF(382, 400))); //MC
        PlacePlayer(lineUp, t.Midfield(new PointF(662, 400))); //MC
        PlacePlayer(lineUp, t.LeftWinger(new PointF(246, 200))); //PE
        PlacePlayer(lineUp, t.RightWinger(new PointF(800, 200))); //PD
        PlacePlayer(lineUp, t.Striker(new PointF(522, 150))); //ATA
    }
    private void PlacePlayer(LineUp lineUp, RectangleF position)
    {
        lineUp.DrawEmptyPosition(position);

        if (t.hasPlayer)
        {
            lineUp.DrawShirtOnPlayer(position);
        }

        // Adicione lógica adicional, se necessário
    }
}