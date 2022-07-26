using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // en la pagina de inicio se muestran las ultimas 5 listas de la compra
            List<ShoppingList> lastShoppingLists = db.ShoppingList.Include(s => s.ListProducts).ThenInclude(s => s.Product).Include(s => s.Supermarket).Where(s => !s.IsDeleted).OrderByDescending(s => s.CreateDate).Take(5).ToList();

            return View(lastShoppingLists);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Supermarket> supermarketList = db.Supermarket.Where(s => s.IsDeleted == false).ToList();
            Dictionary<string, string> supermarkets = new Dictionary<string, string>();

            foreach (Supermarket mySupermarket in supermarketList)
            {
                supermarkets.Add(mySupermarket.Id.ToString(), mySupermarket.Name);
            }

            return Ok(supermarkets);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id)
        {
            ShoppingList myShoppingList = new ShoppingList();
            myShoppingList.Supermarket = db.Supermarket.Find(id);
            myShoppingList.Code = GenerateCode(myShoppingList.Supermarket.Name);
            myShoppingList.CreateDate = DateTime.Now;
            myShoppingList.DeleteDate = DateTime.MinValue;
            myShoppingList.Active = true;
            myShoppingList.Printed = false;
            myShoppingList.IsDeleted = false;

            db.ShoppingList.Add(myShoppingList);
            db.SaveChanges();

            return Ok(myShoppingList.Code);
        }

        private string GenerateCode(string supermarket)
        {
            string code = supermarket.Substring(0, 3).ToUpper() + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return code;
        }
    }
}
