using System;
using System.IO;
using System.Collections.Generic;

namespace Game;

public static class Teams
    {
        private static List<Team> allTeams;

        public static List<Team> GetAllTeams
        {
            get
            {
                if (allTeams == null)
                {
                    allTeams = new List<Team>();

                    string[] lines = File.ReadAllLines("./Game/teams.txt");
                    foreach (string line in lines)
                    {
                        string[] stats = line.Split(',');
                        allTeams.Add(new Team(stats[0], int.Parse(stats[1])));
                    }
                }

                return allTeams;
            }
        }
    }