using System;
using System.Collections.Generic;

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
        foreach(Team t1 in teams)
        {
            foreach (Team t2 in teams)
            {
                if(t1 != t2)
                    confrontations.Add(new Team[] { t1, t2 });
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