using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Executor;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class ExecutoresController : Controller
    {
        private readonly ContextBase _context;

        public ExecutoresController(ContextBase context)
        {
            _context = context;
        }

        // GET: Executores
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Executor.Include(e => e.Empresa);
            return View(await contextBase.ToListAsync());
        }

        // GET: Executores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Executor == null)
            {
                return NotFound();
            }

            var executor = await _context.Executor
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.IdExecutor == id);
            if (executor == null)
            {
                return NotFound();
            }

            return View(executor);
        }

        // GET: Executores/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome");
            return View();
        }

        // POST: Executores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExecutor,IdEmpresa,Nome,Email,Senha,CFTA,Assinatura")] Executor executor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(executor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", executor.IdEmpresa);
            return View(executor);
        }

        // GET: Executores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Executor == null)
            {
                return NotFound();
            }

            var executor = await _context.Executor.FindAsync(id);
            if (executor == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", executor.IdEmpresa);
            return View(executor);
        }

        // POST: Executores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdExecutor,IdEmpresa,Nome,Email,Senha,CFTA,Assinatura")] Executor executor)
        {
            if (id != executor.IdExecutor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(executor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExecutorExists(executor.IdExecutor))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", executor.IdEmpresa);
            return View(executor);
        }

        // GET: Executores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Executor == null)
            {
                return NotFound();
            }

            var executor = await _context.Executor
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.IdExecutor == id);
            if (executor == null)
            {
                return NotFound();
            }

            return View(executor);
        }

        // POST: Executores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Executor == null)
            {
                return Problem("Entity set 'ContextBase.Executor'  is null.");
            }
            var executor = await _context.Executor.FindAsync(id);
            if (executor != null)
            {
                _context.Executor.Remove(executor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExecutorExists(int id)
        {
          return (_context.Executor?.Any(e => e.IdExecutor == id)).GetValueOrDefault();
        }
    }
}
