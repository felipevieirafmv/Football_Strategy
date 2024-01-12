using System;
using System.Drawing;
using System.Windows.Forms;

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
        this.Load += delegate
        {
            WindowState = FormWindowState.Maximized;
            this.Text = "Joguinho";

            Button ngBtn = new Button();
            ngBtn.Text = "New Game";
            ngBtn.Font = new Font("Comic Sans MS", 30);
            ngBtn.Width = 200;
            ngBtn.Height = 70;
            ngBtn.Location = new Point(1300, 330);
            ngBtn.Click += delegate
            {
                this.Hide();
                //Abrir tela de novo jogo
            };

            Button cntBtn = new Button();
            cntBtn.Text = "Continue";
            cntBtn.Font = new Font("Comic Sans MS", 30);
            cntBtn.Width = 200;
            cntBtn.Height = 70;
            cntBtn.Location = new Point(1300, 480);
            cntBtn.Click += delegate
            {
                this.Hide();
                //Abrir save do usuario
            };

            Button exBtn = new Button();
            exBtn.Text = "Exit";
            exBtn.Font = new Font("Comic Sans MS", 30);
            exBtn.Width = 200;
            exBtn.Height = 70;
            exBtn.Location = new Point(1300, 630);
            exBtn.Click += delegate
            {
                Application.Exit();
            };

            bmp = new Bitmap(
                pb.Width,
                pb.Height
            );

            // this.img = Image.FromFile("Img/suarezPixelado.png");

            this.g = Graphics.FromImage(bmp);

            // this.g.DrawImage(bmp, 0, 0);

            Controls.Add(ngBtn);
            Controls.Add(cntBtn);
            Controls.Add(exBtn);
        };
    }
}