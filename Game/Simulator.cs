using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Game;

public class Simulator
{
    public int ScoreHome { get; set; }
    public int ScoreAway { get; set; }

    private int currentTime = -1;
    private Dictionary<Player, PointF> playerMap = new();
    private Dictionary<Player, PointF> nextMap = new();
    private List<PointF> field433 = new();
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

        MessageBox.Show(teamHome.Name);
        foreach(Player p in this.teamHome)
        {
            MessageBox.Show(p.Name);
        }

        MessageBox.Show(teamAway.Name);
        foreach(Player p in this.teamAway)
        {
            MessageBox.Show(p.Name);
        }

        resetPosition(true);
    }

    public void Draw(Graphics g, float time)
    {
        var dt = currentTime - (int)time;
        if (dt < 0)
        {
            playerMap = nextMap;
            nextMap = new();
            simulate(g);
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


        }

        foreach (var player in teamAway)
        {
            var oldPosition = playerMap[player];
            var newPosition = playerMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );

            
        }
    }

    private void resetPosition(bool homeStart)
    {
        int i = 0;
        foreach(Player p in teamHome)
        {
            playerMap.Add(p, field433[i]);
            i++;
        }
    }

    private void simulate(Graphics g)
    {
        foreach (var pair in playerMap)
            nextMap.Add(pair.Key, pair.Value);
        
        foreach(Player p in teamHome)
        {
            g.FillRectangle(Brushes.DarkBlue, 50, 50, playerMap[p].X, playerMap[p].Y);
        }
    }

    private void fillTacticals()
    {
        field433.Add(new PointF(150, 500));
        field433.Add(new PointF(300, 350));
        field433.Add(new PointF(300, 550));
        field433.Add(new PointF(300, 200));
        field433.Add(new PointF(300, 800));
        field433.Add(new PointF(800, 500));
        field433.Add(new PointF(900, 600));
        field433.Add(new PointF(300, 500));
        field433.Add(new PointF(300, 500));
        field433.Add(new PointF(300, 500));
        field433.Add(new PointF(300, 500));
        field433.Add(new PointF(300, 500));
    }
}