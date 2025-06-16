using System.ComponentModel.DataAnnotations;

namespace Highscore.Models.Domain
{
    public class Score
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Player { get; set; }
        public Game Game  { get; set; }
        public int GameId { get; set; }
        public string GameTitle { get; set; }

        public DateTime Date { get; set; }
        public int ScoreP { get; set; }
    }
}
