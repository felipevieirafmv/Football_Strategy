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
    public int GD { get; set; } //Goals Difference
    public List<Player> Squad = new List<Player>();
    public List<Player> FirstTeam = new List<Player>();

    public Team(string name, int points, int gd)
    {
        this.Name = name;
        this.Points = points;
        this.GD = gd;

        for(int i = 0; i < 20; i++)
        {
            Squad.Add(new Player(this.Name));
        }

        for(int i = 0; i < 11; i++)
        {
            FirstTeam.Add(Squad[i]);
        }

        Random random = new Random();

        // this.Style = random.Next(0, 3);
    }

    // public void getTeamPlayer()
    // {
    //     for(int i = 0; i < 20; i++)
    //     {
    //         this.TeamPlayers.Add(new Player(this.Name));
    //     }
    // }
}