using Highscore.Areas.API.Models.DTO;
using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Highscore.Areas.API.Conroller
{
    [Authorize(
       AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme
    )]
    [Area("Api")]
    [Route("api/[controller]")]
    [ApiController]
public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public GamesController(ApplicationDbContext context)
        { 
            this.context = context;
        }


        /// <summary>
        /// Sök spel
        /// </summary>
        /// <param name="gameTitle">Spelnamn</param>
        


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        public IEnumerable<GameDto> GetGames([FromQuery] string? gameTitle)
        {
            var games = gameTitle is null
               ? context.Game.ToList()
               : context.Game.Where(x => x.Title.Contains(gameTitle)).ToList();

            var gameDtos = games.Select(x=> new GameDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Genre = x.Genre,
                Year = x.Year,
            } );

            return gameDtos;
        }

        /// <summary>
        /// Skapa ett spel
        /// </summary>
        /// <param name="gameDto">Spel Info</param>

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult CreateGame(GameDto gameDto)
        {

            var savedGames = context.Game.FirstOrDefault(x => x.Title == gameDto.Title);

            if(savedGames!=null)
            {
                return BadRequest();
            }


            var game = new Game
            {
                Title = gameDto.Title,
                Description = gameDto.Description,
                ImageUrl = gameDto.ImageUrl,
                Genre = gameDto.Genre,
                Year = gameDto.Year,
            };


            game.UrlSlug = game.Title
            .Replace("-", "")
            .Replace(" ", "-")
            .ToLower();

            context.Game.Add(game);
            context.SaveChanges();
            return Created("",null);
        }


        /// <summary>
        /// Hämta ett spel beroende på ID
        /// </summary>
        /// <param name="id">Spel ID</param>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public ActionResult<GameDto> GetGame(int id)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var gameDto = new GameDto
            {
                Title = game.Title,
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                Genre = game.Genre,
                Year = game.Year,
                Id = id
            };

            return gameDto          ;
        }



        /// <summary>
        /// Uppdatera ett spel
        /// </summary>
        /// <param name="id">Spel ID</param>
        /// <param name="gameDto">Spel Info</param>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        [HttpPut]
        public  ActionResult UpdateGame(int id, [FromBody] GameDto gameDto)
        {
            var existingGame = context.Game.FirstOrDefault(x => x.Id == id);

            if (existingGame == null)
            {
                return NotFound(); 
            }
            var updateTitle = context.Game.FirstOrDefault(x => x.Title == gameDto.Title && x.Id !=gameDto.Id );

            if (updateTitle != null)
            {
                return BadRequest();
            }

            existingGame.Title = gameDto.Title;
            existingGame.Description = gameDto.Description;
            existingGame.ImageUrl = gameDto.ImageUrl;
            existingGame.Year = gameDto.Year;
            existingGame.Genre = gameDto.Genre;

            context.Entry(existingGame).State = EntityState.Modified;
            context.SaveChanges();

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("The game was updated successfully");

            return Ok();
        }


        /// <summary>
        /// Radera ett spel
        /// </summary>
        /// <param name="id">Spel ID</param>


        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteGame(int id)
        {
            var game = context.Game.FirstOrDefault(x => x.Id == id);

            if (game == null)
            {
                return NotFound(); 
            }

            context.Game.Remove(game);
            context.SaveChanges();

            return NoContent();
        }
    }
}
