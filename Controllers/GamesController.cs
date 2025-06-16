using Highscore.Data;
using Highscore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Highscore.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext  context;
        public GamesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Route("games/{urlSlug}")]
        public IActionResult Details(string urlSlug)
        {
            var game = context.Game.FirstOrDefault(x => x.UrlSlug == urlSlug);
            var gameScores = context.Score.Where(s=>s.GameId==game.Id).ToList();
            GameScoresModel gameScoresModel = new GameScoresModel()
            {
                Game = game,
                Scores = gameScores.OrderByDescending(p => p.ScoreP).ToList()
            };
            if (game != null) {
                ViewData["Title"] = game.Title;
                return View(gameScoresModel);
            }
            else
            {
                return NotFound();
            }
              
        }
    }
}
