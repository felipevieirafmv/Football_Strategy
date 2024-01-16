using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Views;

public class NewGame : Form
{
    public bool selecionado = false;
    private Graphics g = null;
    private Bitmap bmp = null;
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };

    List<TeamButton> Teams = new List<TeamButton>();
    
    public NewGame()
    {
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        this.Text = "Joguinho";

        Controls.Add(pb);

        Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/athletico.png"), 200, 100, 275, 150, "Athletico Paranaense"));
        Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/athletico.png"), 1000, 100, 350, 200, "CAP"));

        this.Load += delegate 
        {
            bmp = new Bitmap(
                pb.Width,
                pb.Height
            );
            this.g = Graphics.FromImage(bmp);
            pb.Image = bmp;

            foreach (TeamButton item in Teams)
            {
                item.DrawTeam(g);
            }

            pb.Refresh();
        };

        pb.MouseDown += (o, e) =>
        {
            foreach (TeamButton item in Teams)
            {
                if (item.Rect.Contains(e.X, e.Y))
                {
                    if(item.Selected)
                    {
                        item.Selected = false;
                        item.DrawTeam(g);
                    }
                    else
                    {
                        item.Selected = true;
                        item.DrawTeam(g);
                    }
                }
            }

            pb.Refresh();
        };
    }


}
