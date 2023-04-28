using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static Highscore.Controllers.HighscoresController;

namespace Highscore.Areas.Admin.Controllers;


[Authorize(Roles = "Administrator")]
[Area("Admin")]
public class GamesController : Controller
{
    private readonly ApplicationDbContext context;
    public GamesController(ApplicationDbContext context)
    {
        this.context = context;

    }

    public IActionResult Index()
    {
        var games = context.Game.ToList();
        return View(games);
    }
    public IActionResult New()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> New(Form form)
    {
        var game = new Game()
        {
            Title = form.Title,
            Description = form.Description,
            ImageUrl = form.imageUrl,
            Genre = form.Genre,
            Year =form.Year,
            UrlSlug =form.UrlSlug,

        };
        context.Game.Add(game);
        context.SaveChanges();
        return Redirect("/Admin/games");

    }


    public class Form
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri imageUrl { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string? UrlSlug { get; set; }
    }
}

