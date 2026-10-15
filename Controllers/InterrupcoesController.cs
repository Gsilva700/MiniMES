using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    public class InterrupcoesController : Controller
    {
        private readonly MiniMesContext _db;
        public InterrupcoesController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Interrupcoes.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Interrupcao i)
        {
            if (!ModelState.IsValid) return View(i);
            _db.Interrupcoes.Add(i);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Interrupção cadastrada!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var i = await _db.Interrupcoes.FindAsync(id);
            if (i == null) return NotFound();
            return View(i);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Interrupcao interrupcao)
        {
            if (id != interrupcao.Id) return BadRequest();
            if (!ModelState.IsValid) return View(interrupcao);
            _db.Update(interrupcao);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Interrupção atualizada!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var i = await _db.Interrupcoes.FindAsync(id);
            if (i == null) return NotFound();
            return View(i);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var i = await _db.Interrupcoes.FindAsync(id);
            if (i != null)
            {
                _db.Interrupcoes.Remove(i);
                await _db.SaveChangesAsync();
                TempData["Sucesso"] = "Interrupção removida!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}