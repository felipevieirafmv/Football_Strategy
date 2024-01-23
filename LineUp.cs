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

    bool isDown = false;

    Timer tm = new Timer();
    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };
    Formation formation = new Tactical433();
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
        };

        pb.MouseMove += (o, e) =>
        {
            cursor = e.Location;
        };

    ////////////////////////////////////////////////////////////////////////////////////////////////

        var cb = gameTactics.TacticalTraining();
        cb.SelectedIndexChanged += delegate
        {
            if (cb.SelectedIndex == 0)
                this.formation = new Tactical433();
            if (cb.SelectedIndex == 1)
                this.formation = new Tactical4222();
            if (cb.SelectedIndex == 2)
                this.formation = new Tactical442();
        };
        Controls.Add(cb);
        Controls.Add(gameTactics.Style());
        Controls.Add(gameTactics.MarkingType());
        Controls.Add(gameTactics.Attack());
        Controls.Add(pb);

        ////////////////////////////////////////////////////////////////////////////////////////////////

        RectangleF player = new RectangleF
        {
            Location = new PointF(1300, y: 40),
            Width = 450,
            Height = 40
        };

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
            
            DrawPlayer(player);

            pb.Refresh();
        };

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    bool selected = false;
    public void DrawPlayer(RectangleF location)
    {
        bool cursorIn = location.Contains(cursor);

        if (cursorIn && isDown)
            selected = true;
            
        if (!isDown)
        {
            if (selected)
                formation.SetPlayer(4, cursor);
            selected = false;
        }
        
        if (!selected)
            return;
        
        formation.Draw(cursor, isDown);

        Draws.DrawPlayerShirt(shirt, 
            new RectangleF(cursor.X - 43, cursor.Y - 44, 86, 88));
    }
}