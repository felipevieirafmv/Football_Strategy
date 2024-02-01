using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public class Menu : Form
{
    private Graphics g = null;
    private Bitmap bmp = null;
    private Image img = null;
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };
    public Menu()
    {
        NewGame game = new NewGame();

        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        this.Text = "Joguinho";

        Button ngBtn = new Button();
        ngBtn.Text = "New Game";
        ngBtn.Font = new Font("Comic Sans MS", 30);
        ngBtn.Width = 230;
        ngBtn.Height = 85;
        ngBtn.Location = new Point(1300, 340);
        ngBtn.Click += delegate
        {
            this.Hide();
            game.Show();
        };

        Button cntBtn = new Button();
        cntBtn.Text = "Continue";
        cntBtn.Font = new Font("Comic Sans MS", 30);
        cntBtn.Width = 230;
        cntBtn.Height = 85;
        cntBtn.Location = new Point(1300, 497);
        cntBtn.Click += delegate
        {
            this.Hide();
            //Abrir save do usuario
        };

        Button exBtn = new Button();
        exBtn.Text = "Exit";
        exBtn.Font = new Font("Comic Sans MS", 30);
        exBtn.Width = 230;
        exBtn.Height = 85;
        exBtn.Location = new Point(1300, 640);
        exBtn.Click += delegate
        {
            Application.Exit();
        };

        Controls.Add(ngBtn);
        Controls.Add(cntBtn);
        Controls.Add(exBtn);
        Controls.Add(pb);

        this.img = Bitmap.FromFile("img/Game/FootballStrategy.png");

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width,
                pb.Height
            );
            this.g = Graphics.FromImage(bmp);
            pb.Image = bmp;
            g.DrawImage(img, 0, 0, img.Width * 1.15f, img.Height* 1.15f);
            pb.Refresh();
        };
    }
}
