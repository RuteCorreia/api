using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Cidades;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class CidadesController : Controller
    {
        private readonly ContextBase _context;

        public CidadesController(ContextBase context)
        {
            _context = context;
        }

        // GET: Cidades
        public async Task<IActionResult> Index()
        {
              return _context.Cidades != null ? 
                          View(await _context.Cidades.ToListAsync()) :
                          Problem("Entity set 'ContextBase.Cidades'  is null.");
        }

        // GET: Cidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cidades == null)
            {
                return NotFound();
            }

            var cidades = await _context.Cidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidades == null)
            {
                return NotFound();
            }

            return View(cidades);
        }

        // GET: Cidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sigla")] Cidades cidades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cidades);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cidades);
        }

        // GET: Cidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cidades == null)
            {
                return NotFound();
            }

            var cidades = await _context.Cidades.FindAsync(id);
            if (cidades == null)
            {
                return NotFound();
            }
            return View(cidades);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sigla")] Cidades cidades)
        {
            if (id != cidades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cidades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadesExists(cidades.Id))
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
            return View(cidades);
        }

        // GET: Cidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cidades == null)
            {
                return NotFound();
            }

            var cidades = await _context.Cidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cidades == null)
            {
                return NotFound();
            }

            return View(cidades);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cidades == null)
            {
                return Problem("Entity set 'ContextBase.Cidades'  is null.");
            }
            var cidades = await _context.Cidades.FindAsync(id);
            if (cidades != null)
            {
                _context.Cidades.Remove(cidades);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CidadesExists(int id)
        {
          return (_context.Cidades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
