using System.Drawing;
using System.Collections.Generic;


namespace Extra;
public abstract class Formation
{
    private Image shirt = Image.FromFile("Img/Shirt.png");
    private List<(Position pos, PointF loc)> list = new();
    public void Add(Position pos, PointF loc)
        => list.Add((pos, loc));

    public Formation() { }

    public void Draw(PointF cursor, bool mouseDown)
    {
        foreach (var item in list)
        {
            this.DrawEmptyPosition(
                new RectangleF(item.loc.X, item.loc.Y, 86, 88),
                cursor, mouseDown);
        }
    }

    public RectangleF DrawEmptyPosition(RectangleF location, PointF cursor, bool isDown)
    {
        float realWidth = location.Width;
        var realSize = new SizeF(location.Width, location.Height);
 
        var position = new PointF(location.X, location.Y);
        RectangleF rect = new RectangleF(position, realSize);
 
        bool cursorIn = rect.Contains(cursor);
 
        var pen = new Pen(cursorIn ? Color.Green : Color.Black, 1);

        Draws.Graphics.FillRectangle(Brushes.Gray, rect);
        Draws.Graphics.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);
 
        if (!cursorIn || !isDown)
            return rect;
        
        return rect;
    }
}