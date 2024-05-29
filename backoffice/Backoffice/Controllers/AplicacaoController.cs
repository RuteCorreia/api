using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Aplicacao;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class AplicacaoController : Controller
    {
        private readonly ContextBase _context;

        public AplicacaoController(ContextBase context)
        {
            _context = context;
        }

        // GET: Aplicacao
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Aplicacao.Include(a => a.Cliente).Include(a => a.Cultura).Include(a => a.Empresa).Include(a => a.Executor).Include(a => a.Piloto);
            return View(await contextBase.ToListAsync());
        }

        // GET: Aplicacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aplicacao == null)
            {
                return NotFound();
            }

            var aplicacao = await _context.Aplicacao
                .Include(a => a.Cliente)
                .Include(a => a.Cultura)
                .Include(a => a.Empresa)
                .Include(a => a.Executor)
                .Include(a => a.Piloto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacao == null)
            {
                return NotFound();
            }

            return View(aplicacao);
        }

        // GET: Aplicacao/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Email");
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura");
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome");
            ViewData["IdExecutor"] = new SelectList(_context.Executor, "IdExecutor", "CFTA");
            ViewData["IdPiloto"] = new SelectList(_context.Piloto, "IdPiloto", "CDAC");
            return View();
        }

        // POST: Aplicacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmpresa,StatusEnvio,IdPiloto,IdExecutor,IdCliente,IdCultura")] Aplicacao aplicacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aplicacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Email", aplicacao.IdCliente);
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura", aplicacao.IdCultura);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", aplicacao.IdEmpresa);
            ViewData["IdExecutor"] = new SelectList(_context.Executor, "IdExecutor", "CFTA", aplicacao.IdExecutor);
            ViewData["IdPiloto"] = new SelectList(_context.Piloto, "IdPiloto", "CDAC", aplicacao.IdPiloto);
            return View(aplicacao);
        }

        // GET: Aplicacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aplicacao == null)
            {
                return NotFound();
            }

            var aplicacao = await _context.Aplicacao.FindAsync(id);
            if (aplicacao == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Email", aplicacao.IdCliente);
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura", aplicacao.IdCultura);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", aplicacao.IdEmpresa);
            ViewData["IdExecutor"] = new SelectList(_context.Executor, "IdExecutor", "CFTA", aplicacao.IdExecutor);
            ViewData["IdPiloto"] = new SelectList(_context.Piloto, "IdPiloto", "CDAC", aplicacao.IdPiloto);
            return View(aplicacao);
        }

        // POST: Aplicacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpresa,StatusEnvio,IdPiloto,IdExecutor,IdCliente,IdCultura")] Aplicacao aplicacao)
        {
            if (id != aplicacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aplicacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicacaoExists(aplicacao.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Email", aplicacao.IdCliente);
            ViewData["IdCultura"] = new SelectList(_context.Cultura, "IdCultura", "IdCultura", aplicacao.IdCultura);
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", aplicacao.IdEmpresa);
            ViewData["IdExecutor"] = new SelectList(_context.Executor, "IdExecutor", "CFTA", aplicacao.IdExecutor);
            ViewData["IdPiloto"] = new SelectList(_context.Piloto, "IdPiloto", "CDAC", aplicacao.IdPiloto);
            return View(aplicacao);
        }

        // GET: Aplicacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aplicacao == null)
            {
                return NotFound();
            }

            var aplicacao = await _context.Aplicacao
                .Include(a => a.Cliente)
                .Include(a => a.Cultura)
                .Include(a => a.Empresa)
                .Include(a => a.Executor)
                .Include(a => a.Piloto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacao == null)
            {
                return NotFound();
            }

            return View(aplicacao);
        }

        // POST: Aplicacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aplicacao == null)
            {
                return Problem("Entity set 'ContextBase.Aplicacao'  is null.");
            }
            var aplicacao = await _context.Aplicacao.FindAsync(id);
            if (aplicacao != null)
            {
                _context.Aplicacao.Remove(aplicacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AplicacaoExists(int id)
        {
          return (_context.Aplicacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
