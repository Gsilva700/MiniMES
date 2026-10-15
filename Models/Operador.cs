using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class Operador
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Matricula { get; set; } = string.Empty;

        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        public Turno? Turno { get; set; }

        public bool Ativo { get; set; } = true;
    }
}