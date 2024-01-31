using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Game;

public class Team
{
    public string Name { get; set; }
    public int Points { get; set; }
    public int Tactical { get; set; }
    public int Style { get; set; }
    public int Attack { get; set; }
    public int Marking { get; set; }
    public int GD { get; set; }
    public List<Player> TeamPlayers = new List<Player>();

    public Team(string name, int points, int gd)
    {
        this.Name = name;
        this.Points = points;
        this.GD = gd;
    }

    // public void getTeamPlayer()
    // {
    //     for(int i = 0; i < 20; i++)
    //     {
    //         this.TeamPlayers.Add(new Player(this.Name));
    //     }
    // }
}