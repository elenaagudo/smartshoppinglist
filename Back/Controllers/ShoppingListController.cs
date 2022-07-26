using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Controllers
{
    public class ShoppingListController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddProduct(int id, int value)
        {
            return Ok();
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(IFormCollection collection)
        //{
        //    return Ok();
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, IFormCollection collection)
        //{
        //    return Ok();
        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id, IFormCollection collection)
        //{
        //    return Ok();
        //}
    }
}
