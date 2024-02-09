using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Views;
using Game;

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
                LineUp lu = new LineUp(Game.Current.CrrTeam.Squad);
                this.Hide();
                lu.Show();
            }
        };

        Font font = new Font("Copperplate Gothic Bold", Screen.PrimaryScreen.Bounds.Width*0.005f);
        

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

            btNewRound = new ChooseButton(g, Screen.PrimaryScreen.Bounds.Width*0.897f, Screen.PrimaryScreen.Bounds.Height * 0.897f, Screen.PrimaryScreen.Bounds.Width *0.093f, Screen.PrimaryScreen.Bounds.Height * 0.055f, "New Round");
            g.DrawImage(standings,0,0,Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            var pen = new Pen(Color.Black, 2);
            var i = 0;
            // var totalHeight = Screen.PrimaryScreen.Bounds.Height * 0.00001f * (Teams.GetAllTeams.Count + 1);
            // var startY = (Height - totalHeight) / 2;

            var orderedTeams = Teams.GetAllTeams.OrderByDescending(t => t.Points*1000 + t.Diff);

            var game = Game.Current;
            var matches = game.Confrontations
                .Skip(game.Round * 10)
                .Take(10)
                .Where(m => m[0] != game.CrrTeam && m[1] != game.CrrTeam)
                .ToArray();

            game.Round++;
            foreach (var match in matches)
            {
                var r = Random.Shared.NextSingle();
                var goals = Random.Shared.Next(5);

                if (r < 0.3)
                {
                    match[0].Points++;
                    match[1].Points++;
                }
                else if (r < 0.7)
                {
                    match[0].Points += 3;
                    match[0].Diff += goals;
                    match[1].Diff -= goals;
                }
                else
                {
                    match[1].Points += 3;
                    match[1].Diff += goals;
                    match[0].Diff -= goals;
                }
            }

            var diff = Simulator.ScoreHome - Simulator.ScoreAway;
            if (diff == 0)
            {
                game.CrrConfrontation[0].Points++;
                game.CrrConfrontation[1].Points++;
            }
            else if (diff > 0)
            {
                game.CrrConfrontation[0].Points += 3;
                game.CrrConfrontation[0].Diff += diff;
                game.CrrConfrontation[1].Diff -= diff;
            }
            else
            {
                game.CrrConfrontation[1].Points += 3;
                game.CrrConfrontation[1].Diff += diff;
                game.CrrConfrontation[0].Diff -= diff;
            }

            Game.Current.CrrConfrontation = game.Confrontations
                .Skip(game.Round  * 10)
                .Take(10)
                .Where(m => m[0] == game.CrrTeam || m[1] == game.CrrTeam)
                .FirstOrDefault();
            
            foreach (var teams in orderedTeams)
            {
                i++;
                Draws.DrawTeamName(teams.Name, Color.White, new RectangleF(Screen.PrimaryScreen.Bounds.Width * 0.24f, Screen.PrimaryScreen.Bounds.Height * 0.0379f * i, Screen.PrimaryScreen.Bounds.Width * 0.104f, Screen.PrimaryScreen.Bounds.Height * 0.22f));
                Draws.DrawPoints(teams.Points.ToString(), Color.White, new RectangleF(Screen.PrimaryScreen.Bounds.Width * 0.54f, Screen.PrimaryScreen.Bounds.Height * 0.0379f * i, Screen.PrimaryScreen.Bounds.Width * 0.104f, Screen.PrimaryScreen.Bounds.Height * 0.22f));
                Draws.DrawDiff(teams.Diff.ToString(), Color.White, new RectangleF(Screen.PrimaryScreen.Bounds.Width * 0.74f, Screen.PrimaryScreen.Bounds.Height * 0.0379f * i, Screen.PrimaryScreen.Bounds.Width * 0.104f, Screen.PrimaryScreen.Bounds.Height * 0.22f));
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