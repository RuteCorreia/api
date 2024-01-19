using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Combustivel;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class CombustiveisController : Controller
    {
        private readonly ContextBase _context;

        public CombustiveisController(ContextBase context)
        {
            _context = context;
        }

        // GET: Combustiveis
        public async Task<IActionResult> Index()
        {
              return _context.Combustivel != null ? 
                          View(await _context.Combustivel.ToListAsync()) :
                          Problem("Entity set 'ContextBase.Combustivel'  is null.");
        }

        // GET: Combustiveis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Combustivel == null)
            {
                return NotFound();
            }

            var combustivel = await _context.Combustivel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (combustivel == null)
            {
                return NotFound();
            }

            return View(combustivel);
        }

        // GET: Combustiveis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Combustiveis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Combustivel combustivel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(combustivel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(combustivel);
        }

        // GET: Combustiveis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Combustivel == null)
            {
                return NotFound();
            }

            var combustivel = await _context.Combustivel.FindAsync(id);
            if (combustivel == null)
            {
                return NotFound();
            }
            return View(combustivel);
        }

        // POST: Combustiveis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Combustivel combustivel)
        {
            if (id != combustivel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(combustivel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CombustivelExists(combustivel.Id))
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
            return View(combustivel);
        }

        // GET: Combustiveis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Combustivel == null)
            {
                return NotFound();
            }

            var combustivel = await _context.Combustivel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (combustivel == null)
            {
                return NotFound();
            }

            return View(combustivel);
        }

        // POST: Combustiveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Combustivel == null)
            {
                return Problem("Entity set 'ContextBase.Combustivel'  is null.");
            }
            var combustivel = await _context.Combustivel.FindAsync(id);
            if (combustivel != null)
            {
                _context.Combustivel.Remove(combustivel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CombustivelExists(int id)
        {
          return (_context.Combustivel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
