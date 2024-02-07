using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;
using Game;

public class Field : Form
{
    private Graphics g = null;
    private Bitmap bmp = null;
    private Image img = null;
    private float timeDraw = 0;
    Timer tm = new Timer();
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };

    public Image field = Bitmap.FromFile("./img/Fields/FieldGame.png");

    public Field()
    {
        tm.Interval = 10;
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        Simulator simulation = null;
        
        if(Game.Current.CrrConfrontation[0] == Game.Current.CrrTeam)
            simulation = new Simulator(Game.Current.CrrTeam, Game.Current.CrrConfrontation[1]);
        else
            simulation = new Simulator(Game.Current.CrrConfrontation[0], Game.Current.CrrTeam);

        Controls.Add(pb);

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            Draws.Graphics = g;
            pb.Image = bmp;
            tm.Start();
        };

        KeyDown += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;

            }
        };

        DateTime start = DateTime.Now;
        tm.Tick += delegate
        {
            g.Clear(Color.DarkGreen);

            g.DrawImage(field,0,0,field.Width, field.Height);

            var time = DateTime.Now - start;

            g.DrawString($"{simulation.TeamHome[0].Team} {simulation.ScoreHome} X {simulation.ScoreAway} {simulation.TeamAway[0].Team} {(60 - time.TotalSeconds>0?"1:":"0:")}{(int)((120 - time.TotalSeconds)%60)}",
                SystemFonts.MenuFont,
                Brushes.Black,
                new RectangleF(Screen.PrimaryScreen.Bounds.Width*0.433f, Screen.PrimaryScreen.Bounds.Height*0.125f, 255, 46)
            );

            simulation.Draw(g, (float)time.TotalSeconds);
            
            // if(60 - time.TotalSeconds < 50)
            // {
            //     this.Close();
            //     Standings standings = new Standings();
            //     standings.Show();
            //     return;
            // }

            pb.Refresh();
        };
    }
}
