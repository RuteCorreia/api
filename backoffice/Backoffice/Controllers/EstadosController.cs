using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Estados;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class EstadosController : Controller
    {
        private readonly ContextBase _context;

        public EstadosController(ContextBase context)
        {
            _context = context;
        }

        // GET: Estados
        public async Task<IActionResult> Index()
        {
              return _context.Estados != null ? 
                          View(await _context.Estados.ToListAsync()) :
                          Problem("Entity set 'ContextBase.Estados'  is null.");
        }

        // GET: Estados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estados == null)
            {
                return NotFound();
            }

            return View(estados);
        }

        // GET: Estados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sigla")] Estados estados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estados);
        }

        // GET: Estados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados.FindAsync(id);
            if (estados == null)
            {
                return NotFound();
            }
            return View(estados);
        }

        // POST: Estados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sigla")] Estados estados)
        {
            if (id != estados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosExists(estados.Id))
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
            return View(estados);
        }

        // GET: Estados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estados == null)
            {
                return NotFound();
            }

            var estados = await _context.Estados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estados == null)
            {
                return NotFound();
            }

            return View(estados);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estados == null)
            {
                return Problem("Entity set 'ContextBase.Estados'  is null.");
            }
            var estados = await _context.Estados.FindAsync(id);
            if (estados != null)
            {
                _context.Estados.Remove(estados);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosExists(int id)
        {
          return (_context.Estados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
