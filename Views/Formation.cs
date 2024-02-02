using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using Game;
using Microsoft.VisualBasic;

namespace Views;

public abstract class Formation
{
    private Image shirt = Image.FromFile("Img/Shirt.png");
    public List<(Position pos, PointF loc, Player player)> FieldList = new();
    SolidBrush grayBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
    public Player[] FieldTeam = new Player[11];
    PictureBox pb = new PictureBox();

    public float y { get; set; } = 40;


    public void AddEmptyPosition(Position pos, PointF loc)
        => FieldList.Add((pos, loc, null));

    public Formation() 
    { }    

    public bool SetPlayer(Player player, PointF cursor, ref Player removedPlayer)
    {
        for (int i = 0; i < FieldList.Count; i++)
        {
            var item = FieldList[i];
            var itemRect = new RectangleF(item.loc, size: new SizeF(86, 88));

            if (!itemRect.Contains(cursor))
                continue;
            
            removedPlayer = FieldList[i].player;
            FieldList[i] = (item.pos, item.loc, player);
            return true;
        }
        return false;
    }


    public void PlayerPosition(PictureBox pb)
    {
        foreach (var item in FieldList)
        {
            if(item.player != null)
            {
                Draws.DrawPlayerShirt(new PointF(item.loc.X, item.loc.Y), pb);
                Draws.DrawText(item.player.Name,Color.Black, 
                    new RectangleF(item.loc.X, item.loc.Y + 88, 86, 20));
            } 
        }
    }
    public void Draw(PointF cursor, bool mouseDown)
    {
        
        foreach (var item in FieldList)
        {
            if(item.player == null)
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