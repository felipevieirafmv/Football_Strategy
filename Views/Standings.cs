using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Game;


namespace Views;

public class Standings : Form
{
    private Graphics g = null;
    private Bitmap bmp = null;
    private Image img = null;
    private float timeDraw = 0;

    ChooseButton btNewRound = null;
    Timer tm = new Timer();
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };

    public Image standings = Bitmap.FromFile("./img/Standings/Standing.png");
    public Standings()
    { 
        tm.Interval = 10;
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        BackColor = Color.Green;

        RectangleF teamRect = new RectangleF
        {
            Location = new PointF(pb.Width*0.677f, pb.Height*0.037f + Teams.GetAllTeams.Count * pb.Height*0.037f),
            Width = pb.Width*0.234f,
            Height = pb.Height*0.037f
        };

        pb.MouseDown += (o, e) =>
        {
            if(btNewRound.Rect.Contains(e.X, e.Y))
            {
                
            }
        };
        

        Controls.Add(pb);

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

            btNewRound = new ChooseButton(g, Screen.PrimaryScreen.Bounds.Width*0.897f, Screen.PrimaryScreen.Bounds.Height * 0.897f, Screen.PrimaryScreen.Bounds.Width *0.093f, Screen.PrimaryScreen.Bounds.Height * 0.055f, "New Round");
            g.DrawImage(standings,0,0,Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            var pen = new Pen(Color.Black, 2);
            var i = 0;
            // var totalHeight = Screen.PrimaryScreen.Bounds.Height * 0.00001f * (Teams.GetAllTeams.Count + 1);
            // var startY = (Height - totalHeight) / 2;

            var orderedTeams = Teams.GetAllTeams.OrderByDescending(t => t.Points*1000 + t.Diff);
            
            foreach (var teams in orderedTeams)
            {
                i++;
                Draws.DrawText(teams.Name, Color.Black, new RectangleF(Screen.PrimaryScreen.Bounds.Width * 0.24f, Screen.PrimaryScreen.Bounds.Height * 0.0379f * i, 200, Screen.PrimaryScreen.Bounds.Height * 0.22f));
                Draws.DrawPoints(teams.Points.ToString(), Color.Black, new RectangleF(Screen.PrimaryScreen.Bounds.Width * 0.54f, Screen.PrimaryScreen.Bounds.Height * 0.0379f * i, 200, Screen.PrimaryScreen.Bounds.Height * 0.22f));
                Draws.DrawGDTeam(teams.Diff.ToString(), Color.Black, new RectangleF(Screen.PrimaryScreen.Bounds.Width * 0.74f, Screen.PrimaryScreen.Bounds.Height * 0.0379f * i, 200, Screen.PrimaryScreen.Bounds.Height * 0.22f));
            }
            
            btNewRound.DrawChooseButton(g);

            pb.Refresh();

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



    }
}