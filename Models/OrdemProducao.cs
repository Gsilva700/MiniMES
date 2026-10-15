using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class OrdemProducao
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Numero { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Produto { get; set; } = string.Empty;

        [Display(Name = "Qtd. Prevista")]
        public int QuantidadePrevista { get; set; }

        [Display(Name = "Qtd. Produzida")]
        public int QuantidadeProduzida { get; set; }

        public string Status { get; set; } = "Aberta";

        [Display(Name = "Máquina")]
        public int MaquinaId { get; set; }
        public Maquina? Maquina { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}