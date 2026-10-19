using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class Apontamento
    {
        public int Id { get; set; }

        [Display(Name = "Ordem de Produção")]
        public int OrdemProducaoId { get; set; }
        public OrdemProducao? OrdemProducao { get; set; }

        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }
        public Maquina? Maquina { get; set; }

        [Display(Name = "Operador")]
        public int OperadorId { get; set; }
        public Operador? Operador { get; set; }

        [Display(Name = "Qtd. Boa")]
        public int QuantidadeBoa { get; set; }

        [Display(Name = "Qtd. Refugo")]
        public int QuantidadeRefugo { get; set; }

        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
    }
}