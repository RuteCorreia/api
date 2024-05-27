using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Piloto;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class PilotosController : Controller
    {
        private readonly ContextBase _context;

        public PilotosController(ContextBase context)
        {
            _context = context;
        }

        // GET: Pilotos
        public async Task<IActionResult> Index()
        {
            var contextBase = _context.Piloto.Include(p => p.Empresa);
            return View(await contextBase.ToListAsync());
        }

        // GET: Pilotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Piloto == null)
            {
                return NotFound();
            }

            var piloto = await _context.Piloto
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.IdPiloto == id);
            if (piloto == null)
            {
                return NotFound();
            }

            return View(piloto);
        }

        // GET: Pilotos/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome");
            return View();
        }

        // POST: Pilotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPiloto,IdEmpresa,NomePiloto,Email,Senha,CDAC,Assinatura,PorcentagemComissao")] Piloto piloto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piloto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", piloto.IdEmpresa);
            return View(piloto);
        }

        // GET: Pilotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Piloto == null)
            {
                return NotFound();
            }

            var piloto = await _context.Piloto.FindAsync(id);
            if (piloto == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", piloto.IdEmpresa);
            return View(piloto);
        }

        // POST: Pilotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPiloto,IdEmpresa,NomePiloto,Email,Senha,CDAC,Assinatura,PorcentagemComissao")] Piloto piloto)
        {
            if (id != piloto.IdPiloto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piloto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PilotoExists(piloto.IdPiloto))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresa, "IdEmpresa", "Nome", piloto.IdEmpresa);
            return View(piloto);
        }

        // GET: Pilotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Piloto == null)
            {
                return NotFound();
            }

            var piloto = await _context.Piloto
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.IdPiloto == id);
            if (piloto == null)
            {
                return NotFound();
            }

            return View(piloto);
        }

        // POST: Pilotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Piloto == null)
            {
                return Problem("Entity set 'ContextBase.Piloto'  is null.");
            }
            var piloto = await _context.Piloto.FindAsync(id);
            if (piloto != null)
            {
                _context.Piloto.Remove(piloto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PilotoExists(int id)
        {
          return (_context.Piloto?.Any(e => e.IdPiloto == id)).GetValueOrDefault();
        }
    }
}
