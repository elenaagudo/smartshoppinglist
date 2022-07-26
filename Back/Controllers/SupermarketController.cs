using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Models;

namespace Back.Controllers
{
    public class SupermarketController : Controller
    {
        private readonly ApplicationContext db;

        public SupermarketController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Supermarket> supermarketList = db.Supermarket.Where(s => s.IsDeleted == false).ToList();
            return View(supermarketList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supermarket supermarket)
        {
            try
            {
                Supermarket mySupermarket = db.Supermarket.Where(s => s.Name == supermarket.Name).FirstOrDefault();

                if (mySupermarket != null)
                {
                    return Conflict();
                }

                supermarket.CreateDate = DateTime.Now;
                supermarket.UpdateDate = DateTime.MinValue;
                supermarket.DeleteDate = DateTime.MinValue;
                supermarket.IsDeleted = false;

                db.Supermarket.Add(supermarket);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supermarket = await db.Supermarket.FindAsync(id);
            if (supermarket == null)
            {
                return NotFound();
            }
            return View(supermarket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Supermarket supermarket)
        {
            if (id != supermarket.Id)
            {
                return NotFound();
            }

            try
            {
                Supermarket mySupermarket = db.Supermarket.Where(s => s.Name == supermarket.Name).FirstOrDefault();
                if (mySupermarket != null)
                {
                    if (mySupermarket.Id != supermarket.Id)
                    {
                        return Conflict();
                    }

                    db.Entry(mySupermarket).State = EntityState.Detached;
                }

                Supermarket dbSupermarket = db.Supermarket.Where(r => r.Id == supermarket.Id).FirstOrDefault();
                dbSupermarket.Name = supermarket.Name;
                dbSupermarket.UpdateDate = DateTime.Now;

                db.Supermarket.Update(dbSupermarket);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var supermarket = await db.Supermarket.FirstOrDefaultAsync(s => s.Id == id);
            if (supermarket == null)
            {
                return NotFound();
            }

            return Ok(supermarket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var supermarket = await db.Supermarket.FindAsync(id);
                supermarket.IsDeleted = true;
                supermarket.DeleteDate = DateTime.Now;
                db.Supermarket.Update(supermarket);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
