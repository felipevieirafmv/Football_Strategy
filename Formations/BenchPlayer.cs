using System.Drawing;

namespace Views;

public class BenchPlayer : Formation 
{
    public BenchPlayer()
    {
        AddEmptyPosition(Position.Bench, new PointF(1000, 100));
        AddEmptyPosition(Position.Bench, new PointF(1000,300));
        AddEmptyPosition(Position.Bench, new PointF(1000,300));
        AddEmptyPosition(Position.Bench, new PointF(1000,300));
        AddEmptyPosition(Position.Bench, new PointF(1000,300));
        AddEmptyPosition(Position.Bench, new PointF(1000,300));
    }
}