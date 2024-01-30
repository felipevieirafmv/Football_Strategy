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
    public Simulator(
        List<Player> teamHome, int taticalHome, int styleHome, int attackHome, int markHome,
        List<Player> teamAway, int taticalAway, int styleAway, int attackAway, int markAway
    )
    {
        this.teamHome = teamHome;
        this.teamAway = teamAway;

        this.taticalHome = taticalHome;
        this.styleHome = styleHome;
        this.attackHome = attackHome;
        this.markHome = markHome;
        this.taticalAway = taticalAway;
        this.styleAway = styleAway;
        this.attackAway = attackAway;
        this.markAway = markAway;
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