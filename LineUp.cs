using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using Extra;

namespace Views;

public class LineUp : Form
{
    Bitmap bmp = null;
    public Graphics g = null;
    PointF cursor = PointF.Empty;
    PointF? grabStart = null;
    PointF? grabDesloc = null;

    bool isDown = false;

    Timer tm = new Timer();
    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };
    Formation formation = new Tactical433();
    PositionDrawer position = new PositionDrawer();
    GameTactics gameTactics = new GameTactics();

    SolidBrush grayBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));

    public Image shirt = Bitmap.FromFile("./img/Shirt.png");



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
            Draws.Graphics = g;
            pb.Image = bmp;
            tm.Start();
            
        };

        tm.Tick += delegate
        {
            g.Clear(Color.DarkGreen);

            Draws.Menu();
            Draws.MenuBorder();
            Draws.DrawField(Bitmap.FromFile("./img/Field.png"));
            
            DrawPlayer(shirt, player);

            pb.Refresh();
        };

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public RectangleF DrawPlayer(Image image, RectangleF location)
    {

        float realWidth = location.Width;
        var realSize = new SizeF(location.Width, location.Height);
 
        var deslocX = grabDesloc?.X ?? 0;
        var deslocY = grabDesloc?.Y ?? 0;

        PointF stance = new PointF(location.X + deslocX, location.Y + deslocY);
        RectangleF rect = new RectangleF(stance, realSize);
 
        bool cursorIn = rect.Contains(cursor);

        var nameRect = new RectangleF(rect.X + 14 , rect.Y + 90, 2 * realWidth / 3, realWidth / 6);
 
        if (!cursorIn && (deslocX != 0 || deslocY != 0))
            rect = new RectangleF(location.Location, realSize);

        var pen = new Pen(cursorIn ? Color.LightGreen : Color.Black, 2f);

        if (cursorIn && !isDown)
        {
            

            grabStart = null;
        }
        
        if (!cursorIn || !isDown)
        {
            g.FillRectangle(Brushes.LightBlue, rect);
            g.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);

            return rect;
        }

        formation.Draw(cursor, isDown);

         
        if (grabStart == null)
        {
            grabStart = cursor;
            return rect;
        }

        RectangleF tShirt = new RectangleF(rect.X = cursor.X - 40,  rect.Y = cursor.Y - 40, 86, 88);
        grabDesloc = new PointF(cursor.X - grabStart.Value.X, cursor.Y - grabStart.Value.Y);
        
        Draws.DrawPlayerShirt(image, tShirt);        
        Draws.DrawText("Murilo", Color.Black, nameRect);

        return rect;
    }
    public void DrawShirtOnPlayer(RectangleF playerPosition)
    {
        if (shirt != null)
        {
            g.DrawImage(shirt, playerPosition);
        }
    }
}