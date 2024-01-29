using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Forms;

namespace Game;

public class Game //singleton?
{
    private static Game crr = new Game();
    public static Game Current => crr;
    public Game() { }

    public List<Player> TeamGame { get; private set; }
    public List<Team> AllTeams { get; set; } = Teams.GetAllTeams;
    public List<Team[]> Confrontations { get; set; } = new();
    public string LineUp { get; set; }
    

    public void OpenSave()
    {

    }

    public void UpdateSave()
    {

    }

    public static void New(bool newGame, string chooseTeam)
    {
        StartGame sg = new StartGame(newGame, chooseTeam);

        
    }
}