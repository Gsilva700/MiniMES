using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    public class OrdensProducaoController : Controller
    {
        private readonly MiniMesContext _db;
        public OrdensProducaoController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.OrdensProducao.Include(o => o.Maquina).ToListAsync());

        public IActionResult Create()
        {
            ViewBag.Maquinas = new SelectList(_db.Maquinas.Where(m => m.Ativa), "Id", "Nome");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrdemProducao ordem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Maquinas = new SelectList(_db.Maquinas, "Id", "Nome", ordem.MaquinaId);
                return View(ordem);
            }
            ordem.DataCriacao = DateTime.Now;
            _db.OrdensProducao.Add(ordem);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Ordem criada!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var o = await _db.OrdensProducao.FindAsync(id);
            if (o == null) return NotFound();
            ViewBag.Maquinas = new SelectList(_db.Maquinas, "Id", "Nome", o.MaquinaId);
            return View(o);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrdemProducao ordem)
        {
            if (id != ordem.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Maquinas = new SelectList(_db.Maquinas, "Id", "Nome", ordem.MaquinaId);
                return View(ordem);
            }
            _db.Update(ordem);
            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Ordem atualizada!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var o = await _db.OrdensProducao.Include(x => x.Maquina).FirstOrDefaultAsync(x => x.Id == id);
            if (o == null) return NotFound();
            return View(o);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var o = await _db.OrdensProducao.FindAsync(id);
            if (o != null)
            {
                _db.OrdensProducao.Remove(o);
                await _db.SaveChangesAsync();
                TempData["Sucesso"] = "Ordem removida!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}