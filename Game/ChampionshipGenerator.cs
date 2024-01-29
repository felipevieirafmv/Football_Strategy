using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.CodeDom.Compiler;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Game;

public class ChampionshipGenerator
{
    private List<Team> teams = new();
    private Dictionary<Team, Team[]> matchMap = new();

    public void Add(Team team)
    {
        this.teams.Add(team);
        this.matchMap.Add(team, new Team[19]);
    }

    public Dictionary<Team, Team[]> Generate()
    {
        LinkedList<Team> teams = new LinkedList<Team>(
            getRandomTeams()
        );

        int round = teams.Count - 1;
        for (int i = 0; i < round; i++)
        {
            var last = teams.Last;

            var ita = teams.First;
            var itb = last;

            for(int j = 0; j < teams.Count / 2; j++)
            {
                matchMap[ita.Value][i] = itb.Value;
                matchMap[itb.Value][i] = ita.Value;
                
                ita = ita.Next;
                itb = itb.Previous;
            }

            teams.RemoveLast();
            teams.AddAfter(teams.First, last);
        }
        

        return matchMap;
    }

    private Team getRandomTeam()
    {
        if (teams.Count == 0)
            return null;

        Random random = Random.Shared;
        return teams[random.Next(teams.Count())];
    }

    private List<Team> getRandomTeams()
    {
        return teams
            .OrderBy(t => Random.Shared.Next())
            .ToList();
    }
}