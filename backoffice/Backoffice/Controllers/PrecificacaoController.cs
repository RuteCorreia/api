using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Precificacao;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class PrecificacaoController : Controller
    {
        private readonly ContextBase _context;

        public PrecificacaoController(ContextBase context)
        {
            _context = context;
        }

        // GET: Precificacao
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Precificacao.Include(p => p.Empresa);
            return View(await contextBase.ToListAsync());
        }

        // GET: Precificacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Precificacao == null)
            {
                return NotFound();
            }

            var precificacao = await _context.Precificacao
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precificacao == null)
            {
                return NotFound();
            }

            return View(precificacao);
        }

        // GET: Precificacao/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome");
            return View();
        }

        // POST: Precificacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmpresa,DistanciaPista,PrecoHA")] Precificacao precificacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(precificacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", precificacao.IdEmpresa);
            return View(precificacao);
        }

        // GET: Precificacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Precificacao == null)
            {
                return NotFound();
            }

            var precificacao = await _context.Precificacao.FindAsync(id);
            if (precificacao == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", precificacao.IdEmpresa);
            return View(precificacao);
        }

        // POST: Precificacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpresa,DistanciaPista,PrecoHA")] Precificacao precificacao)
        {
            if (id != precificacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(precificacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrecificacaoExists(precificacao.Id))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", precificacao.IdEmpresa);
            return View(precificacao);
        }

        // GET: Precificacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Precificacao == null)
            {
                return NotFound();
            }

            var precificacao = await _context.Precificacao
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (precificacao == null)
            {
                return NotFound();
            }

            return View(precificacao);
        }

        // POST: Precificacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Precificacao == null)
            {
                return Problem("Entity set 'ContextBase.Precificacao'  is null.");
            }
            var precificacao = await _context.Precificacao.FindAsync(id);
            if (precificacao != null)
            {
                _context.Precificacao.Remove(precificacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrecificacaoExists(int id)
        {
          return (_context.Precificacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
