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

    List<(RectangleF? rect, Player player, bool selected)> list = new();


        

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

            foreach (var teams in Teams.GetAllTeams)
            {
                i++;
                Draws.Graphics.FillRectangle(Brushes.White, 600, 50 * i, 1200, 50);
                Draws.Graphics.DrawRectangle(pen, 600, 50 * i, 1200, 50);
                Draws.DrawText(teams.Name,Color.Black,new RectangleF(600, 50 * i, 1200, 50));
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

    public void DrawTeams(int index)
    {
        var item = list[index: index];
        var defaultRect = new RectangleF(pb.Width*0.677f, pb.Height*0.037f + (index - scrollInfo) * pb.Height*0.037f, pb.Width*0.234f, pb.Height*0.037f);
        var playerRect = item.rect ?? defaultRect;
        var player = item.player;
        var selected = item.selected;

        if (isDown && isRight)
        {
            Player removed = null;
            formation.SetPlayer(null, cursor, ref removed, pb);
            if (removed is not null)
                AddPlayer(removed);
        }

        bool cursorIn = playerRect.Contains(cursor);

        if (cursorIn && isDown && list.All(x => !x.selected))
            selected = true;

        if (!isDown)
        {
            if (selected)
            {
                Player removed = null;
                if (formation.SetPlayer(player, cursor, ref removed, pb))
                    list.RemoveAt(index);
                else 
                {
                    list[index] = (null, player, false);
                }

                if (removed is not null)
                {
                    AddPlayer(removed);
                }
                return;
            }
            selected = false;
        }

        if(!cursorIn || !isDown && !selected)
        {
            if(!selected)
            {
                var pen = new Pen(Color.Black, 2);
                
                foreach (var position in list)
                {
                    Draws.Graphics.FillRectangle(Brushes.White, playerRect);
                    Draws.Graphics.DrawRectangle(pen, playerRect);
                    Draws.DrawText(text: player.Name,Color.Black,playerRect);
                }
            }
        }

        list[index] = (playerRect == defaultRect ? null : playerRect , player, selected);
        if (!selected)
            return;

        Draws.DrawPlayerShirt(
            new PointF(cursor.X - pb.Width*0.022f, cursor.Y - pb.Height*0.04f), pb, this.shirt);
        Draws.DrawText(player.Name,Color.Black, 
            new RectangleF(cursor.X - pb.Width*0.022f, cursor.Y + pb.Height*0.04f, pb.Width*0.044f, pb.Height*0.027f));

    }
}