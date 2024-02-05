using System;
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
    private Image homePlayer = Bitmap.FromFile("./img/Players/PlayerRight.png");
    private Image awayPlayer = Bitmap.FromFile("./img/Players/PlayerLeftRed.png");

    private List<PointF> home433 = new();
    private List<PointF> away433 = new();
    private SizeF playerSize= new SizeF(20, 20);
    private Player ball = new Player("ball");
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

        MessageBox.Show(teamHome.Style.ToString());
        fillTacticals();
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
            var newPosition = nextMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );

            // g.FillRectangle(Brushes.Blue, position.X - playerSize.Width/2, position.Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawPlayer(homePlayer, new PointF(position.X - homePlayer.Width/2, position.Y - homePlayer.Height/2));
            Draws.DrawText(player.Name, Color.White, new RectangleF(position.X - playerSize.Width/2, position.Y - playerSize.Height/2, 100, 20));
        }

        foreach (var player in teamAway)
        {
            var oldPosition = playerMap[player];
            var newPosition = nextMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );

            // g.FillRectangle(Brushes.Red, position.X - playerSize.Width/2, position.Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawPlayer(awayPlayer, new PointF(position.X - homePlayer.Width/2, position.Y - homePlayer.Height/2));
            Draws.DrawText(player.Name, Color.White, new RectangleF(position.X - playerSize.Width/2, position.Y - playerSize.Height/2, 100, 20));
        }

        var oldPositionball = playerMap[ball];
        var newPositionball = nextMap[ball];
        var positionball = new PointF(
            oldPositionball.X * (1 - frameTime) + newPositionball.X * frameTime,
            oldPositionball.Y * (1 - frameTime) + newPositionball.Y * frameTime
        );
        g.FillEllipse(Brushes.FloralWhite, new RectangleF(positionball.X - 5, positionball.Y - 5, 10, 10));
    }

    private void resetPosition(bool homeStart)
    {
        int i = 0;
        foreach(Player p in teamHome)
        {
            nextMap.Add(p, home433[i]);
            i++;

            if (i == 11)
                nextMap[ball] = home433[i - 1];
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
        Random random = new Random();

        var ball = playerMap
            .FirstOrDefault(p => p.Key.Team == "ball");
        var ballPosition = ball.Value;
        
        var players = playerMap
            .Where(p => p.Key != ball.Key);
        
        var playerWithBall = players
            .OrderBy(p => (p.Value.X - ballPosition.X) * (p.Value.X - ballPosition.X) + (p.Value.Y - ballPosition.Y) * (p.Value.Y - ballPosition.Y))
            .FirstOrDefault()
            .Key;
        
        var otherPlayers = players
            .Where(p => p.Key != playerWithBall);
        
        var playersToPass = otherPlayers
            .Where(p => p.Key.Team == playerWithBall.Team)
            .OrderBy(p => (p.Value.X - ballPosition.X) * (p.Value.X - ballPosition.X) + (p.Value.Y - ballPosition.Y) * (p.Value.Y - ballPosition.Y))
            .Take(5);

        var playersRandom = playersToPass
            .OrderBy(p => random.Next()).ToList();
        
        var playerOptions = playersRandom.Take(2).ToList();

        var playerChoosed = new KeyValuePair<Player, System.Drawing.PointF>();

        if(playerOptions[0].Key.Team == teamHome[0].Team)
        {
            playerChoosed = playerOptions
                .OrderByDescending(p => p.Value.X)
                .FirstOrDefault();
        }
        else
        {
            playerChoosed = playerOptions
                .OrderBy(p => p.Value.X)
                .FirstOrDefault();
        }

        playerWithBall = playerChoosed.Key;

        otherPlayers = players
            .Where(p => p.Key != playerWithBall);

        nextMap.Add(playerChoosed.Key, playerChoosed.Value);
        if(random.Next(1, 100) < playerChoosed.Key.PassingAbility)
            nextMap.Add(ball.Key, playerChoosed.Value);
        else
            nextMap.Add(ball.Key, new PointF(playerChoosed.Value.X + random.Next(1,100), playerChoosed.Value.Y + random.Next(1,100)));
            

        foreach (var pair in otherPlayers)
        {
            var player = pair.Key;
            var position = pair.Value;

            // Aqui

            var nextPosition = new PointF();

            if(pair.Key.Team == teamHome[0].Team)
            {
                if(pair.Value == home433[0])
                    nextPosition = new PointF(position.X, position.Y);
                else
                    {
                        switch (styleHome)
                        {
                            case 0:
                                nextPosition = new PointF(position.X + random.Next(0,0), position.Y);
                                break;
                            case 1:
                                nextPosition = new PointF(position.X + random.Next(0,0), position.Y);
                                break;
                            case 2:
                                nextPosition = new PointF(position.X + random.Next(0,0), position.Y);
                                break;
                            default:
                                break;
                        }
                    }
            }
            else
            {
                if(pair.Value == away433[0])
                    nextPosition = new PointF(position.X, position.Y);
                else
                {
                        switch (styleAway)
                        {
                            case 0:
                                nextPosition = new PointF(position.X - random.Next(0,0), position.Y);
                                break;
                            case 1:
                                nextPosition = new PointF(position.X - random.Next(0,0), position.Y);
                                break;
                            case 2:
                                nextPosition = new PointF(position.X - random.Next(0,0), position.Y);
                                break;
                            default:
                                break;
                        }
                    }
            }

            nextMap.Add(player, nextPosition);
        }
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