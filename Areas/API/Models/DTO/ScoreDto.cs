using Highscore.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Highscore.Areas.API.Models.DTO
{
    public class ScoreDto
    {

        public string Player { get; set; }
        public string GameTitle { get; set; }
        public int ScoreP { get; set; }
    }
}
