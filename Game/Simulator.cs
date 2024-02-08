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
    public int ScoreHome { get; set; } = 0;
    public int ScoreAway { get; set; } = 0;

    private int currentTime = -1;
    private Dictionary<Player, PointF> playerMap = new();
    private Dictionary<Player, PointF> crrMap = new();
    private Dictionary<Player, PointF> nextMap = new();
    private Image homePlayer = Bitmap.FromFile("./img/Players/PlayerRight.png");
    private Image awayPlayer = Bitmap.FromFile("./img/Players/PlayerLeftRed.png");

    private List<PointF> home433 = new();
    private List<PointF> away433 = new();
    private SizeF playerSize= new SizeF(Screen.PrimaryScreen.Bounds.Height*0.018f, Screen.PrimaryScreen.Bounds.Height*0.018f);
    private Player ball = new Player("ball", 0);
    public List<Player> TeamHome;
    public List<Player> TeamAway;
    int taticalHome, styleHome, attackHome, markHome;
    int taticalAway, styleAway, attackAway, markAway;
    bool kicked = false;
    public Simulator(Team teamHome, Team teamAway)
    {
        this.TeamHome = teamHome.FirstTeam;
        this.TeamAway = teamAway.FirstTeam;

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

        this.awayPlayer = this.awayPlayer.GetThumbnailImage(
            (int)(Screen.PrimaryScreen.Bounds.Width*0.013f), 
            (int)(Screen.PrimaryScreen.Bounds.Height*0.038f),
            null, nint.Zero
        );
        this.homePlayer = this.homePlayer.GetThumbnailImage(
            (int)(Screen.PrimaryScreen.Bounds.Width*0.013f), 
            (int)(Screen.PrimaryScreen.Bounds.Height*0.038f),
            null, nint.Zero
        );
    }

    public void Draw(Graphics g, float time)
    {
        Font font = new Font("Copperplate Gothic Bold", Screen.PrimaryScreen.Bounds.Width*0.004f);

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
        foreach (var player in TeamHome)
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

            SizeF nameSize = g.MeasureString(player.Name, font);

            // g.FillRectangle(Brushes.Blue, position.X - playerSize.Width/2, position.Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawPlayer(homePlayer, new PointF(position.X - homePlayer.Width/2, position.Y - homePlayer.Height/2));
            g.DrawString(
                player.Name,
                font,
                Brushes.White,
                new PointF(position.X - nameSize.Width/2, position.Y + playerSize.Height)
            );
        }

        foreach (var player in TeamAway)
        {
            if (!nextMap.ContainsKey(player))
                nextMap.Add(player, playerMap[player]);
            
            var oldPosition = playerMap[player];
            var newPosition = nextMap[player];
            var position = new PointF(
                oldPosition.X * (1 - frameTime) + newPosition.X * frameTime,
                oldPosition.Y * (1 - frameTime) + newPosition.Y * frameTime
            );

            SizeF nameSize = g.MeasureString(player.Name, font);

            // g.FillRectangle(Brushes.Red, position.X - playerSize.Width/2, position.Y - playerSize.Height/2, playerSize.Width, playerSize.Height);
            Draws.DrawPlayer(awayPlayer, new PointF(position.X - homePlayer.Width/2, position.Y - homePlayer.Height/2));
            g.DrawString(
                player.Name,
                font,
                Brushes.White,
                new PointF(position.X - nameSize.Width/2, position.Y + playerSize.Height)
            );
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
        foreach(Player p in TeamHome)
        {
            nextMap.Add(p, home433[i]);
            i++;
        }

        i = 0;
        foreach(Player p in TeamAway)
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

        float startX = Screen.PrimaryScreen.Bounds.Width * 0.047f;
        float endX = Screen.PrimaryScreen.Bounds.Width * 0.951f;
        float startY = Screen.PrimaryScreen.Bounds.Height * 0.237f;
        float endY = Screen.PrimaryScreen.Bounds.Height * 0.947f;

        float yTopGoal = Screen.PrimaryScreen.Bounds.Height*0.537f;
        float yBottomGoal = Screen.PrimaryScreen.Bounds.Height*0.652f;
        
        var homeGoal = new RectangleF(
            Screen.PrimaryScreen.Bounds.Width*0.031f, 
            yTopGoal,
            Screen.PrimaryScreen.Bounds.Width*0.016f, 
            Screen.PrimaryScreen.Bounds.Height*0.115f
        );
        
        if(homeGoal.Contains(ballInGame.Value.X, ballInGame.Value.Y))
        {
            resetPosition(true);
            return;
        }

        var awayGoal = new RectangleF(
            endX, 
            yTopGoal, 
            Screen.PrimaryScreen.Bounds.Width*0.016f, 
            Screen.PrimaryScreen.Bounds.Height*0.115f
        );

        if(awayGoal.Contains(ballInGame.Value.X, ballInGame.Value.Y))
        {
            resetPosition(false);
            return;
        }

        if (ballInGame.Value.X > endX)
        {
            resetPosition(false);
            return;
        }

        if (ballInGame.Value.X < Screen.PrimaryScreen.Bounds.Width*0.053f)
        {
            resetPosition(true);
            return;
        }
        
        var players = playerMap
            .Where(p => p.Key != ballInGame.Key);
        
        var playerWithBall = players
            .OrderBy(p => -p.Key.IntercepionsAbility +
                (p.Value.X - ballPosition.X) * (p.Value.X - ballPosition.X) + 
                (p.Value.Y - ballPosition.Y) * (p.Value.Y - ballPosition.Y))
            .FirstOrDefault()
            .Key;

        if(playerWithBall.Team == TeamHome[0].Team && tryKick(true))
            return;

        if(playerWithBall.Team == TeamAway[0].Team && tryKick(false))
            return;

        bool tryKick(bool home)
        {
            float xGoal = home ? endX : startX;

            var dx = xGoal - playerMap[playerWithBall].X;
            var dy = Screen.PrimaryScreen.Bounds.Height*0.592f - playerMap[playerWithBall].Y;
            var dist = MathF.Sqrt(dx * dx + dy * dy);

            var defaultDist = MathF.Sqrt(
                Screen.PrimaryScreen.Bounds.Width * Screen.PrimaryScreen.Bounds.Width + 
                Screen.PrimaryScreen.Bounds.Height * Screen.PrimaryScreen.Bounds.Height
            );

            if (dist > defaultDist*0.227f)
                return false;
            
            if (dist < defaultDist*0.227f && dist > defaultDist*0.136f && random.NextSingle() < 0.8f)
                return false;

            int keeper = TeamAway[0].GoalKeeperAbility;
            int kicker = playerWithBall.KickingAblity;
            int gap = (kicker - keeper) / 5 - (dist > defaultDist*0.136f ? 3 : 2);
            float goalChance = 1 / (1 + MathF.Exp(-gap));
            var isGoal = goalChance > random.NextSingle();

            float missedGoal = random.NextSingle() > 0.5 ? 
                random.Next((int)startY, (int)(Screen.PrimaryScreen.Bounds.Height*0.462f)) : 
                random.Next((int)(Screen.PrimaryScreen.Bounds.Height*0.703f), (int)endY);

            if (isGoal)
            {
                nextMap.Add(ballInGame.Key, new PointF(xGoal + (Screen.PrimaryScreen.Bounds.Width*0.006f), random.Next((int)yTopGoal, (int)yBottomGoal)));
                if(home) ScoreHome++; else ScoreAway++;
            }
            else nextMap.Add(ballInGame.Key, new PointF(xGoal + (Screen.PrimaryScreen.Bounds.Width*0.006f), missedGoal));
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

        if(playerOptions[0].Key.Team == TeamHome[0].Team)
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

        var defaultDist = MathF.Sqrt(
            Screen.PrimaryScreen.Bounds.Width * Screen.PrimaryScreen.Bounds.Width + 
            Screen.PrimaryScreen.Bounds.Height * Screen.PrimaryScreen.Bounds.Height
        );

        if(!kicked)
        {
            var ballDx = playerMap[ballInGame.Key].X - playerMap[playerWithBall].X;
            var ballDy = playerMap[ballInGame.Key].Y - playerMap[playerWithBall].Y;
            var dist = Math.Sqrt(ballDx * ballDx + ballDy * ballDy);

            float standDevX = Screen.PrimaryScreen.Bounds.Width*0.052f;
            float standDevY = Screen.PrimaryScreen.Bounds.Height*0.092f;

            float randX = playerChoosed.Value.X + random.Next((int)(standDevX*-1),(int)standDevX);
            
            if(randX > endX)
                randX = endX;
            if(randX < startX)
                randX = startX;

            float randY = playerChoosed.Value.Y + random.Next((int)(standDevY*-1),(int)standDevY);
            if(randY > endY)
                randY = endY;
            if(randY < startY)
                randY = startY;

            if (dist > defaultDist*0.005f)
            {
                nextMap[playerWithBall] = ballInGame.Value;
                nextMap[ballInGame.Key] = ballInGame.Value;
            }
            else if (random.Next(1, 100) < playerWithBall.PassingAbility)
                nextMap.Add(ballInGame.Key, playerChoosed.Value);
            else
                nextMap.Add(ballInGame.Key, new PointF(randX, randY));
        }
            
        foreach (var pair in otherPlayers)
        {
            if (nextMap.ContainsKey(pair.Key))
                continue;
            
            var player = pair.Key;
            var position = pair.Value;
            bool isHome = TeamHome[0].Team == pair.Key.Team;

            var ballDx = playerMap[key: ballInGame.Key].X - playerMap[player].X;
            var ballDy = playerMap[ballInGame.Key].Y - playerMap[key: player].Y;
            var dist = Math.Sqrt(ballDx * ballDx + ballDy * ballDy);

            if (dist < defaultDist*0.045f)
            {
                nextMap[player] = ballInGame.Value;
                continue;
            }

            int index = TeamHome.FindIndex(p => p == player);
            if (index == -1) 
                index = TeamAway.FindIndex(match: p => p == player);
            
            var nextPosition = new PointF();
            var style = isHome ? styleHome : styleAway;
            float ep = isHome ? 
                Screen.PrimaryScreen.Bounds.Width*0.052f + Screen.PrimaryScreen.Bounds.Width*0.885f * (index + 2 - style) / 11 : 
                Screen.PrimaryScreen.Bounds.Width*0.937f - Screen.PrimaryScreen.Bounds.Width*0.885f * (index + 2 - style) / 11;
            float error = ep - position.X;
            float dx = (random.NextSingle() - 0.1f) * error / 10;

            float yDeviation = Screen.PrimaryScreen.Bounds.Height * 0.013f;

            if(isHome)
            {
                if(pair.Key == TeamHome[0])
                    nextPosition = new PointF(position.X, position.Y + random.Next((int)(yDeviation*-1),(int)yDeviation));
                else nextPosition = new PointF(position.X + dx, position.Y + random.Next((int)(yDeviation*-1),(int)yDeviation));
            }
            else
            {
                if(pair.Key == TeamAway[0])
                    nextPosition = new PointF(position.X, position.Y + random.Next((int)(yDeviation*-1),(int)yDeviation));
                else nextPosition = new PointF(position.X + dx, position.Y + random.Next((int)(yDeviation*-1),(int)yDeviation));
            }

            nextMap.Add(player, nextPosition);
        }
    }

    private void fillTacticals()
    {
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.08f, Screen.PrimaryScreen.Bounds.Height*0.581f)); //GK
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.147f, Screen.PrimaryScreen.Bounds.Height*0.462f)); //DCL
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.136f, Screen.PrimaryScreen.Bounds.Height*0.683f)); //DCR
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.179f, Screen.PrimaryScreen.Bounds.Height*0.31f)); //LB
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.158f, Screen.PrimaryScreen.Bounds.Height*0.835f)); //RB
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.26f, Screen.PrimaryScreen.Bounds.Height*0.581f)); //MD
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.314f, Screen.PrimaryScreen.Bounds.Height*0.488f)); //MCL
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.314f, Screen.PrimaryScreen.Bounds.Height*0.683f)); //MCR
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.416f, Screen.PrimaryScreen.Bounds.Height*0.31f)); //LW
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.416f, Screen.PrimaryScreen.Bounds.Height*0.835f)); //RW
        home433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.468f, Screen.PrimaryScreen.Bounds.Height*0.581f)); //ST

        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.914f, Screen.PrimaryScreen.Bounds.Height*0.581f)); //GK
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.851f, Screen.PrimaryScreen.Bounds.Height*0.462f)); //DCL
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.861f, Screen.PrimaryScreen.Bounds.Height*0.683f)); //DCR
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.82f, Screen.PrimaryScreen.Bounds.Height*0.31f)); //LB
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.841f, Screen.PrimaryScreen.Bounds.Height*0.835f)); //RB
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.733f, Screen.PrimaryScreen.Bounds.Height*0.581f)); //MD
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.68f, Screen.PrimaryScreen.Bounds.Height*0.488f)); //MCL
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.68f, Screen.PrimaryScreen.Bounds.Height*0.683f)); //MCR
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.583f, Screen.PrimaryScreen.Bounds.Height*0.31f)); //LW
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.583f, Screen.PrimaryScreen.Bounds.Height*0.835f)); //RW
        away433.Add(new PointF(Screen.PrimaryScreen.Bounds.Width*0.531f, Screen.PrimaryScreen.Bounds.Height*0.581f)); //ST
    }
}
