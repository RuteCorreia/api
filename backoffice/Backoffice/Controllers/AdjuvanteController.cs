using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Adjuvante;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class AdjuvanteController : Controller
    {
        private readonly ContextBase _context;

        public AdjuvanteController(ContextBase context)
        {
            _context = context;
        }

        // GET: Adjuvante
        public async Task<IActionResult> Index()
        {
              return _context.Adjuvante != null ? 
                          View(await _context.Adjuvante.ToListAsync()) :
                          Problem("Entity set 'ContextBase.Adjuvante'  is null.");
        }

        // GET: Adjuvante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adjuvante == null)
            {
                return NotFound();
            }

            var adjuvante = await _context.Adjuvante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adjuvante == null)
            {
                return NotFound();
            }

            return View(adjuvante);
        }

        // GET: Adjuvante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adjuvante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Adjuvante adjuvante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adjuvante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adjuvante);
        }

        // GET: Adjuvante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adjuvante == null)
            {
                return NotFound();
            }

            var adjuvante = await _context.Adjuvante.FindAsync(id);
            if (adjuvante == null)
            {
                return NotFound();
            }
            return View(adjuvante);
        }

        // POST: Adjuvante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Adjuvante adjuvante)
        {
            if (id != adjuvante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adjuvante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdjuvanteExists(adjuvante.Id))
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
            return View(adjuvante);
        }

        // GET: Adjuvante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adjuvante == null)
            {
                return NotFound();
            }

            var adjuvante = await _context.Adjuvante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adjuvante == null)
            {
                return NotFound();
            }

            return View(adjuvante);
        }

        // POST: Adjuvante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adjuvante == null)
            {
                return Problem("Entity set 'ContextBase.Adjuvante'  is null.");
            }
            var adjuvante = await _context.Adjuvante.FindAsync(id);
            if (adjuvante != null)
            {
                _context.Adjuvante.Remove(adjuvante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdjuvanteExists(int id)
        {
          return (_context.Adjuvante?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
