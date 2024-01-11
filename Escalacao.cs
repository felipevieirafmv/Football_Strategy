using System;
using System.Drawing;
using System.Windows.Forms;

public class Escalacao : Form
{

Bitmap bmp = null;
Graphics g = null;

public Escalacao() {



var pb = new PictureBox {
    Dock = DockStyle.Fill,
};

var form = new Form {
    WindowState = FormWindowState.Maximized,
    FormBorderStyle = FormBorderStyle.None,
    Controls = { pb }
};


form.Load += (o, e) =>
{
    bmp = new Bitmap(
        pb.Width, 
        pb.Height
    );
    g = Graphics.FromImage(bmp);
    g.Clear(Color.Black);
    pb.Image = bmp;

};

}
}

