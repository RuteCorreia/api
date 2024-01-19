using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Frota;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class FrotasController : Controller
    {
        private readonly ContextBase _context;

        public FrotasController(ContextBase context)
        {
            _context = context;
        }

        // GET: Frotas
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Frota.Include(f => f.Empresa);
            return View(await contextBase.ToListAsync());
        }

        // GET: Frotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Frota == null)
            {
                return NotFound();
            }

            var frota = await _context.Frota
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frota == null)
            {
                return NotFound();
            }

            return View(frota);
        }

        // GET: Frotas/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome");
            return View();
        }

        // POST: Frotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEmpresa,NomeVeiculo,Placa,Combustivel,Hodometro")] Frota frota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(frota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", frota.IdEmpresa);
            return View(frota);
        }

        // GET: Frotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Frota == null)
            {
                return NotFound();
            }

            var frota = await _context.Frota.FindAsync(id);
            if (frota == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", frota.IdEmpresa);
            return View(frota);
        }

        // POST: Frotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEmpresa,NomeVeiculo,Placa,Combustivel,Hodometro")] Frota frota)
        {
            if (id != frota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(frota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FrotaExists(frota.Id))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", frota.IdEmpresa);
            return View(frota);
        }

        // GET: Frotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Frota == null)
            {
                return NotFound();
            }

            var frota = await _context.Frota
                .Include(f => f.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (frota == null)
            {
                return NotFound();
            }

            return View(frota);
        }

        // POST: Frotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Frota == null)
            {
                return Problem("Entity set 'ContextBase.Frota'  is null.");
            }
            var frota = await _context.Frota.FindAsync(id);
            if (frota != null)
            {
                _context.Frota.Remove(frota);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrotaExists(int id)
        {
          return (_context.Frota?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
