using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game;

public class Simulator
{
    public int ScoreHome { get; set; }
    public int ScoreAway { get; set; }

    private int currentTime = -1;
    private Dictionary<Player, PointF> playerMap = new();
    private Dictionary<Player, PointF> nextMap = new();
    private Player ball = null;
    private List<Player> teamHome;
    private List<Player> teamAway;
    int taticalHome, styleHome, attackHome, markHome;
    int taticalAway, styleAway, attackAway, markAway;
    public Simulator(Team teamHome, Team teamAway)
    {
        this.teamHome = teamHome.TeamPlayers;
        this.teamAway = teamAway.TeamPlayers;

        this.taticalHome = teamHome.Tactical;
        this.styleHome = teamHome.Style;
        this.attackHome = teamHome.Attack;
        this.markHome = teamHome.Marking;
        this.taticalAway = teamAway.Tactical;
        this.styleAway = teamAway.Style;
        this.attackAway = teamAway.Attack;
        this.markAway = teamAway.Marking;
        // TODO
        resetPosition(true);
    }

    public void Draw(Graphics g, float time)
    {
        var dt = currentTime - (int)time;
        if (dt < 0)
        {
            playerMap = nextMap;
            nextMap = new();
            simulate();
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

    }

    private void simulate()
    {
        foreach (var pair in playerMap)
            nextMap.Add(pair.Key, pair.Value);
    }
}