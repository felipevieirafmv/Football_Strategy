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
    public int Intercepions { get; set; }

    public Player(string team)
    {
        Random random = Random.Shared;
        string[] firstName = File.ReadAllLines("./Game/firstName.txt");
        string[] lastName = File.ReadAllLines("./Game/lastName.txt");
        this.Name = firstName[random.Next(firstName.Length)] + " " + lastName[random.Next(lastName.Length)];
        this.Team = team;
        this.OverAll = random.Next(40, 61);

        this.PassingAbility = random.Next(40, 61);
        this.KickingAblity = random.Next(40, 61);
        this.GoalKeeperAbility = random.Next(40, 61);
        this.Intercepions = random.Next(40, 61);
    }
}