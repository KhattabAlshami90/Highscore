namespace Highscore.Models;

public class GameHighScore
{
    public string GameTitle { get; set; } = string.Empty;
    public string Player { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int ScoreP { get; set; }
    public int ScoreIden { get; set; }
    public string GameUrlSlug { get; set; }
}
