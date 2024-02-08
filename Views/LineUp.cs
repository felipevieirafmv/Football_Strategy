using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Views;

using System.Security.Cryptography.X509Certificates;
using Game;

public class LineUp : Form
{
    Bitmap bmp = null;
    public Graphics g = null;
    PointF cursor = PointF.Empty;
    private List<(Position pos, PointF loc, Player player)> fieldPlayer = new();


    int scrollInfo = 0;
    bool isDown = false;
    bool isRight = false;

    ChooseButton btMatch = null;
    ChooseButton btRand = null;

    Timer tm = new Timer();
    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };
    Formation formation = new Tactical433();

    public Image shirt = null;



    ////////////////////////////////////////////////////////////////////////////////////////////////
    List<(RectangleF? rect, Player player, bool selected)> list = new();
    public void AddPlayer(Player player)
    {
        var rect = new RectangleF
        {
            Location = new PointF(pb.Width*0.677f, pb.Height*0.037f + list.Count * pb.Height*0.037f),
            Width = pb.Width*0.234f,
            Height = pb.Height*0.037f
        };
        list.Add((null, player, false));
    }


    public void SetShirt()
    {
        if(Game.Current.CrrTeam.Name == "America")
            this.shirt = Bitmap.FromFile("./img//Shirts/America.png");

        else if (Game.Current.CrrTeam.Name == "Athletico")
            this.shirt = Bitmap.FromFile("./img//Shirts/Athletico.png");

        else if (Game.Current.CrrTeam.Name == "AtleticoMG")
            this.shirt = Bitmap.FromFile("./img//Shirts/AtleticoMG.png");

        else if (Game.Current.CrrTeam.Name == "Bahia")
            this.shirt = Bitmap.FromFile("./img//Shirts/Bahia.png");

        else if (Game.Current.CrrTeam.Name == "Botafogo")
            this.shirt = Bitmap.FromFile("./img//Shirts/Botafogo.png");

        else if (Game.Current.CrrTeam.Name == "Corinthians")
                this.shirt = Bitmap.FromFile("./img//Shirts/Corinthians.png");
    
        else if (Game.Current.CrrTeam.Name == "Coritiba")
                this.shirt = Bitmap.FromFile("./img/Shirts/Coritiba.png");

        else if (Game.Current.CrrTeam.Name == "Cruzeiro")
            this.shirt = Bitmap.FromFile("./img/Shirts/Cruzeiro.png");

        else if (Game.Current.CrrTeam.Name == "Cuiaba")
            this.shirt = Bitmap.FromFile("./img/Shirts/Cuiaba.png");

        else if (Game.Current.CrrTeam.Name == "Flamengo")
            this.shirt = Bitmap.FromFile("./img/Shirts/Flamengo.png");

        else if (Game.Current.CrrTeam.Name == "Fluminense")
            this.shirt = Bitmap.FromFile("./img/Shirts/Fluminense.png");

        else if (Game.Current.CrrTeam.Name == "Fortaleza")
            this.shirt = Bitmap.FromFile("./img/Shirts/Fortaleza.png");

        else if (Game.Current.CrrTeam.Name == "Goias")
            this.shirt = Bitmap.FromFile("./img//Shirts/Goias.png");

        else if (Game.Current.CrrTeam.Name == "Gremio")
            this.shirt = Bitmap.FromFile("./img/Shirts/Gremio.png");

        else if (Game.Current.CrrTeam.Name == "Internacional")
            this.shirt = Bitmap.FromFile("./img/Shirts/Internacional.png");

        else if (Game.Current.CrrTeam.Name == "Palmeiras")
            this.shirt = Bitmap.FromFile("./img/Shirts/Palmeiras.png");

        else if (Game.Current.CrrTeam.Name == "RBBragantino")
            this.shirt = Bitmap.FromFile("./img/Shirts/RBBragantino.png");

        else if (Game.Current.CrrTeam.Name == "Santos")
            this.shirt = Bitmap.FromFile("./img/Shirts/Santos.png");

        else if (Game.Current.CrrTeam.Name == "São Paulo")
            this.shirt = Bitmap.FromFile("./img/Shirts/SaoPaulo.png");

        else if (Game.Current.CrrTeam.Name == "Vasco")
            this.shirt = Bitmap.FromFile("./img/Shirts/Vasco.png");

        else
            this.shirt = Bitmap.FromFile("./img/Shirts/Shirt.png");

        this.shirt = this.shirt.GetThumbnailImage(pb.Width, pb.Height, null, nint.Zero);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public LineUp(List<Player> playerList)
    {
        tm.Interval = 10;
        WindowState = FormWindowState.Maximized;
        // FormBorderStyle = FormBorderStyle.None;

        SetShirt();

    ////////////////////////////////////////////////////////////////////////////////////////////////
        
        pb.MouseWheel += (o, e) =>
        {
            if (e.Delta > 0)
                scrollInfo++;
            else scrollInfo--;
            
            if (scrollInfo > list.Count - 20)
                scrollInfo = list.Count - 20;

            if (scrollInfo < 0)
                scrollInfo = 0;
        };

        pb.MouseDown += (o, e) =>
        {
            isDown = true;
            isRight = e.Button == MouseButtons.Right;

            if(btMatch.Rect.Contains(e.X, e.Y))
            {
                fieldPlayer = formation.FieldList.OrderByDescending(item => item.loc.Y).ToList();

                Game.Current.TeamGame = new();

                foreach(var p in fieldPlayer)
                {
                    if(p.player is null)
                        continue;

                    Game.Current.TeamGame.Add(p.player);
                }

                if (Game.Current.TeamGame.Count < 11)
                {
                    MessageBox.Show("Incomplete LineUp");
                    return;
                }

                Game.Current.CrrTeam.FirstTeam = Game.Current.TeamGame;

                Game.Current.CrrConfrontation = Game.Current.Confrontations.FirstOrDefault(t => t[0] == Game.Current.CrrTeam || t[1] == Game.Current.CrrTeam);

                Field field = new Field();
                this.Hide();
                field.Show();
            }

            if (btRand.Rect.Contains(e.X, e.Y))
            {
                formation.FieldList = list
                    .Select(el => (Position.Bench, PointF.Empty, el.player))
                    .OrderByDescending(el => Random.Shared.Next())
                    .Take(11)
                    .ToList();
            }
        };

        pb.MouseUp += (o, e) =>
        {
            isDown = false;
        };

        pb.MouseMove += (o, e) =>
        {
            cursor = e.Location;
        };

    ////////////////////////////////////////////////////////////////////////////////////////////////

        var cbTactics = GameTactics.TacticalTraining();
        cbTactics.SelectedIndexChanged += delegate
        {
            if (cbTactics.SelectedIndex == 0)
            {
                this.formation = new Tactical433();
                Game.Current.CrrTeam.Tactical = 0;
                for (int i = scrollInfo; i < int.Min(list.Count, 20 + scrollInfo); i++)
                    DrawPlayer(i);

            }
            if (cbTactics.SelectedIndex == 1)
            {
                this.formation = new Tactical4222();
                Game.Current.CrrTeam.Tactical = 1;

            }
            if (cbTactics.SelectedIndex == 2)
            {
                this.formation = new Tactical442();
                Game.Current.CrrTeam.Tactical = 2;
            }
        };
        var cbStyle = GameTactics.Style();
        cbStyle.SelectedIndexChanged += delegate
        {
            if(cbStyle.SelectedIndex == 0)
                Game.Current.CrrTeam.Style = 0;

            if(cbStyle.SelectedIndex == 1)
                Game.Current.CrrTeam.Style = 1;
            if(cbStyle.SelectedIndex == 2)
                Game.Current.CrrTeam.Style = 2;
        };
        var cbMarking = GameTactics.Style();
        cbMarking.SelectedIndexChanged += delegate
        {
            switch (cbMarking.SelectedIndex)
            {
                case 0:
                    Game.Current.CrrTeam.Marking = 0;
                    break;
                case 1:
                    Game.Current.CrrTeam.Marking = 1;
                    break;
                case 2:
                    Game.Current.CrrTeam.Marking = 2;
                    break;
                default:
                    break;
            }
        };
        var cbAttack = GameTactics.Style();
        cbAttack.SelectedIndexChanged += delegate
        {
            switch (cbAttack.SelectedIndex)
            {
                case 0:
                    Game.Current.CrrTeam.Attack = 0;
                    break;
                case 1:
                    Game.Current.CrrTeam.Attack = 1;
                    break;
                case 2:
                    Game.Current.CrrTeam.Attack = 2;
                    break;
                default:
                    break;
            }
        };

        Controls.Add(cbTactics);
        Controls.Add(cbStyle);
        Controls.Add(cbMarking);
        Controls.Add(cbAttack);
        Controls.Add(GameTactics.Style());
        Controls.Add(GameTactics.MarkingType());
        Controls.Add(GameTactics.Attack());
        Controls.Add(pb);

        ////////////////////////////////////////////////////////////////////////////////////////////////

        foreach(Player p in playerList)
        {
            AddPlayer(p);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        KeyDown += (o, e) =>
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;

            }
        };

        /////////////////////////////////////////////////////////////////////////////////////////////////
        
        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            Draws.Graphics = g;
            pb.Image = bmp;
            tm.Start();

            btMatch = new ChooseButton(g, pb.Width*0.897f, pb.Height * 0.897f, pb.Width*0.093f, pb.Height * 0.055f, "Game");
            btRand = new ChooseButton(g, pb.Width*0.897f, pb.Height * 0.837f, pb.Width*0.093f, pb.Height * 0.055f, "Random");
        };

        this.FormClosed += delegate
        {
            Application.Exit();
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

        tm.Tick += delegate
        {

            g.Clear(Color.DarkGreen);

            
            Draws.Menu(pb);
            Draws.MenuBorder(pb);
            Draws.DrawField(Bitmap.FromFile("./img/Fields/fieldLineUp.png"), pb);
            
                    
            formation.PlayerPosition(pb, this.shirt);
            if (list.Any(x => x.selected))
                formation.Draw(cursor: cursor, isDown, pb);
            for (int i = scrollInfo; i < int.Min(list.Count, 20 + scrollInfo); i++)
                DrawPlayer(i);
                
            btMatch.DrawChooseButton(g);
            btRand.DrawChooseButton(g);
            Draws.DrawPlayerStats( "Name", new PointF( Screen.PrimaryScreen.Bounds.Width * 0.713f, Screen.PrimaryScreen.Bounds.Height * 0.023f));
            Draws.DrawPlayerStats( "GK", new PointF( Screen.PrimaryScreen.Bounds.Width * 0.793f, Screen.PrimaryScreen.Bounds.Height * 0.023f));
            Draws.DrawPlayerStats( "DC", new PointF( Screen.PrimaryScreen.Bounds.Width * 0.813f, Screen.PrimaryScreen.Bounds.Height * 0.023f));
            Draws.DrawPlayerStats( "MC", new PointF( Screen.PrimaryScreen.Bounds.Width * 0.833f, Screen.PrimaryScreen.Bounds.Height * 0.023f));
            Draws.DrawPlayerStats( "ST", new PointF( Screen.PrimaryScreen.Bounds.Width * 0.853f, Screen.PrimaryScreen.Bounds.Height * 0.023f));
            Draws.DrawPlayerStats( "Over", new PointF( Screen.PrimaryScreen.Bounds.Width * 0.873f, Screen.PrimaryScreen.Bounds.Height * 0.023f));

            pb.Refresh();
        };

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawPlayer(int index)
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
                    Draws.DrawText(player.Name,Color.Black,new RectangleF( playerRect.X - playerRect.Width / 3, playerRect.Y , playerRect.Width, playerRect.Height));
                    Draws.DrawText(player.GoalKeeperAbility.ToString(),Color.Black,new RectangleF( playerRect.X + Screen.PrimaryScreen.Bounds.Width * 0.0f, playerRect.Y , playerRect.Width, playerRect.Height));
                    Draws.DrawText(player.IntercepionsAbility.ToString(),Color.Black,new RectangleF( playerRect.X + Screen.PrimaryScreen.Bounds.Width * 0.02f , playerRect.Y , playerRect.Width, playerRect.Height));
                    Draws.DrawText(player.PassingAbility.ToString(),Color.Black,new RectangleF( playerRect.X + Screen.PrimaryScreen.Bounds.Width * 0.04f, playerRect.Y , playerRect.Width, playerRect.Height));
                    Draws.DrawText(player.KickingAblity.ToString(),Color.Black,new RectangleF( playerRect.X + Screen.PrimaryScreen.Bounds.Width * 0.06f, playerRect.Y , playerRect.Width, playerRect.Height));
                    Draws.DrawText(player.OverAll.ToString(),Color.Black,new RectangleF( playerRect.X + Screen.PrimaryScreen.Bounds.Width * 0.08f, playerRect.Y , playerRect.Width, playerRect.Height));
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