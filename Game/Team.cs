using System;
using System.Collections.Generic;

namespace Game;

public class Team
{
    public string Name { get; set; }
    public int Points { get; set; }
    public int Tactical { get; set; }
    public int Style { get; set; }
    public int Attack { get; set; }
    public List<Player> TeamPlayers { get; set; }

    public Team(string name, int points)
    {
        this.Name = name;
        this.Points = points;
    }

    public void getTeamPlayer()
    {
        List<Player> allPlayers = Players.GetAllPlayers;

        foreach(Player p in allPlayers)
        {
            if(p.Team == this.Name)
                this.TeamPlayers.Add(p);
        }
    }
}