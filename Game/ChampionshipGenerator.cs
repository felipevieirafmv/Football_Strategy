using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Game;

public class ChampionshipGenerator
{
    private List<Team> teams = Teams.GetAllTeams;
    private Dictionary<Team, Team[]> matchMap = new();

    public void Add(Team team)
    {
        this.teams.Add(team);
        this.matchMap.Add(team, new Team[19]);
    }

    public Dictionary<Team, Team[]> Generate()
    {
        while (teams.Count > 0)
        {
            Team crr = getRandomTeam();
            this.teams.Remove(crr);

            Team[] matches = matchMap[crr];
            fillMatches(matches, crr);
        }

        return matchMap;
    }

    private Team getRandomTeam()
    {
        Random random = new Random();

        return teams[random.Next(teams.Count())];
    }

    private void fillMatches(Team[] matches, Team crr)
    {
        for(int i = 0; i < teams.Count(); i++)
        {
            Team t = getRandomTeam();
            if(matches[i] != null)
            {
                if(!matches.Contains(t))
                {
                    matches[i] = t;
                    matchMap[t][i] = crr;
                }
                else
                    i--;
            }
        }
    }
}
