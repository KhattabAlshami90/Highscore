using Highscore.Data;
using Highscore.Models.Domain;
using Highscore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Highscore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Admin")]
    public class HighScoresController : Controller
    {
        private readonly ApplicationDbContext context;
        public HighScoresController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var highScores = new List<GameHighScore>();
            var games = context.Game.ToList();
            var scores = context.Score;

            foreach (var game in games)
            {
                Score _gameHighScore = new Score();

                var gameScores = scores.Where(p => p.GameId == game.Id).ToList();


                if (gameScores == null || gameScores.Count == 0)
                {
                    continue;
                }

                else
                {
                    int max = 0;
                    foreach (var score in gameScores)
                    {
                        if (score.ScoreP > max)
                        {
                            max = score.ScoreP;
                            _gameHighScore = score;
                        }

                    }

                    var gameHighScore = new GameHighScore()
                    {
                        GameTitle = game.Title,
                        Player = _gameHighScore.Player,
                        Date = _gameHighScore.Date,
                        ScoreP = _gameHighScore.ScoreP,
                        ScoreIden = _gameHighScore.Id,
                    };
                    highScores.Add(gameHighScore);
                }
            }

            return View(highScores);
        }
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            
            Score score = context.Score.Find(id);

            if (score == null)
            {

                return HttpNotFound();
            }

            
            context.Score.Remove(score);
            context.SaveChanges();
            var scores = context.Score.ToList();


            return RedirectToAction("Index");
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
