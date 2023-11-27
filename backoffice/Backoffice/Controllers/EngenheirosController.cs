using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Engenheiro;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class EngenheirosController : Controller
    {
        private readonly ContextBase _context;

        public EngenheirosController(ContextBase context)
        {
            _context = context;
        }

        // GET: Engenheiros
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Engenheiro.Include(e => e.Empresa);
            return View(await contextBase.ToListAsync());
        }

        // GET: Engenheiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Engenheiro == null)
            {
                return NotFound();
            }

            var engenheiro = await _context.Engenheiro
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.IdEngenheiro == id);
            if (engenheiro == null)
            {
                return NotFound();
            }

            return View(engenheiro);
        }

        // GET: Engenheiros/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome");
            return View();
        }

        // POST: Engenheiros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEngenheiro,IdEmpresa,Nome,Email,Senha,CREA,Assinatura")] Engenheiro engenheiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(engenheiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", engenheiro.IdEmpresa);
            return View(engenheiro);
        }

        // GET: Engenheiros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Engenheiro == null)
            {
                return NotFound();
            }

            var engenheiro = await _context.Engenheiro.FindAsync(id);
            if (engenheiro == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", engenheiro.IdEmpresa);
            return View(engenheiro);
        }

        // POST: Engenheiros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEngenheiro,IdEmpresa,Nome,Email,Senha,CREA,Assinatura")] Engenheiro engenheiro)
        {
            if (id != engenheiro.IdEngenheiro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engenheiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EngenheiroExists(engenheiro.IdEngenheiro))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", engenheiro.IdEmpresa);
            return View(engenheiro);
        }

        // GET: Engenheiros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Engenheiro == null)
            {
                return NotFound();
            }

            var engenheiro = await _context.Engenheiro
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.IdEngenheiro == id);
            if (engenheiro == null)
            {
                return NotFound();
            }

            return View(engenheiro);
        }

        // POST: Engenheiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Engenheiro == null)
            {
                return Problem("Entity set 'ContextBase.Engenheiro'  is null.");
            }
            var engenheiro = await _context.Engenheiro.FindAsync(id);
            if (engenheiro != null)
            {
                _context.Engenheiro.Remove(engenheiro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EngenheiroExists(int id)
        {
          return (_context.Engenheiro?.Any(e => e.IdEngenheiro == id)).GetValueOrDefault();
        }
    }
}
