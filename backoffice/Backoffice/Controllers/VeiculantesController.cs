using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Veiculante;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class VeiculantesController : Controller
    {
        private readonly ContextBase _context;

        public VeiculantesController(ContextBase context)
        {
            _context = context;
        }

        // GET: Veiculantes
        public async Task<IActionResult> Index()
        {
              return _context.Veiculante != null ? 
                          View(await _context.Veiculante.ToListAsync()) :
                          Problem("Entity set 'ContextBase.Veiculante'  is null.");
        }

        // GET: Veiculantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veiculante == null)
            {
                return NotFound();
            }

            var veiculante = await _context.Veiculante
                .FirstOrDefaultAsync(m => m.IdVeiculante == id);
            if (veiculante == null)
            {
                return NotFound();
            }

            return View(veiculante);
        }

        // GET: Veiculantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veiculantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVeiculante,Nome")] Veiculante veiculante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculante);
        }

        // GET: Veiculantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veiculante == null)
            {
                return NotFound();
            }

            var veiculante = await _context.Veiculante.FindAsync(id);
            if (veiculante == null)
            {
                return NotFound();
            }
            return View(veiculante);
        }

        // POST: Veiculantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVeiculante,Nome")] Veiculante veiculante)
        {
            if (id != veiculante.IdVeiculante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculanteExists(veiculante.IdVeiculante))
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
            return View(veiculante);
        }

        // GET: Veiculantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veiculante == null)
            {
                return NotFound();
            }

            var veiculante = await _context.Veiculante
                .FirstOrDefaultAsync(m => m.IdVeiculante == id);
            if (veiculante == null)
            {
                return NotFound();
            }

            return View(veiculante);
        }

        // POST: Veiculantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veiculante == null)
            {
                return Problem("Entity set 'ContextBase.Veiculante'  is null.");
            }
            var veiculante = await _context.Veiculante.FindAsync(id);
            if (veiculante != null)
            {
                _context.Veiculante.Remove(veiculante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculanteExists(int id)
        {
          return (_context.Veiculante?.Any(e => e.IdVeiculante == id)).GetValueOrDefault();
        }
    }
}
