using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMES.Web.Data;
using MiniMES.Web.ViewModels;

namespace MiniMES.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MiniMesContext _db;

        public HomeController(MiniMesContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var apontamentos = await _db.Apontamentos.ToListAsync();
            var paradas = await _db.Paradas.ToListAsync();

            var totalBoa = apontamentos.Sum(a => a.QuantidadeBoa);
            var totalRefugo = apontamentos.Sum(a => a.QuantidadeRefugo);
            var totalGeral = totalBoa + totalRefugo;

            var vm = new DashboardViewModel
            {
                TotalOrdens = await _db.OrdensProducao.CountAsync(),
                TotalProduzido = totalBoa,
                TotalRefugo = totalRefugo,
                Qualidade = totalGeral == 0 ? 0 : Math.Round((double)totalBoa / totalGeral * 100, 2),
                TotalParadas = paradas
                    .Where(p => p.DataFim.HasValue)
                    .Sum(p => (int)(p.DataFim!.Value - p.DataInicio).TotalMinutes),

                ProducaoPorMaquina = apontamentos
                    .GroupBy(a => a.MaquinaId)
                    .Select(g => new ProducaoPorMaquina
                    {
                        Maquina = _db.Maquinas.Find(g.Key)?.Nome ?? $"Máquina {g.Key}",
                        Boa = g.Sum(x => x.QuantidadeBoa),
                        Refugo = g.Sum(x => x.QuantidadeRefugo)
                    }).ToList(),

                ParadasPorMotivo = paradas
                    .Where(p => p.DataFim.HasValue)
                    .GroupBy(p => p.InterrupcaoId)
                    .Select(g => new ParadaPorMotivo
                    {
                        Motivo = _db.Interrupcoes.Find(g.Key)?.Descricao ?? $"Motivo {g.Key}",
                        Minutos = g.Sum(x => (int)(x.DataFim!.Value - x.DataInicio).TotalMinutes)
                    }).ToList()
            };

            return View(vm);
        }
    }
}