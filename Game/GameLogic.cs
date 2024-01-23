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

        foreach(Team t in teams)
        {
            MessageBox.Show(t.Name);
            for (int i = 0; i < dict[t].Length; i++)
            {
                MessageBox.Show(dict[t][i].Name);
            }
        }
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