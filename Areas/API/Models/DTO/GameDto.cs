namespace Highscore.Areas.API.Models.DTO
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int Year { get; set; }
        public Uri ImageUrl { get; set; }
        public string Genre { get; set; } = String.Empty;
    }
}
