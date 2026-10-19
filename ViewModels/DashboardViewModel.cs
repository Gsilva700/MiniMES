namespace MiniMES.Web.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalOrdens { get; set; }
        public int TotalProduzido { get; set; }
        public int TotalRefugo { get; set; }
        public double Qualidade { get; set; }
        public int TotalParadas { get; set; }
        public List<ProducaoPorMaquina> ProducaoPorMaquina { get; set; } = new();
        public List<ParadaPorMotivo> ParadasPorMotivo { get; set; } = new();
    }

    public class ProducaoPorMaquina
    {
        public string Maquina { get; set; } = string.Empty;
        public int Boa { get; set; }
        public int Refugo { get; set; }
    }

    public class ParadaPorMotivo
    {
        public string Motivo { get; set; } = string.Empty;
        public int Minutos { get; set; }
    }
}