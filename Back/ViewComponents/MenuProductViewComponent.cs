using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.ViewComponents
{
    public class MenuProductViewComponent : ViewComponent
    {
        private ApplicationContext db;
        public MenuProductViewComponent(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Room> roomList = db.Room.Where(r => r.IsDeleted == false).ToList();
            return View("~/Views/Shared/Components/Product/_Product.cshtml", roomList);
        }
    }
}
