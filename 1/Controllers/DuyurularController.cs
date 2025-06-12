using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BenimsiteMvc.Data;
using BenimsiteMvc.Models;

namespace BenimsiteMvc.Controllers
{
    public class DuyurularController : Controller
    {
        private readonly VeritabaniContext _context;

        public DuyurularController(VeritabaniContext context)
        {
            _context = context;
        }

        // GET: Duyurular
        public async Task<IActionResult> Index()
        {
            return View(await _context.Duyurular.ToListAsync());
        }

        // GET: Duyurular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyurular
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duyuru == null)
            {
                return NotFound();
            }

            return View(duyuru);
        }

        // GET: Duyurular/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Duyurular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Baslik,Icerik,Aktif,YayimTarihi")] Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duyuru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duyuru);
        }

        // GET: Duyurular/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyurular.FindAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }
            return View(duyuru);
        }

        // POST: Duyurular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Baslik,Icerik,Aktif,YayimTarihi")] Duyuru duyuru)
        {
            if (id != duyuru.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duyuru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuyuruExists(duyuru.Id))
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
            return View(duyuru);
        }

        // GET: Duyurular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyurular
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duyuru == null)
            {
                return NotFound();
            }

            return View(duyuru);
        }

        // POST: Duyurular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duyuru = await _context.Duyurular.FindAsync(id);
            if (duyuru != null)
            {
                _context.Duyurular.Remove(duyuru);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuyuruExists(int id)
        {
            return _context.Duyurular.Any(e => e.Id == id);
        }
    }
}
