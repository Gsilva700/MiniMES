using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    public class TurnosController : Controller
    {
        private readonly MiniMesContext _db;
        public TurnosController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Turnos.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Turno turno)
        {
            if (!ModelState.IsValid) return View(turno);
            _db.Turnos.Add(turno);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Turno cadastrado!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var t = await _db.Turnos.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Turno turno)
        {
            if (id != turno.Id) return BadRequest();
            if (!ModelState.IsValid) return View(turno);
            _db.Update(turno);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Turno atualizado!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var t = await _db.Turnos.FindAsync(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t = await _db.Turnos.FindAsync(id);
            if (t != null)
            {
                _db.Turnos.Remove(t);
                await _db.SaveChangesAsync();
                TempData["Sucesso"] = "Turno removido!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}