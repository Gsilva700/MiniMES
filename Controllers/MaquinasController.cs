using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    public class MaquinasController : Controller
    {
        private readonly MiniMesContext _db;
        public MaquinasController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Maquinas.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Maquina maquina)
        {
            if (!ModelState.IsValid) return View(maquina);
            _db.Maquinas.Add(maquina);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Máquina cadastrada!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var m = await _db.Maquinas.FindAsync(id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Maquina maquina)
        {
            if (id != maquina.Id) return BadRequest();
            if (!ModelState.IsValid) return View(maquina);
            _db.Update(maquina);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Máquina atualizada!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var m = await _db.Maquinas.FindAsync(id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _db.Maquinas.FindAsync(id);
            if (m != null)
            {
                _db.Maquinas.Remove(m);
                await _db.SaveChangesAsync();
                TempData["Sucesso"] = "Máquina removida!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}