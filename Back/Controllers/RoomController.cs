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
    public class RoomController : Controller
    {
        private readonly ApplicationContext db;

        public RoomController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Room> roomList = db.Room.Where(r => r.IsDeleted == false).ToList();
            return View(roomList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            try
            {
                Room myRoom = db.Room.Where(r => r.Name == room.Name).FirstOrDefault();

                if (myRoom != null)
                {
                    return Conflict();
                }

                room.CreateDate = DateTime.Now;
                room.UpdateDate = DateTime.MinValue;
                room.DeleteDate = DateTime.MinValue;
                room.IsDeleted = false;

                db.Room.Add(room);
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

            var room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            try
            {
                Room myRoom = db.Room.Where(r => r.Name == room.Name).FirstOrDefault();
                if (myRoom != null)
                {
                    if (myRoom.Id != room.Id)
                    {
                        return Conflict();
                    }

                    db.Entry(myRoom).State = EntityState.Detached;
                }

                Room dbRoom = db.Room.Where(r => r.Id == room.Id).FirstOrDefault();
                dbRoom.Name = room.Name;
                dbRoom.UpdateDate = DateTime.Now;

                db.Room.Update(dbRoom);
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

            var room = await db.Room.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var room = await db.Room.FindAsync(id);
                room.IsDeleted = true;
                room.DeleteDate = DateTime.Now;
                db.Room.Update(room);
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
