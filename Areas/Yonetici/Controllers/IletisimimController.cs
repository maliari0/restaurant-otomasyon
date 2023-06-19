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
    public class IletisimimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IletisimimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yonetici/Iletisimim
        public async Task<IActionResult> Index()
        {
            return View(await _context.Iletisimims.ToListAsync());
        }

        // GET: Yonetici/Iletisimim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iletisimim = await _context.Iletisimims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iletisimim == null)
            {
                return NotFound();
            }

            return View(iletisimim);
        }

        // GET: Yonetici/Iletisimim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yonetici/Iletisimim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Telefon,Adres")] Iletisimim iletisimim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iletisimim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iletisimim);
        }

        // GET: Yonetici/Iletisimim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iletisimim = await _context.Iletisimims.FindAsync(id);
            if (iletisimim == null)
            {
                return NotFound();
            }
            return View(iletisimim);
        }

        // POST: Yonetici/Iletisimim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Telefon,Adres")] Iletisimim iletisimim)
        {
            if (id != iletisimim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iletisimim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IletisimimExists(iletisimim.Id))
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
            return View(iletisimim);
        }

        // GET: Yonetici/Iletisimim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iletisimim = await _context.Iletisimims
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iletisimim == null)
            {
                return NotFound();
            }

            return View(iletisimim);
        }

        // POST: Yonetici/Iletisimim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iletisimim = await _context.Iletisimims.FindAsync(id);
            _context.Iletisimims.Remove(iletisimim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IletisimimExists(int id)
        {
            return _context.Iletisimims.Any(e => e.Id == id);
        }
    }
}
