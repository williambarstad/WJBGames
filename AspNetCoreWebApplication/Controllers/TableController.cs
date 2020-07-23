using AspNetCoreWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApplication.Controllers
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