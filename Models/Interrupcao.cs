using System.ComponentModel.DataAnnotations;

namespace MiniMES.Web.Models
{
    public class Interrupcao
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Tipo { get; set; } = "Parada";

        public bool Ativa { get; set; } = true;
    }
}