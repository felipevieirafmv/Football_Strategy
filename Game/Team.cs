using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Game;

public class Team
{
    public string Name { get; set; }
    public int Points { get; set; }
    public int Tactical { get; set; } = 1;
    public int Style { get; set; } = 0;
    public int Attack { get; set; } = 1;
    public int Marking { get; set; } = 1;
    public int Diff { get; set; } //Goals Difference
    public List<Player> Squad = new List<Player>();
    public List<Player> FirstTeam = new List<Player>();

    public Team(string name, int points, int diff)
    {
        Random random = new Random();

        int overall = random.Next(80, 121);

        this.Name = name;
        this.Points = points;
        this.Diff = diff;

        for(int i = 0; i < 20; i++)
        {
            Squad.Add(new Player(this.Name, overall));
        }

        for(int i = 0; i < 11; i++)
        {
            FirstTeam.Add(Squad[i]);
        }


        // this.Style = random.Next(0, 3);
    }
}