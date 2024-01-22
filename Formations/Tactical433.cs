using System.Drawing;
using Extra;

public class Tactical433 : Formation
{
    public Tactical433()
    {
        Add(Position.GoalKeeper, new PointF(522, 800)); //GL
        Add(Position.LeftBack, new PointF(246, 640)); //LE
        Add(Position.Defender, new PointF(422, 680)); //ZC
        Add(Position.Defender, new PointF(621, 680)); //ZC
        Add(Position.RightBack, new PointF(800, 640)); //LD
        Add(Position.Midfield, new PointF(521, 500)); //VOL
        Add(Position.Midfield, new PointF(382, 400)); //MC
        Add(Position.Midfield, new PointF(662, 400)); //MC
        Add(Position.LeftWinger, new PointF(246, 200)); //PE
        Add(Position.RightWinger,new PointF(800, 200)); //PD
        Add(Position.Striker, new PointF(522, 150)); //ATA
    }
}