using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Empresa;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class BulasController : Controller
    {
        private readonly ContextBase _context;

        public BulasController(ContextBase context)
        {
            _context = context;
        }

        // GET: Bulas
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Bula.Include(b => b.AlvoBiologico).Include(b => b.Cultura);
            return View(await contextBase.ToListAsync());
        }

        // GET: Bulas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bula == null)
            {
                return NotFound();
            }

            var bula = await _context.Bula
                .Include(b => b.AlvoBiologico)
                .Include(b => b.Cultura)
                .FirstOrDefaultAsync(m => m.IdBula == id);
            if (bula == null)
            {
                return NotFound();
            }

            return View(bula);
        }

        // GET: Bulas/Create
        public IActionResult Create()
        {
            ViewData["IdAlvoBiologico"] = new SelectList(_context.AlvoBiologico, "Id", "DoseProdutoPorHectare");
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura");
            return View();
        }

        // POST: Bulas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBula,NomeProduto,IdCultura,IdClassificacaoToxicologica,Classe,TipoDeFormulacao,IdAlvoBiologico,DoseProdutoComercial,Adjuvante,IdTipoDeServico")] Bula bula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlvoBiologico"] = new SelectList(_context.AlvoBiologico, "Id", "DoseProdutoPorHectare", bula.IdAlvoBiologico);
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura", bula.IdCultura);
            return View(bula);
        }

        // GET: Bulas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bula == null)
            {
                return NotFound();
            }

            var bula = await _context.Bula.FindAsync(id);
            if (bula == null)
            {
                return NotFound();
            }
            ViewData["IdAlvoBiologico"] = new SelectList(_context.AlvoBiologico, "Id", "DoseProdutoPorHectare", bula.IdAlvoBiologico);
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura", bula.IdCultura);
            return View(bula);
        }

        // POST: Bulas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBula,NomeProduto,IdCultura,IdClassificacaoToxicologica,Classe,TipoDeFormulacao,IdAlvoBiologico,DoseProdutoComercial,Adjuvante,IdTipoDeServico")] Bula bula)
        {
            if (id != bula.IdBula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BulaExists(bula.IdBula))
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
            ViewData["IdAlvoBiologico"] = new SelectList(_context.AlvoBiologico, "Id", "DoseProdutoPorHectare", bula.IdAlvoBiologico);
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura", bula.IdCultura);
            return View(bula);
        }

        // GET: Bulas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bula == null)
            {
                return NotFound();
            }

            var bula = await _context.Bula
                .Include(b => b.AlvoBiologico)
                .Include(b => b.Cultura)
                .FirstOrDefaultAsync(m => m.IdBula == id);
            if (bula == null)
            {
                return NotFound();
            }

            return View(bula);
        }

        // POST: Bulas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bula == null)
            {
                return Problem("Entity set 'ContextBase.Bula'  is null.");
            }
            var bula = await _context.Bula.FindAsync(id);
            if (bula != null)
            {
                _context.Bula.Remove(bula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BulaExists(int id)
        {
          return (_context.Bula?.Any(e => e.IdBula == id)).GetValueOrDefault();
        }
    }
}
