using System;
using LGroup.Strategy;
using LGroup.Strategy.Model;

namespace LGroup.Strategy.Business
{
    public sealed class BlackFridayBusiness : IDescontoStrategy
    {
        /// <summary>
        /// M significa Money (din din)
        /// </summary>
        /// <param name="valor_"></param>
        /// <returns></returns>
        public decimal CalcularDesconto(decimal valor_)
        {
            return (valor_ * 2) - (valor_ * 0.7M);
        }
    }
}
