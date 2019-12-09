using System;

namespace LGroup.Adapter.Model
{
    public sealed class BoletoModel
    {
        public Int32 Codigo { get; set; }
        public Decimal Valor { get; set; }

        //quem emitiu
        public string  Cedente { get; set; }

        //quem paga
        public string Sacado { get; set; }

        public DateTime DataVencimento { get; set; }
    }
}