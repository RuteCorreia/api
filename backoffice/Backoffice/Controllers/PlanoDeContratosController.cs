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
    public class PlanoDeContratosController : Controller
    {
        private readonly ContextBase _context;

        public PlanoDeContratosController(ContextBase context)
        {
            _context = context;
        }

        // GET: PlanoDeContratos
        public async Task<IActionResult> Index()
        {
              return _context.PlanoDeContrato != null ? 
                          View(await _context.PlanoDeContrato.ToListAsync()) :
                          Problem("Entity set 'ContextBase.PlanoDeContrato'  is null.");
        }

        // GET: PlanoDeContratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanoDeContrato == null)
            {
                return NotFound();
            }

            var planoDeContrato = await _context.PlanoDeContrato
                .FirstOrDefaultAsync(m => m.IdPlano == id);
            if (planoDeContrato == null)
            {
                return NotFound();
            }

            return View(planoDeContrato);
        }

        // GET: PlanoDeContratos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanoDeContratos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlano,NomeDoPlano")] PlanoDeContrato planoDeContrato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planoDeContrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planoDeContrato);
        }

        // GET: PlanoDeContratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlanoDeContrato == null)
            {
                return NotFound();
            }

            var planoDeContrato = await _context.PlanoDeContrato.FindAsync(id);
            if (planoDeContrato == null)
            {
                return NotFound();
            }
            return View(planoDeContrato);
        }

        // POST: PlanoDeContratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlano,NomeDoPlano")] PlanoDeContrato planoDeContrato)
        {
            if (id != planoDeContrato.IdPlano)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planoDeContrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoDeContratoExists(planoDeContrato.IdPlano))
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
            return View(planoDeContrato);
        }

        // GET: PlanoDeContratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlanoDeContrato == null)
            {
                return NotFound();
            }

            var planoDeContrato = await _context.PlanoDeContrato
                .FirstOrDefaultAsync(m => m.IdPlano == id);
            if (planoDeContrato == null)
            {
                return NotFound();
            }

            return View(planoDeContrato);
        }

        // POST: PlanoDeContratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlanoDeContrato == null)
            {
                return Problem("Entity set 'ContextBase.PlanoDeContrato'  is null.");
            }
            var planoDeContrato = await _context.PlanoDeContrato.FindAsync(id);
            if (planoDeContrato != null)
            {
                _context.PlanoDeContrato.Remove(planoDeContrato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoDeContratoExists(int id)
        {
          return (_context.PlanoDeContrato?.Any(e => e.IdPlano == id)).GetValueOrDefault();
        }
    }
}
