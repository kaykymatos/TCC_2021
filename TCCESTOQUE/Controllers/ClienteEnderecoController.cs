using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TCCESTOQUE.Data;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Controllers
{
    public class ClienteEnderecoController : Controller
    {
        private readonly TCCESTOQUEContext _context;

        public ClienteEnderecoController(TCCESTOQUEContext context)
        {
            _context = context;
        }

        // GET: ClienteEndereco/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteEnderecoModel = await _context.ClienteEnderecoModel.FindAsync(id);
            if (clienteEnderecoModel == null)
            {
                return NotFound();
            }
            return View(clienteEnderecoModel);
        }

        // POST: ClienteEndereco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClienteEnderecoModel clienteEnderecoModel)
        {
            if (id != clienteEnderecoModel.EnderecoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteEnderecoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteEnderecoModelExists(clienteEnderecoModel.EnderecoId))
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
            return View(clienteEnderecoModel);
        }

        // GET: ClienteEndereco/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteEnderecoModel = await _context.ClienteEnderecoModel
                .FirstOrDefaultAsync(m => m.EnderecoId == id);
            if (clienteEnderecoModel == null)
            {
                return NotFound();
            }

            return View(clienteEnderecoModel);
        }

        // POST: ClienteEndereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var clienteEnderecoModel = await _context.ClienteEnderecoModel.FindAsync(id);
            _context.ClienteEnderecoModel.Remove(clienteEnderecoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteEnderecoModelExists(Guid id)
        {
            return _context.ClienteEnderecoModel.Any(e => e.EnderecoId == id);
        }
    }
}
