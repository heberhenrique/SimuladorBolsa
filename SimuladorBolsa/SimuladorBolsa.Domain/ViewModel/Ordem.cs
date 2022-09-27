using System;
namespace SimuladorBolsa.Domain.ViewModel
{
    public class Ordem
    {
        public Ordem()
        {
        }

        public int IdOrdem { get; set; }
        public int IdCliente { get; set; }
        public string Ticker { get; set; }
        public double ValorOrdem { get; set; }
        public int QuantidadePapeis { get; set; }
        public DateTime DataHoraOrdem { get; set; }
    }
}

