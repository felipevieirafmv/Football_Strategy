using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Game;

public class Testes : Form
{
    public Testes()
    {
        this.Load += delegate
        {
            List<Player> players = Players.GetAllPlayers;
            List<Team> teams = Teams.GetAllTeams;
            
            foreach (Player p in players)
            {
                MessageBox.Show(p.Name);
            }

            foreach (Team team in teams)
            {
                MessageBox.Show(team.Name);
            }
        };

    }
}