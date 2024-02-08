using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public class Menu : Form
{
    private Graphics g = null;
    private Bitmap bmp = null;
    private Image img = null;
    ChooseButton ngBtn = null;
    ChooseButton cntBtn = null;
    ChooseButton exBtn = null;
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };
    public Menu()
    {
        NewGame game = new NewGame();

        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        this.Text = "Joguinho";

        Controls.Add(pb);

        this.img = Bitmap.FromFile("img/Game/FootballStrategy.png");

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width,
                pb.Height
            );
            this.g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            pb.Image = bmp;

            g.DrawImage(img, 0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            SizeF btnSize = new SizeF(pb.Width * 0.1198f, pb.Height * 0.078f);

            ngBtn = new ChooseButton(g, pb.Width * 0.25f, pb.Height * 0.7f, btnSize.Width, btnSize.Height, "New Game");
            ngBtn.DrawChooseButton(g);

            cntBtn = new ChooseButton(g, pb.Width * 0.45f, pb.Height * 0.7f, btnSize.Width, btnSize.Height, "Continue");
            cntBtn.DrawChooseButton(g);

            exBtn = new ChooseButton(g, pb.Width * 0.65f, pb.Height * 0.7f, btnSize.Width, btnSize.Height, "Exit");
            exBtn.DrawChooseButton(g);

            pb.Refresh();
        };

        this.FormClosed += delegate
        {
            Application.Exit();
        };

        pb.MouseDown += (o, e) =>
        {
            if(ngBtn.Rect.Contains(e.X, e.Y))
            {
                this.Hide();
                game.Show();
            }
            if(exBtn.Rect.Contains(e.X, e.Y))
            {
                Application.Exit();
            }
        };
    }
}
