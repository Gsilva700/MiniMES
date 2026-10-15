using Microsoft.AspNetCore.Mvc;
using MiniMES.Web.Data;
using MiniMES.Web.Models;

namespace MiniMES.Web.Controllers
{
    [ApiController]
    [Route("api/erp")]
    public class IntegracaoErpController : ControllerBase
    {
        private readonly MiniMesContext _db;
        public IntegracaoErpController(MiniMesContext db) => _db = db;

        [HttpPost("ordens")]
        public async Task<IActionResult> ReceberOrdem([FromBody] OrdemErpDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Numero))
                return BadRequest(new { erro = "Número é obrigatório" });

            var maquina = await _db.Maquinas.FindAsync(dto.MaquinaId);
            if (maquina == null)
                return BadRequest(new { erro = $"Máquina {dto.MaquinaId} não encontrada" });

            var ordem = new OrdemProducao
            {
                Numero = dto.Numero,
                Produto = dto.Produto,
                QuantidadePrevista = dto.QuantidadePrevista,
                MaquinaId = dto.MaquinaId,
                Status = "Aberta",
                DataCriacao = DateTime.Now
            };

            _db.OrdensProducao.Add(ordem);
            await _db.SaveChangesAsync();

            return Ok(new { id = ordem.Id, numero = ordem.Numero });
        }

        [HttpGet("producao")]
        public IActionResult ObterProducao()
            => Ok(_db.OrdensProducao.Select(o => new
            {
                o.Numero,
                o.Produto,
                o.QuantidadePrevista,
                o.QuantidadeProduzida,
                o.Status
            }).ToList());
    }

    public class OrdemErpDto
    {
        public string Numero { get; set; } = string.Empty;
        public string Produto { get; set; } = string.Empty;
        public int QuantidadePrevista { get; set; }
        public int MaquinaId { get; set; }
    }
}