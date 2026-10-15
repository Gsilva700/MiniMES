using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class Turno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do turno")]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan HoraFim { get; set; }
    }
}