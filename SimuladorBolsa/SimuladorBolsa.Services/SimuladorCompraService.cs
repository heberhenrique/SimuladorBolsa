using System;
using Newtonsoft.Json;
using RestSharp;
using SimuladorBolsa.Domain.ViewModel;

namespace SimuladorBolsa.Services
{
    public class SimuladorCompraService
    {
        public const int HorarioInicioPregao = 10;
        public const int HorarioFimPregao = 17;

        public SimuladorCompraService()
        {
        }

        public bool ValidarHorarioOrdem(Ordem ordem)
        {
            bool response = ordem.DataHoraOrdem.Hour >= HorarioInicioPregao &&
                            ordem.DataHoraOrdem.Hour <= HorarioFimPregao;
            return response;
        }

        public bool ValidarQuantidadePapeisOrdem(Ordem ordem)
        {
            if (ordem.QuantidadePapeis <= 0)
                throw new Exception("Valor da ordem não pode ser menor ou igual zero");

            return (ordem.QuantidadePapeis % 100).Equals(0);
        }

        public async Task<bool> ValidarTickerAcao(Ordem ordem)
        {
            var listaAcoes = await ObterListaAcoes();

            return listaAcoes.Any(a => a.CdAcao == ordem.Ticker);
        }

        public virtual async Task<List<Empresa>> ObterListaAcoes()
        {
            List<Empresa> listaAcoes = null;
            RestClient client = new RestClient("https://api-cotacao-b3.labdo.it/api/");
            RestRequest request = new RestRequest();
            request.Method = Method.Get;
            request.Resource = "empresa";

            var response = await client.GetAsync(request);

            if (response.IsSuccessful)
            {
                listaAcoes = ProcessResponse<List<Empresa>>(response.Content);
            }

            return listaAcoes;
        }

        public T ProcessResponse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}

