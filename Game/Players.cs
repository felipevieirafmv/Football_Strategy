using System;
using System.IO;
using System.Collections.Generic;

namespace Game;

public static class Players
    {
        private static List<Player> allPlayers;

        public static List<Player> GetAllPlayers
        {
            get
            {
                if (allPlayers == null)
                {
                    allPlayers = new List<Player>();

                    string[] lines = File.ReadAllLines("./Game/players.txt");
                    foreach (string line in lines)
                    {
                        string[] stats = line.Split(',');
                        allPlayers.Add(new Player(int.Parse(stats[0]), stats[1], stats[2], int.Parse(stats[3]), stats[4]));
                    }
                }

                return allPlayers;
            }
        }
    }