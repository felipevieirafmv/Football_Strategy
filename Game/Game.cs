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
    public int Round { get; set; } = 0;
    public List<Team> AllTeams { get; set; } = Teams.GetAllTeams;
    public List<Team[]> Confrontations { get; set; } = new List<Team[]>();
    public Team CrrTeam { get; set; }
    public object LineUp { get; set; }
    public int Gold { get; set; } = 1_000;
    public Team[] CrrConfrontation { get; set; } = new Team[2];

    public static void OpenSave()
    {

    }

    public void UpdateSave()
    {

    }

    public static void New(string chooseTeam)
    {
        StartGame sg = new StartGame(true, chooseTeam);
    }
}