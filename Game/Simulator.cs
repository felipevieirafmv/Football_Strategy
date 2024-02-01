using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Views;

namespace Game;

public class Simulator
{
    public int ScoreHome { get; set; }
    public int ScoreAway { get; set; }

    private int currentTime = -1;
    private Dictionary<Player, PointF> playerMap = new();
    private Dictionary<Player, PointF> nextMap = new();
    private List<PointF> home433 = new();
    private List<PointF> away433 = new();
    private SizeF playerSize= new SizeF(20, 20);
    private Player ball = null;
    private List<Player> teamHome;
    private List<Player> teamAway;
    int taticalHome, styleHome, attackHome, markHome;
    int taticalAway, styleAway, attackAway, markAway;
    public Simulator(Team teamHome, Team teamAway)
    {
        this.teamHome = teamHome.FirstTeam;
        this.teamAway = teamAway.FirstTeam;

        this.taticalHome = teamHome.Tactical;
        this.styleHome = teamHome.Style;
        this.attackHome = teamHome.Attack;
        this.markHome = teamHome.Marking;
        this.taticalAway = teamAway.Tactical;
        this.styleAway = teamAway.Style;
        this.attackAway = teamAway.Attack;
        this.markAway = teamAway.Marking;

        // MessageBox.Show(teamHome.Name);
        // foreach(Player p in this.teamHome)
        // {
        //     MessageBox.Show(p.Name);
        // }

        // MessageBox.Show(teamAway.Name);
        // foreach(Player p in this.teamAway)
        // {
        //     MessageBox.Show(p.Name);
        // }

        fillTacticals();
        resetPosition(true);
    }

    public void Draw(Graphics g, float time)
    {
        foreach(Player p in teamHome)
        {
            g.FillRectangle(Brushes.DarkBlue, nextMap[p].X - playerSize.Width/2, nextMap[p].Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawText(p.Name, Color.White, new RectangleF(nextMap[p].X - playerSize.Width/2, nextMap[p].Y - playerSize.Height/2, 100, 20));
        }

        foreach(Player p in teamAway)
        {
            g.FillRectangle(Brushes.Red, nextMap[p].X - playerSize.Width/2, nextMap[p].Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawText(p.Name, Color.White, new RectangleF(nextMap[p].X - playerSize.Width/2, nextMap[p].Y - playerSize.Height/2, 100, 20));
        }

        var dt = currentTime - (int)time;
        if (dt < 0)
        {
            playerMap = nextMap;
            nextMap = new();
            currentTime = (int)time;
        }
        float frameTime = time - currentTime;

        foreach (var player in teamHome)
        {
            var oldPosition = playerMap[player];
            var newPosition = playerMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );

            nextMap[player] = position;
        }

        foreach (var player in teamAway)
        {
            var oldPosition = playerMap[player];
            var newPosition = playerMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );

            nextMap[player] = position;
        }
    }

    private void resetPosition(bool homeStart)
    {
        int i = 0;
        foreach(Player p in teamHome)
        {
            nextMap.Add(p, home433[i]);
            i++;
        }

        i = 0;
        foreach(Player p in teamAway)
        {
            nextMap.Add(p, away433[i]);
            i++;
        }
    }

    private void simulate()
    {
        foreach (var pair in playerMap)
            nextMap.Add(pair.Key, pair.Value);
    }

    private void fillTacticals()
    {
        home433.Add(new PointF(150, 540)); //GK
        home433.Add(new PointF(300, 400)); //DCL
        home433.Add(new PointF(300, 680)); //DCR
        home433.Add(new PointF(400, 200)); //LB
        home433.Add(new PointF(400, 880)); //RB
        home433.Add(new PointF(500, 540)); //MD
        home433.Add(new PointF(600, 440)); //MCL
        home433.Add(new PointF(600, 640)); //MCR
        home433.Add(new PointF(800, 250)); //LW
        home433.Add(new PointF(800, 830)); //RW
        home433.Add(new PointF(900, 540)); //ST

        away433.Add(new PointF(1770, 540)); //GK
        away433.Add(new PointF(1620, 680)); //DCL
        away433.Add(new PointF(1620, 400)); //DCR
        away433.Add(new PointF(1520, 880)); //LB
        away433.Add(new PointF(1520, 200)); //RB
        away433.Add(new PointF(1420, 540)); //MD
        away433.Add(new PointF(1320, 640)); //MCL
        away433.Add(new PointF(1320, 440)); //MCR
        away433.Add(new PointF(1120, 830)); //LW
        away433.Add(new PointF(1120, 250)); //RW
        away433.Add(new PointF(1020, 540)); //ST
    }
}