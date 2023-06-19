using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurant.Data;
using restaurant.Models;

namespace restaurant.Areas.Yonetici.Controllers
{
    [Area("Yonetici")]
    [Authorize]
    public class IletisimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IletisimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yonetici/Iletisim
        public async Task<IActionResult> Index()
        {
            return View(await _context.Iletisims.ToListAsync());
        }

        // GET: Yonetici/Iletisim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iletisim = await _context.Iletisims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iletisim == null)
            {
                return NotFound();
            }

            return View(iletisim);
        }

        // GET: Yonetici/Iletisim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/Iletisim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Telefon,Mesaj,Tarih")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iletisim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iletisim);
        }

        // GET: Yonetici/Iletisim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iletisim = await _context.Iletisims.FindAsync(id);
            if (iletisim == null)
            {
                return NotFound();
            }
            return View(iletisim);
        }

        // POST: Yonetici/Iletisim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Telefon,Mesaj,Tarih")] Iletisim iletisim)
        {
            if (id != iletisim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iletisim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IletisimExists(iletisim.Id))
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
            return View(iletisim);
        }

        // GET: Yonetici/Iletisim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iletisim = await _context.Iletisims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iletisim == null)
            {
                return NotFound();
            }

            return View(iletisim);
        }

        // POST: Yonetici/Iletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iletisim = await _context.Iletisims.FindAsync(id);
            _context.Iletisims.Remove(iletisim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IletisimExists(int id)
        {
            return _context.Iletisims.Any(e => e.Id == id);
        }
    }
}
