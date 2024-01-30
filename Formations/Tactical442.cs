using System.Drawing;

namespace Views;

public class Tactical442 : Formation 
{
    public Tactical442()
    {
        AddEmptyPosition(Position.GoalKeeper, new PointF(522, 800)); //GL
        AddEmptyPosition(Position.LeftBack, new PointF(246, 640)); //LE
        AddEmptyPosition(Position.Defender, new PointF(422, 680)); //ZC
        AddEmptyPosition(Position.Defender, new PointF(621, 680)); //ZC
        AddEmptyPosition(Position.RightBack, new PointF(800, 640)); //LD
        AddEmptyPosition(Position.Midfield, new PointF(421, 440)); //MC
        AddEmptyPosition(Position.Midfield, new PointF(621, 440)); //MCD
        AddEmptyPosition(Position.Midfield, new PointF(262, 400)); //ME
        AddEmptyPosition(Position.Midfield, new PointF(762, 400)); //MD
        AddEmptyPosition(Position.Striker,new PointF(622, 150)); //ATA
        AddEmptyPosition(Position.Striker, new PointF(422, 150)); //ATA
    }
}