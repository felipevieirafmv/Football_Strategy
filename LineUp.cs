using System;
using System.Drawing;
using System.Windows.Forms;
using Extra;

namespace Views;

public class LineUp : Form
{
    Bitmap bmp = null;
    Graphics g = null;
    PointF cursor = PointF.Empty;
    PointF? grabStart = null;
    PointF? grabDesloc = null;

    bool isDown = false;

    Timer tm = new Timer();
    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };
    Draws draw = new Draws();
    Formation formation = new Formation();
    GameTactics gameTactics = new GameTactics();

    SolidBrush grayBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));



    ////////////////////////////////////////////////////////////////////////////////////////////////

    public LineUp()
    {
        tm.Interval = 10;
        WindowState = FormWindowState.Maximized;
        // FormBorderStyle = FormBorderStyle.None;

    ////////////////////////////////////////////////////////////////////////////////////////////////

        pb.MouseDown += (o, e) =>
        {
            isDown = true;
        };

        pb.MouseUp += (o, e) =>
        {
            isDown = false;
            grabDesloc = null;
            grabStart = null;
        };

        pb.MouseMove += (o, e) =>
        {
            cursor = e.Location;
        };

    ////////////////////////////////////////////////////////////////////////////////////////////////

        Controls.Add(gameTactics.TacticalTraining());
        Controls.Add(gameTactics.Style());
        Controls.Add(gameTactics.MarkingType());
        Controls.Add(gameTactics.Attack());
        Controls.Add(pb);

    ////////////////////////////////////////////////////////////////////////////////////////////////
    
        RectangleF player = new RectangleF();
        
        player.Location = new PointF(1300, 40);
        player.Width = 450;
        player.Height = 40;
        
    ////////////////////////////////////////////////////////////////////////////////////////////////

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            tm.Start();
            
        };

        tm.Tick += delegate
        {
            g.Clear(Color.DarkGreen);

            draw.Menu(g);
            draw.MenuBorder(g);
            draw.DrawField(g, Bitmap.FromFile("./img/Field.png"));
            
            DrawPlayer(Bitmap.FromFile("./img/Shirt.png"), player);

            pb.Refresh();
        };

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public RectangleF DrawPlayer( Image image, RectangleF location)
    {

        float realWidth = location.Width;
        var realSize = new SizeF(location.Width, location.Height);
 
        var deslocX = grabDesloc?.X ?? 0;
        var deslocY = grabDesloc?.Y ?? 0;

        PointF position = new PointF(location.X + deslocX, location.Y + deslocY);
        RectangleF rect = new RectangleF(position, realSize);
 
        bool cursorIn = rect.Contains(cursor);
 
        if (!cursorIn && (deslocX != 0 || deslocY != 0))
            rect = new RectangleF(location.Location, realSize);

        var pen = new Pen(cursorIn ? Color.Green : Color.Black, 1f);

        if (!cursorIn || !isDown){
            g.FillRectangle(Brushes.LightBlue, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);
            return rect;
        }
         
        if (grabStart == null)
        {
            grabStart = cursor;
            return rect;
        }

        RectangleF tShirt = new RectangleF(rect.X = cursor.X - 40,  rect.Y = cursor.Y - 40, 86, 88);
        grabDesloc = new PointF(cursor.X - grabStart.Value.X, cursor.Y - grabStart.Value.Y);
        
        formation.Tactical_4_3_3(this);

        draw.DrawPlayerShirt(g, image, tShirt);

        return rect;
    }



    public RectangleF DrawEmptyPosition(RectangleF location)
    {
        float realWidth = location.Width;
        var realSize = new SizeF(location.Width, location.Height);
 
        var position = new PointF(location.X, location.Y);
        RectangleF rect = new RectangleF(position, realSize);
 
        bool cursorIn = rect.Contains(cursor);
 
        var pen = new Pen(cursorIn ? Color.Cyan : Color.Black, 1);

        var nameRect = new RectangleF(rect.X + 14 , rect.Y + 90, 2 * realWidth / 3, realWidth / 6);
 
        g.FillRectangle(grayBrush, rect);
        g.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);
        draw.DrawText(g, "Murilo", Color.Black, nameRect);
 
        if (!cursorIn || !isDown)
            return rect;
         
        if (grabStart == null)
        {
            grabStart = cursor;
            return rect;
        }
         if(cursorIn && isDown )
         {
            //colocar codigo para aparecer aqui das cmamisas
            return rect;
         }
 
        return rect;
    }

 
}

