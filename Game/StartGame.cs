using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Game;

public class StartGame
{
    public List<Player> TeamGame = new List<Player>();
    private List<Team> teams = Teams.GetAllTeams;
    private List<Team[]> confrontations = new List<Team[]>();

    public StartGame(bool newGame, string chooseTeam)
    {
        if(newGame)
        {
            List<Player> players = Players.GetAllPlayers;

            ResetTeams();
            CreateConfrontations();

            foreach (Player p in players)
            {
                if(p.Team == chooseTeam)
                    TeamGame.Add(p);
            }
        }
        else
        {
            UseSave();
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

        Team[,] halfCS = new Team[190, 2];

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
                    halfCS[indexArray, 0] = pair.Key;
                    halfCS[indexArray, 1] = pair.Value[i];
                }
                else
                {
                    halfCS[indexArray, 1] = pair.Key;
                    halfCS[indexArray, 0] = pair.Value[i];
                }
                result2 += pair.Key.Name;
                result2 += pair.Value[i].Name;
                indexArray++;
            }
        }
        
        for(int i = 0; i < 190; i++)
        {
            sw.WriteLine(halfCS[i, 0].Name + "," + halfCS[i, 1].Name);
        }

        for(int i = 0; i < 190; i++)
        {
            sw.WriteLine(halfCS[i, 1].Name + "," + halfCS[i, 0].Name);
        }

        sw.Close();
    }
    private void UseSave()
    {
        string[] lines = File.ReadAllLines("./Game/confrontations.txt");
        foreach (string l in lines)
        {
            string[] line = l.Split(',');
            Team[] conf = new Team[2];
            conf[0] = teams.FirstOrDefault(t => t.Name == line[0]);
            conf[1] = teams.FirstOrDefault(t => t.Name == line[1]);
            confrontations.Add(conf);
        }
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
}