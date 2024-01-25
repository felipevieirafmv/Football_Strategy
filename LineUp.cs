using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Extra;

namespace Views;

public class LineUp : Form
{
    Bitmap bmp = null;
    public Graphics g = null;
    PointF cursor = PointF.Empty;
    PointF? grabStart = null;

    bool isDown = false;

    Timer tm = new Timer();
    private PictureBox pb = new PictureBox{
        Dock = DockStyle.Fill,
    };
    Formation formation = new Tactical433();
    GameTactics gameTactics = new GameTactics();

    public Image shirt = Bitmap.FromFile("./img/Shirt.png");


    ////////////////////////////////////////////////////////////////////////////////////////////////

    List<(RectangleF rect, object player, bool selected)> list = new();

    public void AddPlayer(object player)
    {
        var rect = new RectangleF
        {
            Location = new PointF(1300, y: 40 + list.Count * 40),
            Width = 450,
            Height = 40
        };
        list.Add((rect, player, false));
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////

    public LineUp()
    {
        tm.Interval = 10;
        WindowState = FormWindowState.Maximized;
        // FormBorderStyle = FormBorderStyle.None;

    ////////////////////////////////////////////////////////////////////////////////////////////////

        pb.MouseDown += (o, e) =>
        {
            isDown = true;
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

        var cb = gameTactics.TacticalTraining();
        cb.SelectedIndexChanged += delegate
        {
            if (cb.SelectedIndex == 0)
                this.formation = new Tactical433();
            if (cb.SelectedIndex == 1)
                this.formation = new Tactical4222();
            if (cb.SelectedIndex == 2)
                this.formation = new Tactical442();
        };
        Controls.Add(cb);
        Controls.Add(gameTactics.Style());
        Controls.Add(gameTactics.MarkingType());
        Controls.Add(gameTactics.Attack());
        Controls.Add(pb);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        AddPlayer("Murilão");
        AddPlayer("Filipinho");
        AddPlayer("Lander louco");
        AddPlayer("Ratue");
        AddPlayer("Renaight");
        AddPlayer("Cineminha");
        AddPlayer("Psicopata Do Detram");
        AddPlayer("VR");
        AddPlayer("Zago do Bem");
        AddPlayer("Reizinho Delas");
        AddPlayer("Feldzinho");

        ////////////////////////////////////////////////////////////////////////////////////////////////

        this.Load += delegate
        {
            bmp = new Bitmap(
                pb.Width, 
                pb.Height
            );
            g = Graphics.FromImage(bmp);
            Draws.Graphics = g;
            pb.Image = bmp;
            tm.Start();
            
        };

        tm.Tick += delegate
        {
            g.Clear(Color.DarkGreen);

            Draws.Menu();
            Draws.MenuBorder();
            Draws.DrawField(Bitmap.FromFile("./img/Field.png"));
            
            formation.Draw(cursor: cursor, isDown);
            for (int i = 0; i < list.Count; i++)
                DrawPlayer(i);

            pb.Refresh();
        };

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void DrawPlayer(int index)
    {
        var item = list[index];
        var playerRect = item.rect;
        var player = item.player;
        var selected = item.selected;

        bool cursorIn = playerRect.Contains(cursor);

        if (cursorIn && isDown && list.All(x => !x.selected))
            selected = true;
            
        if (!isDown)
        {
            if (selected)
            {
                if (formation.SetPlayer(player, cursor))
                    list.RemoveAt(index);
                else 
                {
                    list[index] = (playerRect, player, false);
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

                Draws.Graphics.FillRectangle(Brushes.White, playerRect);
                Draws.Graphics.DrawRectangle(pen, playerRect);
                Draws.DrawText(player.ToString(),Color.Black,playerRect);
            }
        }

        list[index] = (playerRect, player, selected);
        if (!selected)
            return;

        Draws.DrawPlayerShirt(
            new PointF(cursor.X - 43, cursor.Y - 44));
        Draws.DrawText(player.ToString(),Color.Black, 
            new RectangleF(cursor.X - 43, cursor.Y + 44, 86, 20));
    }
}