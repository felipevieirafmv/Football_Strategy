using System;
using System.Drawing;
using System.IO;

namespace Game;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Team { get; set; }
    public int OverAll { get; set; }
    public string Position { get; set; }
    public SizeF Tamanho = new SizeF(450, 40);
    public PointF Location { get; set; }

    public int PassingAbility { get; set; }
    public int KickingAblity { get; set; }
    public int GoalKeeperAbility { get; set; }
    public int IntercepionsAbility { get; set; }

    public Player(string team, int overAll)
    {
        Random random = new Random();
        string[] firstName = File.ReadAllLines("./txt/firstName.txt");
        string[] lastName = File.ReadAllLines("./txt/lastName.txt");
        this.Name = firstName[random.Next(firstName.Length)] + " " + lastName[random.Next(lastName.Length)];
        this.Team = team;
        this.OverAll = overAll;

        switch (random.Next(0, 7))
        {
            case 0:
                this.IntercepionsAbility = random.Next((overAll-20)/2, overAll/2);
                this.PassingAbility = random.Next((overAll-20)/6, overAll/6);
                this.GoalKeeperAbility = random.Next((overAll-20)/6, overAll/6);
                this.KickingAblity = overAll - GoalKeeperAbility - IntercepionsAbility - PassingAbility;
                break;
            case 1:
                this.IntercepionsAbility = random.Next((overAll-20)/2, overAll/2);
                this.PassingAbility = random.Next((overAll-20)/6, overAll/6);
                this.GoalKeeperAbility = random.Next((overAll-20)/6, overAll/6);
                this.KickingAblity = overAll - GoalKeeperAbility - IntercepionsAbility - PassingAbility;
                break;
            case 2:
                this.PassingAbility = random.Next((overAll-20)/2, overAll/2);
                this.KickingAblity = random.Next((overAll-20)/6, overAll/6);
                this.IntercepionsAbility = random.Next((overAll-20)/6, overAll/6);
                this.GoalKeeperAbility = overAll - KickingAblity - IntercepionsAbility - PassingAbility;
                break;
            case 3:
                this.PassingAbility = random.Next((overAll-20)/2, overAll/2);
                this.KickingAblity = random.Next((overAll-20)/6, overAll/6);
                this.IntercepionsAbility = random.Next((overAll-20)/6, overAll/6);
                this.GoalKeeperAbility = overAll - KickingAblity - IntercepionsAbility - PassingAbility;
                break;
            case 4:
                this.KickingAblity = random.Next((overAll-20)/2, overAll/2);
                this.PassingAbility = random.Next((overAll-20)/6, overAll/6);
                this.IntercepionsAbility = random.Next((overAll-20)/6, overAll/6);
                this.GoalKeeperAbility = overAll - KickingAblity - IntercepionsAbility - PassingAbility;
                break;
            case 5:
                this.KickingAblity = random.Next((overAll-20)/2, overAll/2);
                this.PassingAbility = random.Next((overAll-20)/6, overAll/6);
                this.IntercepionsAbility = random.Next((overAll-20)/6, overAll/6);
                this.GoalKeeperAbility = overAll - KickingAblity - IntercepionsAbility - PassingAbility;
                break;
            default:
                this.GoalKeeperAbility = random.Next((overAll-20)/2, overAll/2);
                this.IntercepionsAbility = random.Next((overAll-20)/6, overAll/6);
                this.PassingAbility = random.Next((overAll-20)/6, overAll/6);
                this.KickingAblity = overAll - GoalKeeperAbility - IntercepionsAbility - PassingAbility;
            break;
        }
    }
}