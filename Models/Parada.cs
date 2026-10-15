using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class Parada
    {
        public int Id { get; set; }

        public int MaquinaId { get; set; }
        public Maquina? Maquina { get; set; }

        [Display(Name = "Motivo")]
        public int InterrupcaoId { get; set; }
        public Interrupcao? Interrupcao { get; set; }

        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; }
        public string? Observacao { get; set; }
    }
}