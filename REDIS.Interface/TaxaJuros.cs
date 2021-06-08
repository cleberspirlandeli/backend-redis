using System;
using System.ComponentModel;

namespace REDIS.Interface
{
    public class TaxaJuros
    {
        public double Taxa { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public int Tipo { get; set; }
    }

    public enum TipoTaxaJurosEnum
    {
        [Description("Juros Composto")]
        Composto,

        [Description("Juros Simples")]
        Simples
    }

    public class ChaveTaxaJuros
    {
        private string KeyJurosComposto { get; } = "KeyJurosComposto";
        private string KeyJurosSimples { get; } = "KeyJurosSimples";

        public string GetKeyJurosComposto() => KeyJurosComposto;
        public string GetKeyJurosSimples() => KeyJurosSimples;
    }
}
