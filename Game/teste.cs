using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace Game;

public class Testes : Form
{
    public List<Player> players = Players.GetAllPlayers;
    public List<Team> teams = Teams.GetAllTeams;
    public Testes()
    {
        StartGame logica = new StartGame(true, "Athletico");
    }

    public void CreatePlayers()
    {
        StreamWriter sw = new StreamWriter("./Game/players.txt");

        foreach(Team t in this.teams)
        {
            for(int i = 0; i < 20; i++)
            {
                // sw.WriteLine(i + "," + $"Jogador{i}" + "," + t.Name + "," + "99" + "," + "Position");
                sw.WriteLine($"{i},Jogador{i},{t.Name},99,Position");
            }
        }

        sw.Close();
    }
}