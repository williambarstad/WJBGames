using AspNetCoreWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApplication.Controllers
{
    public class GameController : Controller
    {
        Table gametable = new Table();

        public IActionResult Index(Table gametable)
        {
            Game game = new Game(gametable);

            return View(game);
        }

    }
}