using System;
using Newtonsoft.Json;

namespace SimuladorBolsa.Domain.ViewModel
{
    public class Empresa
    {
        public Empresa()
        {
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cd_acao_rdz")]
        public string CdAcaoRdz { get; set; }

        [JsonProperty("nm_empresa")]
        public string NmEmpresa { get; set; }

        [JsonProperty("setor_economico")]
        public string SetorEconomico { get; set; }

        [JsonProperty("subsetor")]
        public string Subsetor { get; set; }

        [JsonProperty("segmento")]
        public string Segmento { get; set; }

        [JsonProperty("segmento_b3")]
        public string SegmentoB3 { get; set; }

        [JsonProperty("nm_segmento_b3")]
        public string NmSegmentoB3 { get; set; }

        [JsonProperty("cd_acao")]
        public string CdAcao { get; set; }

        [JsonProperty("tx_cnpj")]
        public string TxCnpj { get; set; }

        [JsonProperty("vl_cnpj")]
        public string VlCnpj { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

