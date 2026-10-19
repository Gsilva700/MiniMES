using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    public class OperadoresController : Controller
    {
        private readonly MiniMesContext _db;
        public OperadoresController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Operadores.Include(o => o.Turno).ToListAsync());

        public IActionResult Create()
        {
            ViewBag.Turnos = new SelectList(_db.Turnos, "Id", "Nome");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Operador operador)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Turnos = new SelectList(_db.Turnos, "Id", "Nome", operador.TurnoId);
                return View(operador);
            }
            _db.Operadores.Add(operador);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Operador cadastrado!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var o = await _db.Operadores.FindAsync(id);
            if (o == null) return NotFound();
            ViewBag.Turnos = new SelectList(_db.Turnos, "Id", "Nome", o.TurnoId);
            return View(o);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Operador operador)
        {
            if (id != operador.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Turnos = new SelectList(_db.Turnos, "Id", "Nome", operador.TurnoId);
                return View(operador);
            }
            _db.Update(operador);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Operador atualizado!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var o = await _db.Operadores.Include(x => x.Turno).FirstOrDefaultAsync(x => x.Id == id);
            if (o == null) return NotFound();
            return View(o);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var o = await _db.Operadores.FindAsync(id);
            if (o != null)
            {
                _db.Operadores.Remove(o);
                await _db.SaveChangesAsync();
                TempData["Sucesso"] = "Operador removido!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}