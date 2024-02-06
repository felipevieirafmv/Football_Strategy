using System;
using System.Drawing;
using System.Windows.Forms;
using Game;


namespace Views;

public class Standings : Form
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


            var pen = new Pen(Color.Black, 2);
            var i = 0;
            var totalHeight = 50 * (Teams.GetAllTeams.Count + 1);
            var startY = (Height - totalHeight) / 2;


            g.FillRectangle(Brushes.White, 600, startY, 1200, totalHeight);
            g.DrawRectangle(pen, 600, startY, 1200, totalHeight);
            Draws.DrawText("Tems", Color.Black, new RectangleF(600, startY, 200, 50));
            Draws.DrawText("Points", Color.Black, new RectangleF(1200, startY, 200, 50));
            Draws.DrawText("Pontos", Color.Black, new RectangleF(1600, startY, 200, 50));

            foreach (var teams in Teams.GetAllTeams)
            {
                i++;
                g.FillRectangle(Brushes.White, 600, startY + 50 * i, 1200, 50);
                g.DrawRectangle(pen, 600, startY + 50 * i, 1200, 50);
                Draws.DrawText(teams.Name, Color.Black, new RectangleF(600, startY + 50 * i, 200, 50));
                Draws.DrawPoints(teams.Points.ToString(), Color.Black, new RectangleF(1200, startY + 50 * i, 200, 50));
                Draws.DrawGDTeam(teams.GD.ToString(), Color.Black, new RectangleF(1600, startY + 50 * i, 200, 50));
            }

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

    // public void DrawTeams(int index)
    // {
    //     var item = Teams.GetAllTeams[index: index];
    //     var defaultRect = new RectangleF(pb.Width*0.677f, pb.Height*0.037f, pb.Width*0.234f, pb.Height*0.037f);
    //     var playerRect = item.rect ?? defaultRect;
    //     var player = item.player;
    //     var selected = item.selected;

    //     var pen = new Pen(Color.Black, 2);
        
    //     foreach (var position in list)
    //     {
    //         Draws.Graphics.FillRectangle(Brushes.White, playerRect);
    //         Draws.Graphics.DrawRectangle(pen, playerRect);
    //         Draws.DrawText(text: player.Name,Color.Black,playerRect);
    //     }


    //     list[index] = (playerRect == defaultRect ? null : playerRect , player, selected);
    //     if (!selected)
    //         return;

    // }
}