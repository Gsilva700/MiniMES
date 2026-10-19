using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class Maquina
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Display(Name = "Linha de Produção")]
        public string? LinhaProducao { get; set; }

        [Display(Name = "Endereço IP")]
        [RegularExpression(@"^(\d{1,3}\.){3}\d{1,3}$", ErrorMessage = "IP inválido")]
        public string? EnderecoIP { get; set; }

        public bool Ativa { get; set; } = true;
    }
}