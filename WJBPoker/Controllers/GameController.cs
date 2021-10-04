using WJBPoker.Models;
using Microsoft.AspNetCore.Mvc;

namespace WJBPoker.Controllers
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