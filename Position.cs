using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Extra;

public class Position
{

    public bool hasPlayer = false;
    public Position() { }

    public void SetShirt()
        => hasPlayer = true;
    public bool HasShirt()
    {
        return hasPlayer;
    }


    public RectangleF GoalKeeper(PointF position)
    {
        RectangleF goalkeeper = new RectangleF();
        goalkeeper.X = position.X;
        goalkeeper.Y = position.Y;
        goalkeeper = TemplatePosition(goalkeeper);

        return goalkeeper;
    }

    public RectangleF LeftBack(PointF position)
    {
        RectangleF leftBack = new RectangleF();
        leftBack.X = position.X;
        leftBack.Y = position.Y;
        leftBack = TemplatePosition(leftBack);

        return leftBack;
    }

    public RectangleF Defender(PointF position)
    {
        RectangleF defender = new RectangleF();
        defender.X = position.X;
        defender.Y = position.Y;
        defender = TemplatePosition(defender);

        return defender;
    }

    public RectangleF RightBack(PointF position)
    {
        RectangleF rightBack = new RectangleF();
        rightBack.X = position.X;
        rightBack.Y = position.Y;
        rightBack = TemplatePosition(rightBack);

        return rightBack;
    }

    public RectangleF Midfield(PointF position)
    {
        RectangleF midfield = new RectangleF();
        midfield.X = position.X;
        midfield.Y = position.Y;
        midfield = TemplatePosition(midfield);

        return midfield;
    }

    public RectangleF LeftWinger(PointF position)
    {
        RectangleF leftWinger = new RectangleF();
        leftWinger.X = position.X;
        leftWinger.Y = position.Y;
        leftWinger = TemplatePosition(leftWinger);

        return leftWinger;
    }

    public RectangleF Striker(PointF position)
    {
        RectangleF striker = new RectangleF();
        striker.X = position.X;
        striker.Y = position.Y;
        striker = TemplatePosition(striker);

        return striker;
    }

    public RectangleF RightWinger(PointF position)
    {
        RectangleF rightWinger = new RectangleF();
        rightWinger.X = position.X;
        rightWinger.Y = position.Y;
        rightWinger = TemplatePosition(rightWinger);

        return rightWinger;
    }
    

    public RectangleF TemplatePosition(RectangleF position)
    {
        position.Location = new PointF(position.X, position.Y);
        position.Height = 88;
        position.Width = 86;

        return position;
    }
}
