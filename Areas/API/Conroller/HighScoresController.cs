using Highscore.Areas.API.Models.DTO;
using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Highscore.Areas.API.Conroller
{



    [Authorize(
   AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme
    )]
    [Route("api/[controller]")]
    [Area("API")]
    [ApiController]
    public class HighScoresController : ControllerBase
    {


        private readonly ApplicationDbContext context;
        public HighScoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Skapa ny Highscore
        /// </summary>
        /// <param name="newHighScoreDto">Highscore Info</param>


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult NewHighScore(NewHighScoreDto newHighScoreDto)
        {
            var game = context.Game.FirstOrDefault(x => x.Title == newHighScoreDto.Game);

            if (game == null)
            {
                return BadRequest();
            }

            var gameId = game.Id;
            var score = new Score
            {
                GameId=gameId,
                GameTitle= newHighScoreDto.Game,
                Player= newHighScoreDto.Player,
                Date = newHighScoreDto.Date,
                ScoreP= newHighScoreDto.ScoreP,

            };


            context.Score.Add(score);
            context.SaveChanges();
            return Created("", null);
        }
    }
}
