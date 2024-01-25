using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Game;

public class GameLogic
{
    public List<Player> TeamGame { get; set; }
    private List<Team> teams = Teams.GetAllTeams;
    private List<Team[]> confrontations = new List<Team[]>();

    public GameLogic(bool newGame, string chooseTeam)
    {
        if(newGame)
        {
            List<Player> players = Players.GetAllPlayers;

            foreach (Player p in players)
            {
                if(p.Team == chooseTeam)
                    TeamGame.Add(p);
            }

        }
        else
        {
            //utilizar o save ja existente
        }
    }

    public void CreateConfrontations()
    {
        StreamWriter sw = new StreamWriter("./Game/confrontations.txt");

        ChampionshipGenerator cg = new ChampionshipGenerator();

        foreach(Team t in teams)
        {
            cg.Add(t);
        }

        Dictionary<Team, Team[]> dict = cg.Generate();

        Team[,] primeiroTurno = new Team[190, 2];

        Random random = Random.Shared;

        int indexArray = 0;
        for(int i = 0; i < 19; i++)
        {
            string result2 = "";
            // sw.WriteLine($"Rodada {i+1}");
            foreach(var pair in dict)
            {
                if(result2.Contains(pair.Key.Name) || result2.Contains(pair.Value[i].Name))
                    continue;
                if(random.Next(10) % 2 == 0)
                {
                    primeiroTurno[indexArray, 0] = pair.Key;
                    primeiroTurno[indexArray, 1] = pair.Value[i];
                }
                else
                {
                    primeiroTurno[indexArray, 1] = pair.Key;
                    primeiroTurno[indexArray, 0] = pair.Value[i];
                }
                result2 += pair.Key.Name;
                result2 += pair.Value[i].Name;
                indexArray++;
            }
        }
        
        indexArray = 0;
        for(int i = 0; i < 19; i++)
        {
            sw.WriteLine($"Rodada {i+1}");
            for(int j = 0; j < 10; j++)
            {
                sw.WriteLine(primeiroTurno[indexArray, 0].Name + "," + primeiroTurno[indexArray, 1].Name);
                indexArray++;
            }
            sw.WriteLine();
        }

        indexArray = 0;
        for(int i = 0; i < 19; i++)
        {
            sw.WriteLine($"Rodada {i+20}");
            for(int j = 0; j < 10; j++)
            {
                sw.WriteLine(primeiroTurno[indexArray, 1].Name + "," + primeiroTurno[indexArray, 0].Name);
                indexArray++;
            }
            sw.WriteLine();
        }

        sw.Close();
    }

    public void ResetTeams()
    {
        foreach(Team t in teams)
        {
            t.Tactical = 1;
            t.Style = 1;
            t.Attack = 1;
            t.Points = 0;
        }
    }

    public void CreateMatch()
    {
        //pegar os dois times que vao se enfrentar e simular a partida (ira precisar de mais funcoes para funcionar, nao descarto a necessidade de uma nova classe)
    }
}