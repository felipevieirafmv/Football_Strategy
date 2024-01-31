using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms;

namespace Game;

public class Game //singleton?
{
    private static Game crr = new Game();
    public static Game Current => crr;
    public Game() { }

    public List<Player> TeamGame { get; set; }
    public List<Team> AllTeams { get; set; } = Teams.GetAllTeams;
    public List<Team[]> Confrontations { get; set; }
    public Team CrrTeam { get; set; }
    public object LineUp { get; set; }
    public int Gold { get; set; } = 1_000;


    public static void OpenSave()
    {

    }

    public void UpdateSave()
    {

    }

    public List<Player> NewPlayers()
    {
        List<Player> list = new();
        for(int i = 0; i < 20; i++)
        {
            list.Add(new Player(this.CrrTeam.Name));
        }

        this.TeamGame = list;
        return list;
    }


    public static void New(string chooseTeam)
    {
        StartGame sg = new StartGame(true, chooseTeam);

        Current.Confrontations = sg.Confrontations;
    }
}