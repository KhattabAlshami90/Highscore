using Highscore.Data;
using Highscore.Models;
using Highscore.Models.Domain;
using Highscore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace Highscore.Areas.Admin.Controllers;
[Authorize(Roles = "Administrator")]
[Area("Admin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext context;
    private readonly ApplicationDbContext _context;
    private string? gamesHighscoreModel;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
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
                };
                highScores.Add(gameHighScore);
            }

        }


        return View(highScores);

    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}