using Highscore.Models.Domain;

namespace Highscore.Models.ViewModels
{
    public class GamesHighscoreModel
    {
        public List<GameHighScore> HighScores { get; set; }=new List<GameHighScore>();
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
