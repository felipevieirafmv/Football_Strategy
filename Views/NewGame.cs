using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Views;
using Game;

public class NewGame : Form
{
    public bool selecionado = false;
    private Graphics g = null;
    private Bitmap bmp = null;
    private PictureBox pb = new PictureBox {
        Dock = DockStyle.Fill,
    };

    List<TeamButton> Teams = new List<TeamButton>();
    ChooseTeamButton chooseTeam = null;
    
    public NewGame()
    {
        StartGame sg = null;

        WindowState = FormWindowState.Maximized;
        FormBorderStyle = FormBorderStyle.None;
        this.Text = "Joguinho";
        SolidBrush celestialBlue = new SolidBrush(Color.FromArgb(255, 68, 157, 209));
        SolidBrush skyBlue = new SolidBrush(Color.FromArgb(255, 120, 192, 224));
        SolidBrush lightYellow = new SolidBrush(Color.FromArgb(255, 229, 250, 184));

        Controls.Add(pb);

        this.Load += delegate 
        {
            this.Cursor = new Cursor("./img/felipera2.cur");
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

            g.FillRectangle(celestialBlue, 0, 0, pb.Width, pb.Height);
            g.FillRectangle(lightYellow, XIB, YIB, pb.Width*0.9f, pb.Height*0.84f);
            
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/america.png"), XIB + XTB, YIB + YTB, WTB, HTB, "America"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/athletico.png"), XTB2 + DifX, YIB + YTB, WTB, HTB, "Athletico"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/atleticomg.png"), XTB2 + DifX*2, YIB + YTB, WTB, HTB, "AtleticoMG"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/bahia.png"), XTB2 + DifX*3, YIB + YTB, WTB, HTB, "Bahia"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/botafogo.png"), XTB2 + DifX*4, YIB + YTB, WTB, HTB, "Botafogo"));

            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/corinthians.png"), XIB + XTB, YTB2 + DifY, WTB, HTB, "Corinthians"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/coritiba.png"), XTB2 + DifX, YTB2 + DifY, WTB, HTB, "Coritiba"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/cruzeiro.png"), XTB2 + DifX*2, YTB2 + DifY, WTB, HTB, "Cruzeiro"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/cuiaba.png"), XTB2 + DifX*3, YTB2 + DifY, WTB, HTB, "Cuiaba"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/flamengo.png"), XTB2 + DifX*4, YTB2 + DifY, WTB, HTB, "Flamengo"));

            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/fluminense.png"), XIB + XTB, YTB2 + DifY*2, WTB, HTB, "Fluminense"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/fortaleza.png"), XTB2 + DifX, YTB2 + DifY*2, WTB, HTB, "Fortaleza"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/goias.png"), XTB2 + DifX*2, YTB2 + DifY*2, WTB, HTB, "Goias"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/gremio.png"), XTB2 + DifX*3, YTB2 + DifY*2, WTB, HTB, "Gremio"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/internacional.png"), XTB2 + DifX*4, YTB2 + DifY*2, WTB, HTB, "Internacional"));

            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/palmeiras.png"), XIB + XTB, YTB2 + DifY*3, WTB, HTB, "Palmeiras"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/bragantino.png"), XTB2 + DifX, YTB2 + DifY*3, WTB, HTB, "RBBragantino"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/santos.png"), XTB2 + DifX*2, YTB2 + DifY*3, WTB, HTB, "Santos"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/saopaulo.png"), XTB2 + DifX*3, YTB2 + DifY*3, WTB, HTB, "SaoPaulo"));
            Teams.Add(new TeamButton(this.g, Bitmap.FromFile("img/vasco.png"), XTB2 + DifX*4, YTB2 + DifY*3, WTB, HTB, "Vasco"));

            chooseTeam = new ChooseTeamButton(g, pb.Width*0.9f - XTB, pb.Height*0.93f, 200, 50);
            chooseTeam.DrawChooseTeam(g);

            foreach (TeamButton item in Teams)
            {
                item.DrawTeam(g);
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

            chooseTeam.Selected = true;

            pb.Refresh();
        };

        pb.MouseUp += (o, e) =>
        {
            if (chooseTeam.Rect.Contains(e.X, e.Y))
            {
                foreach(TeamButton item in Teams)
                {
                    if (item.Selected)
                    {
                        Game.New(item.Name);
                        LineUp lu = new LineUp(Game.Current.TeamGame);
                        this.Hide();
                        lu.Show();
                    }
                }
            }
        };
    }
}
