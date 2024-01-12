using System;
using System.Drawing;
using System.Windows.Forms;

public class NewGame : Form
{
    public string selected { get; set; }
    private Graphics g = null;
    private Bitmap bmp = null;
    private Image img = null;
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };
    public NewGame()
    {
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        this.Text = "Joguinho";

        Controls.Add(pb);

        // this.img = Bitmap.FromFile("img/athletico.png");

        float X = 200, Y = 100, widRect = 350, heiRect = 200;

        this.Load += delegate 
        {
            bmp = new Bitmap(
                pb.Width,
                pb.Height
            );
            this.g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            teamSelection(Bitmap.FromFile("img/athletico.png"), X, Y, widRect, heiRect);
            teamSelection(Bitmap.FromFile("img/athletico.png"), X + widRect + 20, Y, widRect, heiRect);
            pb.Refresh();
        };
    }

    private void teamSelection (Image image, float X, float Y, float widRect, float heiRect)
    {
        g.FillRectangle(Brushes.Gray, X, Y, widRect, heiRect);
        g.DrawImage(image, new RectangleF((X + (widRect/2 - image.Width/6)), Y + (heiRect/2 - image.Height/5), image.Width/3, image.Height/3));
    }
}