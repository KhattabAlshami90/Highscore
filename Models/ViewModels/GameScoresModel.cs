using Highscore.Models.Domain;

namespace Highscore.Models.ViewModels
{
    public class GameScoresModel
    {
        public Game Game { get; set; } 
       public List<Score> Scores { get; set; }
    }
}
