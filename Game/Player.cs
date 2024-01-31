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

    public Player(string team)
    {
        Random random = Random.Shared;
        string[] firstName = File.ReadAllLines("./Game/firstName.txt");
        string[] lastName = File.ReadAllLines("./Game/lastName.txt");
        this.Name = firstName[random.Next(firstName.Length)] + " " + lastName[random.Next(lastName.Length)];
        this.Team = team;
    }
}