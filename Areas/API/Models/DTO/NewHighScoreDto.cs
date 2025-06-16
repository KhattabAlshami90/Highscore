namespace Highscore.Areas.API.Models.DTO
{
public class NewHighScoreDto
    {
        public string Game { get; set; }
        public string Player { get; set; }
        public DateTime Date { get; set; }
        public int ScoreP { get; set; }
    }
}
