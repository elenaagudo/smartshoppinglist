using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            // en la pagina de inicio se muestran las ultimas 5 listas de la compra
            List<ShoppingList> lastShoppingLists = db.ShoppingList.Include(s => s.ListProducts).ThenInclude(s => s.Product).Where(s => !s.IsDeleted).OrderByDescending(s => s.CreateDate).Take(5).ToList();

            return View(lastShoppingLists);
        }
    }
}
