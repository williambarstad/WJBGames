using WJBPoker.Models;
using Microsoft.AspNetCore.Mvc;

namespace WJBPoker.Controllers
{
    public class TableController : Controller
    {
        Table wjbTable = new Table();

        public IActionResult Index()
        {
            return View(wjbTable);
        }
    }
}