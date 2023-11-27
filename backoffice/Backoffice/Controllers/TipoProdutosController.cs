using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.Tipo_Produto;
using Infra.Configuracao;

namespace Backoffice.Controllers
{
    public class TipoProdutosController : Controller
    {
        private readonly ContextBase _context;

        public TipoProdutosController(ContextBase context)
        {
            _context = context;
        }

        // GET: TipoProdutos
        public async Task<IActionResult> Index()
        {
              return _context.TipoProduto != null ? 
                          View(await _context.TipoProduto.ToListAsync()) :
                          Problem("Entity set 'ContextBase.TipoProduto'  is null.");
        }

        // GET: TipoProdutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoProduto == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // GET: TipoProdutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoProduto tipoProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProduto);
        }

        // GET: TipoProdutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoProduto == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto.FindAsync(id);
            if (tipoProduto == null)
            {
                return NotFound();
            }
            return View(tipoProduto);
        }

        // POST: TipoProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TipoProduto tipoProduto)
        {
            if (id != tipoProduto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProdutoExists(tipoProduto.Id))
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
            return View(tipoProduto);
        }

        // GET: TipoProdutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoProduto == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // POST: TipoProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoProduto == null)
            {
                return Problem("Entity set 'ContextBase.TipoProduto'  is null.");
            }
            var tipoProduto = await _context.TipoProduto.FindAsync(id);
            if (tipoProduto != null)
            {
                _context.TipoProduto.Remove(tipoProduto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProdutoExists(int id)
        {
          return (_context.TipoProduto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
