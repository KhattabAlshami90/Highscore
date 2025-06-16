using Microsoft.AspNetCore.Mvc;
using Highscore.Models;
using Highscore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Highscore.Models.ViewModels;
using Highscore.Models.Domain;





namespace Highscore.Controllers;

[Authorize]
[ApiExplorerSettings(IgnoreApi = true)]
public class HighscoresController : Controller
{
    private readonly ApplicationDbContext context;  
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public HighscoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        _userManager = userManager;
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
    public IActionResult New()
    {
        var games = context.Game.ToList();


        return View(games);

    }

    [HttpPost]
    public async Task<IActionResult> New(Form form)
    {
        var game = context.Game.Where(x => x.Title == form.Game).FirstOrDefault();
        var user = await _userManager.GetUserAsync(User);




        var score = new Score()
        {
            GameId = game.Id,
            GameTitle = game.Title,
            Player = form.Player,
            Date = form.Date,
            ScoreP = form.ScoreP,
        };
        context.Score.Add(score);
        context.SaveChanges();


        var highScores = new List<GameHighScore>();
        var games = context.Game.ToList();
        var scores = context.Score;

        foreach (var _game in games)
        {
            Score _gameHighScore = new Score();

            var gameScores = scores.Where(p => p.GameId == _game.Id).ToList();


            if (gameScores == null || gameScores.Count == 0)
            {
                continue;
            }

            else
            {
                int max = 0;
                foreach (var _score in gameScores)
                {
                    if (score.ScoreP > max)
                    {
                        max = _score.ScoreP;
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
                    GameUrlSlug = game.UrlSlug,
                };
                highScores.Add(gameHighScore);
            }



        }
        var highScoreModel = new GamesHighscoreModel()
        {
            HighScores = highScores,
            Games = games
        };






        return RedirectToAction("Index", "home", new { GamesHighscoreModel = highScoreModel });
    }

    }
    public class Form
    {
        public string Game { get; set; }
        public string Player { get; set; }
        public DateTime Date { get; set; }
        public int ScoreP { get; set; }
    }

