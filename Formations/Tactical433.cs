using System.Drawing;
using Extra;

public class Tactical433 : Formation
{
    public Tactical433()
    {
        AddEmptyPosition(Position.GoalKeeper, new PointF(522, 800)); //GL
        AddEmptyPosition(Position.LeftBack, new PointF(246, 640)); //LE
        AddEmptyPosition(Position.Defender, new PointF(422, 680)); //ZC
        AddEmptyPosition(Position.Defender, new PointF(621, 680)); //ZC
        AddEmptyPosition(Position.RightBack, new PointF(800, 640)); //LD
        AddEmptyPosition(Position.Midfield, new PointF(521, 500)); //VOL
        AddEmptyPosition(Position.Midfield, new PointF(382, 400)); //MC
        AddEmptyPosition(Position.Midfield, new PointF(662, 400)); //MC
        AddEmptyPosition(Position.LeftWinger, new PointF(246, 200)); //PE
        AddEmptyPosition(Position.RightWinger,new PointF(800, 200)); //PD
        AddEmptyPosition(Position.Striker, new PointF(522, 150)); //ATA
    }
}