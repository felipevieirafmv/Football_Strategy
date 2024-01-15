using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Views;

public class NewGame : Form
{
    public string selected { get; set; }
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

        Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/athletico.png"), 200, 100, 350, 200));
        Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/athletico.png"), 570, 100, 350, 200));

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
                item.DrawTeam(g, item.image, item.Rect);
            }

            pb.Refresh();
        };

        pb.MouseDown += (o, e) =>
        {
            foreach (TeamButton item in Teams)
            {
                if (item.Rect.Contains(e.X, e.Y))
                    item.DrawSelected(g, item.image, item.Rect);
                else
                    item.DrawTeam(g, item.image, item.Rect);
            }
            pb.Refresh();
        };
    }

    private void teamSelection (Graphics g, Image image, float X, float Y, float widRect, float heiRect)
    {
        this.g.FillRectangle(Brushes.Gray, X, Y, widRect, heiRect);
        this.g.DrawImage(image, new RectangleF((X + (widRect/2 - image.Width/6)), Y + (heiRect/2 - image.Height/5), image.Width/3, image.Height/3));
    }
}