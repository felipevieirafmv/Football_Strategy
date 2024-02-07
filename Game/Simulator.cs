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
    private Dictionary<Player, PointF> crrMap = new();
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
    bool kicked = false;
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

        fillTacticals();
        resetPosition(true);
    }

    public void Draw(Graphics g, float time)
    {
        var dt = currentTime - (int)time;
        if (dt < 0)
        {
            playerMap = nextMap;
            nextMap = new Dictionary<Player, PointF>();
            simulate();
            currentTime = (int)time;
        }
        float frameTime = time - currentTime;

        crrMap.Clear();
        foreach (var player in teamHome)
        {
            if (!nextMap.ContainsKey(player))
                nextMap.Add(player, playerMap[player]);

            var oldPosition = playerMap[player];
            var newPosition = nextMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );
            crrMap[player] = position;

            // g.FillRectangle(Brushes.Blue, position.X - playerSize.Width/2, position.Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawPlayer(homePlayer, new PointF(position.X - homePlayer.Width/2, position.Y - homePlayer.Height/2));
            Draws.DrawText(player.Name, Color.White, new RectangleF(position.X - playerSize.Width/2, position.Y - playerSize.Height/2, 100, 20));
        }

        foreach (var player in teamAway)
        {
            if (!nextMap.ContainsKey(player))
                nextMap.Add(player, playerMap[player]);
            
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

        if (!nextMap.ContainsKey(ball))
            return;
        if (!playerMap.ContainsKey(ball))
            return;
        
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
        }

        i = 0;
        foreach(Player p in teamAway)
        {
            nextMap.Add(p, away433[i]);
            i++;
        }

        nextMap[ball] = 
            homeStart ? 
            home433[10] : 
            away433[10];

        kicked = false;
    }

    private void simulate()
    {
        Random random = Random.Shared;

        var ballInGame = playerMap
            .FirstOrDefault(p => p.Key.Team == "ball");
        var ballPosition = ballInGame.Value;

        int endField = 1827;
        
        var homeGoal = new RectangleF(70, 500, 32, 122);
        if(homeGoal.Contains(ballInGame.Value.X, ballInGame.Value.Y))
        {
            resetPosition(true);
            return;
        }

        var awayGoal = new RectangleF(endField, 500, 32, 122);
        if(awayGoal.Contains(ballInGame.Value.X, ballInGame.Value.Y))
        {
            resetPosition(false);
            return;
        }

        if (ballInGame.Value.X > endField)
        {
            resetPosition(false);
            return;
        }

        if (ballInGame.Value.X < 102)
        {
            resetPosition(true);
            return;
        }
        
        var players = playerMap
            .Where(p => p.Key != ballInGame.Key);
        
        var playerWithBall = players
            .OrderBy(p => -p.Key.Intercepions +
                (p.Value.X - ballPosition.X) * (p.Value.X - ballPosition.X) + 
                (p.Value.Y - ballPosition.Y) * (p.Value.Y - ballPosition.Y))
            .FirstOrDefault()
            .Key;

        // MessageBox.Show(Math.Sqrt(((1817 - playerMap[playerWithBall].X) * (1817 - playerMap[playerWithBall].X)) + ((639 - playerMap[playerWithBall].Y) * (639 - playerMap[playerWithBall].Y))).ToString());
        if(playerWithBall.Team == teamHome[0].Team && tryKick())
            return;

        if(playerWithBall.Team == teamAway[0].Team)
        {
            if(Math.Sqrt(((102 - playerMap[playerWithBall].X) * (102 - playerMap[playerWithBall].X)) + ((639 - playerMap[playerWithBall].Y) * (639 - playerMap[playerWithBall].Y))) < 300)
            {
                nextMap.Add(ballInGame.Key, new PointF(95, random.Next(578, 700)));
                return;
            }
        }

        bool tryKick()
        {
            var dx = 1817 - playerMap[playerWithBall].X;
            var dy = 639 - playerMap[playerWithBall].Y;
            var dist = MathF.Sqrt(dx * dx + dy * dy);

            if (dist > 500)
                return false;
            
            if (dist < 500 && dist > 300 && random.NextSingle() < 0.8f)
                return false;

            int keeper = teamAway[0].GoalKeeperAbility;
            int kicker = playerWithBall.KickingAblity;
            int gap = (kicker - keeper) / 5 - (dist > 300 ? 3 : 2);
            float goalChance = 1 / (1 + MathF.Exp(-gap));
            var isGoal = goalChance > random.NextSingle();

            if (isGoal)
                nextMap.Add(key: ballInGame.Key, new PointF(endField + 12, random.Next(578, 700)));
            else nextMap.Add(key: ballInGame.Key, new PointF(endField + 12, random.Next(800, 1000)));
            kicked = true;
            return isGoal;
        }
        
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

        otherPlayers = players
            .Where(p => p.Key != playerWithBall);

        nextMap.Add(playerChoosed.Key, playerChoosed.Value);

        if(!kicked)
        {
            var ballDx = playerMap[key: ballInGame.Key].X - playerMap[playerWithBall].X;
            var ballDy = playerMap[ballInGame.Key].Y - playerMap[playerWithBall].Y;
            var dist = Math.Sqrt(ballDx * ballDx + ballDy * ballDy);

            if (dist > 50)
            {
                nextMap[playerWithBall] = ballInGame.Value;
                nextMap[ballInGame.Key] = ballInGame.Value;
            }
            else if (random.Next(1, 100) < playerWithBall.PassingAbility)
                nextMap.Add(ballInGame.Key, playerChoosed.Value);
            else
                nextMap.Add(ballInGame.Key, new PointF(
                    playerChoosed.Value.X + random.Next(-100,100), 
                    playerChoosed.Value.Y + random.Next(-100,100)
                    ));
        }
            
        foreach (var pair in otherPlayers)
        {
            if (nextMap.ContainsKey(pair.Key))
                continue;
            
            var player = pair.Key;
            var position = pair.Value;
            bool isHome = teamHome[0].Team == pair.Key.Team;

            var ballDx = playerMap[key: ballInGame.Key].X - playerMap[player].X;
            var ballDy = playerMap[ballInGame.Key].Y - playerMap[key: player].Y;
            var dist = Math.Sqrt(ballDx * ballDx + ballDy * ballDy);

            if (dist < 100)
            {
                nextMap[player] = ballInGame.Value;
                continue;
            }


            int index = teamHome.FindIndex(p => p == player);
            if (index == -1) 
                index = teamAway.FindIndex(match: p => p == player);
            
            var nextPosition = new PointF();
            var style = isHome ? styleHome : styleAway;
            int ep = isHome ? 100 + 1700 * (index + 2 - style) / 11 : 1800 - 1700 * (index + 2 - style) / 11;
            float error = ep - position.X;
            float dx = (random.NextSingle() - 0.1f) * error / 10;

            if(isHome)
            {
                if(pair.Key == teamHome[0])
                    nextPosition = new PointF(position.X, position.Y + random.Next(minValue: -15,16));
                else nextPosition = new PointF(position.X + dx, position.Y + random.Next(minValue: -15,16));
            }
            else
            {
                if(pair.Key == teamAway[0])
                    nextPosition = new PointF(position.X, position.Y + random.Next(minValue: -15,16));
                else nextPosition = new PointF(position.X + dx, position.Y + random.Next(minValue: -15,16));
            }

            nextMap.Add(player, nextPosition);
        }
    }

    private void fillTacticals()
    {
        home433.Add(new PointF(154, 628)); //GK
        home433.Add(new PointF(284, 498)); //DCL
        home433.Add(new PointF(264, 738)); //DCR
        home433.Add(new PointF(344, 335)); //LB
        home433.Add(new PointF(304, 902)); //RB
        home433.Add(new PointF(500, 628)); //MD
        home433.Add(new PointF(603, 528)); //MCL
        home433.Add(new PointF(597, 728)); //MCR
        home433.Add(new PointF(800, 335)); //LW
        home433.Add(new PointF(800, 902)); //RW
        home433.Add(new PointF(900, 628)); //ST

        away433.Add(new PointF(1755, 628)); //GK
        away433.Add(new PointF(1635, 498)); //DCL
        away433.Add(new PointF(1655, 738)); //DCR
        away433.Add(new PointF(1575, 335)); //LB
        away433.Add(new PointF(1615, 902)); //RB
        away433.Add(new PointF(1409, 628)); //MD
        away433.Add(new PointF(1307, 528)); //MCL
        away433.Add(new PointF(1312, 728)); //MCR
        away433.Add(new PointF(1120, 335)); //LW
        away433.Add(new PointF(1120, 902)); //RW
        away433.Add(new PointF(1020, 628)); //ST
    }
}