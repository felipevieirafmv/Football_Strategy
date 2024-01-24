using System;
using System.Collections.Generic;
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
        // StreamWriter sw = new StreamWriter("./Game/confrontations.txt");
        // foreach(Team t1 in teams)
        // {
        //     foreach (Team t2 in teams)
        //     {
        //         if(t1 != t2)
        //         {
        //             confrontations.Add(new Team[] { t1, t2 });
        //             sw.WriteLine(t1.Name + "," + t2.Name);
        //         }
        //     }
        // }
        // sw.Close();

        ChampionshipGenerator cg = new ChampionshipGenerator();

        foreach(Team t in teams)
        {
            cg.Add(t);
        }

        Dictionary<Team, Team[]> dict = cg.Generate();
        
        string result = "";
        foreach(var pair in dict)
        {
            result += (pair.Key?.Name ?? "null") + " ->\t";
            var array = pair.Value;
            foreach (var value in array)
                result += (value?.Name ?? "null") + ", ";
            result += "END\n\n";
        }
        MessageBox.Show(result);
    }

    public void ResetTeams()
    {
        foreach(Team t in teams)
        {
            t.Tactical = 1;
            t.Style = 1;
            t.Attack = 1;
        }
    }

    public void CreateMatch()
    {
        //pegar os dois times que vao se enfrentar e simular a partida (ira precisar de mais funcoes para funcionar, nao descarto a necessidade de uma nova classe)
    }
}