using System.ComponentModel.DataAnnotations;

namespace Highscore.Models.Domain
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }  = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Uri ImageUrl { get; set; } 
        public string Genre { get; set; } = String.Empty;
        public int Year { get; set; }

        [MaxLength(50)]
        public string? UrlSlug { get; set; }
    }
}
