namespace Game;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Team { get; set; }
    public int OverAll { get; set; }
    public string Position { get; set; }

    public Player(int id, string name, string team, int overall, string position)
    {
        this.Id = id;
        this.Name = name;
        this.Team = team;
        this.OverAll = overall;
        this.Position = position;
    }
}