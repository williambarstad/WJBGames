using AspNetCoreWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreWebApplication.ViewComponents
{
    public class PlayerHandViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Hand _playerHand)
        {
            List<Card> cards = _playerHand._cardsInHand;
            return await Task.FromResult((IViewComponentResult)View(cards));
        }
    }
}
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewComponentSample.Models;

namespace ViewComponentSample.ViewComponents
{
    public class PriorityListViewComponent : ViewComponent
    {
        private readonly ToDoContext db;

        public PriorityListViewComponent(ToDoContext context)
        {
            db = context;
        }
       
        private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
        {
            return db.ToDo.Where(x => x.IsDone == isDone &&
                                 x.Priority <= maxPriority).ToListAsync();
        }
        #region snippet1
        public async Task<IViewComponentResult> InvokeAsync(
            int maxPriority, bool isDone)
        {
            string MyView = "Default";
            // If asking for all completed tasks, render with the "PVC" view.
            if (maxPriority > 3 && isDone == true)
            {
                MyView = "PVC";
            }
            var items = await GetItemsAsync(maxPriority, isDone);
            return View(MyView, items);
        }
        #endregion
    }
}

#endif*/
