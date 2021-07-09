using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TCCESTOQUE.Data;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Controllers
{
    public class FornecedorEnderecoController : ControllerPai
    {
        private readonly TCCESTOQUEContext _context;

        public FornecedorEnderecoController(TCCESTOQUEContext context2)
        {
            _context = context2;
        }

        // GET: FornecedorEndereco/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var fornecedorEnderecoModel = await _context.FornecedorEnderecoModel.FindAsync(id);
            if (fornecedorEnderecoModel == null)
                return NotFound();

            ViewData["FornecedorId"] = new SelectList(_context.FornecedorModel, "FornecedorId", "Nome", fornecedorEnderecoModel.FornecedorId);
            return View(fornecedorEnderecoModel);
        }

        // POST: FornecedorEndereco/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, FornecedorEnderecoModel fornecedorEnderecoModel)
        {
            Autenticar();
            if (id != fornecedorEnderecoModel.EnderecoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedorEnderecoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorEnderecoModelExists(fornecedorEnderecoModel.EnderecoId))
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
            ViewData["FornecedorId"] = new SelectList(_context.FornecedorModel, "FornecedorId", "Nome", fornecedorEnderecoModel.FornecedorId);
            return View(fornecedorEnderecoModel);
        }

        // GET: FornecedorEndereco/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            Autenticar();
            if (id == null)
            {
                return NotFound();
            }

            var fornecedorEnderecoModel = await _context.FornecedorEnderecoModel
                .Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(m => m.EnderecoId == id);
            if (fornecedorEnderecoModel == null)
            {
                return NotFound();
            }

            return View(fornecedorEnderecoModel);
        }

        // POST: FornecedorEndereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Autenticar();
            var fornecedorEnderecoModel = await _context.FornecedorEnderecoModel.FindAsync(id);
            _context.FornecedorEnderecoModel.Remove(fornecedorEnderecoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorEnderecoModelExists(Guid id)
        {
            return _context.FornecedorEnderecoModel.Any(e => e.EnderecoId == id);
        }
    }
}
