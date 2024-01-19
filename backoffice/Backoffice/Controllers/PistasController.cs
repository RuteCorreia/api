using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Pistas;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class PistasController : Controller
    {
        private readonly ContextBase _context;

        public PistasController(ContextBase context)
        {
            _context = context;
        }

        // GET: Pistas
        public async Task<IActionResult> Index()
        {
              return _context.Pista != null ? 
                          View(await _context.Pista.ToListAsync()) :
                          Problem("Entity set 'ContextBase.Pista'  is null.");
        }

        // GET: Pistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pista == null)
            {
                return NotFound();
            }

            var pista = await _context.Pista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pista == null)
            {
                return NotFound();
            }

            return View(pista);
        }

        // GET: Pistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,LAT,LONG")] Pista pista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pista);
        }

        // GET: Pistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pista == null)
            {
                return NotFound();
            }

            var pista = await _context.Pista.FindAsync(id);
            if (pista == null)
            {
                return NotFound();
            }
            return View(pista);
        }

        // POST: Pistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,LAT,LONG")] Pista pista)
        {
            if (id != pista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PistaExists(pista.Id))
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
            return View(pista);
        }

        // GET: Pistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pista == null)
            {
                return NotFound();
            }

            var pista = await _context.Pista
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pista == null)
            {
                return NotFound();
            }

            return View(pista);
        }

        // POST: Pistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pista == null)
            {
                return Problem("Entity set 'ContextBase.Pista'  is null.");
            }
            var pista = await _context.Pista.FindAsync(id);
            if (pista != null)
            {
                _context.Pista.Remove(pista);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PistaExists(int id)
        {
          return (_context.Pista?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
