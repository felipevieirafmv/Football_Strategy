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

        this.Load += delegate 
        {
            bmp = new Bitmap(
                pb.Width,
                pb.Height
            );
            this.g = Graphics.FromImage(bmp);
            pb.Image = bmp;

            float XIB = pb.Width*0.05f; //X inside box
            float YIB = pb.Height*0.08f; //Y inside box
            float XTB = pb.Width*0.0308f;//X team button
            float XTB2 = XTB + XIB;//X team button + X inside box
            float YTB = pb.Height*0.0568f;//Y team button
            float YTB2 = YTB + YIB;//Y team button + Y inside box
            float WTB = pb.Width*0.143f;//Width team button
            float HTB = pb.Height*0.139f;//Heigth team button

            float DifX = XTB + WTB;
            float DifY = YTB + HTB;

            g.FillRectangle(Brushes.DarkCyan, 0, 0, pb.Width, pb.Height);
            g.FillRectangle(Brushes.Cyan, XIB, YIB, pb.Width*0.9f, pb.Height*0.84f);

            
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/america.png"), XIB + XTB, YIB + YTB, WTB, HTB, "América-MG"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/athletico.png"), XTB2 + DifX, YIB + YTB, WTB, HTB, "Athletico Paranaense"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/atleticomg.png"), XTB2 + DifX*2, YIB + YTB, WTB, HTB, "Atlético-MG"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/bahia.png"), XTB2 + DifX*3, YIB + YTB, WTB, HTB, "Bahia"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/botafogo.png"), XTB2 + DifX*4, YIB + YTB, WTB, HTB, "Botafogo"));

            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/corinthians.png"), XIB + XTB, YTB2 + DifY, WTB, HTB, "Corinthians"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/fantasma.jpg"), XTB2 + DifX, YTB2 + DifY, WTB, HTB, "CoritiBa"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/cruzeiro.png"), XTB2 + DifX*2, YTB2 + DifY, WTB, HTB, "Cruzeiro"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/cuiaba.png"), XTB2 + DifX*3, YTB2 + DifY, WTB, HTB, "Cuiabá"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/urubu.jpg"), XTB2 + DifX*4, YTB2 + DifY, WTB, HTB, "Flamengo"));

            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/fluminense.png"), XIB + XTB, YTB2 + DifY*2, WTB, HTB, "Fluminense"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/fortaleza.png"), XTB2 + DifX, YTB2 + DifY*2, WTB, HTB, "Fortaleza"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/goias.png"), XTB2 + DifX*2, YTB2 + DifY*2, WTB, HTB, "Goiás"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/gremio.png"), XTB2 + DifX*3, YTB2 + DifY*2, WTB, HTB, "Grêmio"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/internacional.png"), XTB2 + DifX*4, YTB2 + DifY*2, WTB, HTB, "Internacional"));

            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/palmeiras.png"), XIB + XTB, YTB2 + DifY*3, WTB, HTB, "Palmeiras"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/bragantino.png"), XTB2 + DifX, YTB2 + DifY*3, WTB, HTB, "Red Bull Bragantino"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/santos.png"), XTB2 + DifX*2, YTB2 + DifY*3, WTB, HTB, "Santos"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/saopaulo.png"), XTB2 + DifX*3, YTB2 + DifY*3, WTB, HTB, "São Paulo"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/vasco.png"), XTB2 + DifX*4, YTB2 + DifY*3, WTB, HTB, "Vasco"));



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

                        foreach (TeamButton item2 in Teams)
                        {
                            if (item != item2)
                            {
                                item2.Selected = false;
                                item2.DrawTeam(g);
                            }
                        }

                    }
                }
            }

            pb.Refresh();
        };
    }


}
