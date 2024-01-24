using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Extra;
public abstract class Formation
{
    private Image shirt = Image.FromFile("Img/Shirt.png");
    private List<(Position pos, PointF loc, object player)> list = new();
    SolidBrush grayBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

    public void AddEmptyPosition(Position pos, PointF loc)
        => list.Add((pos, loc, null));

    public Formation() { }
    

    public void SetPlayer(object player, PointF cursor)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var itemRect = new RectangleF(item.loc, size: new SizeF(100, 100));

            if (!itemRect.Contains(cursor))
                continue;
            
            list[i] = (item.pos, item.loc, player);
        }
    }

    public void SetPlayerMenu(object player, PointF location)
    {
        for(int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var itemRect = new RectangleF(item.loc, size: new SizeF(100, 100));

            if(i != 0){
                Draws.DrawPlayerMenu(new PointF(1300,  40));
            }


        }
    }

    public void Draw(PointF cursor, bool mouseDown)
    {
        foreach (var item in list)
        {
            this.DrawEmptyPosition(
                new RectangleF(item.loc.X, item.loc.Y, 86, 88),
                cursor, mouseDown);
            
            if (item.player != null)
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

        Draws.Graphics.FillRectangle(grayBrush, rect);
        Draws.Graphics.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);
 
        if (!cursorIn || !isDown)
            return rect;
        
        return rect;
    }
}