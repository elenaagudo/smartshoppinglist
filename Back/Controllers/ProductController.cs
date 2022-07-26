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
    public class ProductController : Controller
    {
        private readonly ApplicationContext db;

        public ProductController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> List()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts(int room, string search)
        {
            // room es el id de la estancia
            // si room es diferente de 0 buscar todos los productos de esa estancia, que isdeleted == false
            // si search es diferente de null buscar los productos que tengan esa cadena de texto en name, description o displayname
            // si los dos campos son diferente de null buscar los productos de esa estancia que contengan esa cadena de search
            List<Product> productList = new List<Product>();

            if (room != 0 && search != null)
            {
                productList = db.Product.Where(p => p.Room.Id == room && p.IsDeleted == false && 
                    (p.Name.Contains(search) || p.Description.Contains(search) || p.DisplayName.Contains(search))).ToList();
            }
            else if (room != 0)
            {
                productList = db.Product.Where(p => p.Room.Id == room && p.IsDeleted == false).ToList();
            }
            else if (search != null)
            {
                productList = db.Product.Where(p => p.IsDeleted == false &&
                    (p.Name.Contains(search) || p.Description.Contains(search) || p.DisplayName.Contains(search))).ToList();
            }
            else
            {
                productList = db.Product.Where(p=>p.IsDeleted == false).ToList();
            }

            ViewBag.room = room;

            return View("List", productList);
        }

        public async Task<IActionResult> Index()
        {
            List<Product> productList = db.Product.Include(p => p.Supermarket).Include(p => p.Room).Where(p => p.IsDeleted == false).ToList();
            return View(productList);
        }

        public IActionResult Create()
        {
            // crear listas para los desplegables
            CreateLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                Product myProduct = db.Product.Where(s => s.Name == product.Name).FirstOrDefault();

                if (myProduct != null)
                {
                    return Conflict();
                }

                product.Supermarket = db.Supermarket.Find(product.Supermarket.Id);
                product.Room = db.Room.Find(product.Room.Id);
                product.CreateDate = DateTime.Now;
                product.UpdateDate = DateTime.MinValue;
                product.DeleteDate = DateTime.MinValue;
                product.IsDeleted = false;

                db.Product.Add(product);
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

            var product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            CreateLists();
            
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            try
            {
                Product myProduct = db.Product.Where(p => p.Name == product.Name).FirstOrDefault();
                if (myProduct != null)
                {
                    if (myProduct.Id != product.Id)
                    {
                        return Conflict();
                    }

                    db.Entry(myProduct).State = EntityState.Detached;
                }

                Product dbProduct = db.Product.Where(p => p.Id == product.Id).FirstOrDefault();
                dbProduct.Name = product.Name;
                dbProduct.Description = product.Description;
                dbProduct.DisplayName = product.DisplayName;
                dbProduct.Supermarket = db.Supermarket.Find(product.Supermarket.Id);
                dbProduct.Room = db.Room.Find(product.Room.Id);
                dbProduct.UpdateDate = DateTime.Now;

                db.Product.Update(dbProduct);
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
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Product.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await db.Product.FindAsync(id);
                product.IsDeleted = true;
                product.DeleteDate = DateTime.Now;
                db.Product.Update(product);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        private void CreateLists()
        {
            List<Supermarket> supermarketList = db.Supermarket.ToList();
            ViewBag.supermarkets = new List<SelectListItem>();
            ViewBag.supermarkets.AddRange(new SelectList(supermarketList, "Id", "Name"));


            List<Room> roomList = db.Room.ToList();
            ViewBag.rooms = new List<SelectListItem>();
            ViewBag.rooms.AddRange(new SelectList(roomList, "Id", "Name"));
        }
    }
}
