using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    public class ApontamentosController : Controller
    {
        private readonly MiniMesContext _db;
        public ApontamentosController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
            => View(await _db.Apontamentos
                .Include(a => a.Maquina)
                .Include(a => a.Operador)
                .Include(a => a.OrdemProducao)
                .OrderByDescending(a => a.DataInicio)
                .ToListAsync());

        public IActionResult Create()
        {
            CarregarCombos();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Apontamento apontamento)
        {
            if (!ModelState.IsValid)
            {
                CarregarCombos();
                return View(apontamento);
            }

            apontamento.DataInicio = DateTime.Now;
            _db.Apontamentos.Add(apontamento);

            var op = await _db.OrdensProducao.FindAsync(apontamento.OrdemProducaoId);
            if (op != null)
            {
                op.QuantidadeProduzida += apontamento.QuantidadeBoa;
                op.Status = op.QuantidadeProduzida >= op.QuantidadePrevista ? "Concluida" : "EmProducao";
            }

            await _db.SaveChangesAsync();
            TempData["Sucesso"] = "Apontamento registrado!";
            return RedirectToAction(nameof(Index));
        }

        private void CarregarCombos()
        {
            ViewBag.Ordens = new SelectList(_db.OrdensProducao.Where(o => o.Status != "Concluida"), "Id", "Numero");
            ViewBag.Maquinas = new SelectList(_db.Maquinas.Where(m => m.Ativa), "Id", "Nome");
            ViewBag.Operadores = new SelectList(_db.Operadores.Where(o => o.Ativo), "Id", "Nome");
        }
    }
}