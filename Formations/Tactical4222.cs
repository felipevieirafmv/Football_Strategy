using System.Drawing;
using Extra;

public class Tactical4222 : Formation 
{
    public Tactical4222()
    {
        AddEmptyPosition(Position.GoalKeeper, new PointF(522, 800)); //GL
        AddEmptyPosition(Position.LeftBack, new PointF(246, 640)); //LE
        AddEmptyPosition(Position.Defender, new PointF(422, 680)); //ZC
        AddEmptyPosition(Position.Defender, new PointF(621, 680)); //ZC
        AddEmptyPosition(Position.RightBack, new PointF(800, 640)); //LD
        AddEmptyPosition(Position.Midfield, new PointF(421, 540)); //VOL
        AddEmptyPosition(Position.Midfield, new PointF(621, 540)); //VOL
        AddEmptyPosition(Position.Midfield, new PointF(312, 300)); //MEI
        AddEmptyPosition(Position.Midfield, new PointF(712, 300)); //MEI
        AddEmptyPosition(Position.Striker,new PointF(622, 150)); //ATA
        AddEmptyPosition(Position.Striker, new PointF(422, 150)); //ATA
    
    }
}