using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkMVCNew.Data;
using EntityFrameworkMVCNew.Models;

namespace EntityFrameworkMVCNew.Controllers
{
    public class CricketPlayersController : Controller
    {
        private readonly EntityFrameworkMVCNewContext _context;

        public CricketPlayersController(EntityFrameworkMVCNewContext context)
        {
            _context = context;
        }

        // GET: CricketPlayers
        public async Task<IActionResult> Index()
        {
            return View(await _context.CricketPlayers.ToListAsync());
        }

        // GET: CricketPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cricketPlayer = await _context.CricketPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cricketPlayer == null)
            {
                return NotFound();
            }

            return View(cricketPlayer);
        }

        // GET: CricketPlayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CricketPlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age")] CricketPlayer cricketPlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cricketPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cricketPlayer);
        }

        // GET: CricketPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cricketPlayer = await _context.CricketPlayers.FindAsync(id);
            if (cricketPlayer == null)
            {
                return NotFound();
            }
            return View(cricketPlayer);
        }

        // POST: CricketPlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] CricketPlayer cricketPlayer)
        {
            if (id != cricketPlayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cricketPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CricketPlayerExists(cricketPlayer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cricketPlayer);
        }

        // GET: CricketPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cricketPlayer = await _context.CricketPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cricketPlayer == null)
            {
                return NotFound();
            }

            return View(cricketPlayer);
        }

        // POST: CricketPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cricketPlayer = await _context.CricketPlayers.FindAsync(id);
            _context.CricketPlayers.Remove(cricketPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CricketPlayerExists(int id)
        {
            return _context.CricketPlayers.Any(e => e.Id == id);
        }

        public async Task<int> GetMaxId()
        {
            return await _context.CricketPlayers.MaxAsync(u => u.Id);
        }
    }
}
