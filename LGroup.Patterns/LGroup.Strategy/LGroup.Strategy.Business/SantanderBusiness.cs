using System;
using LGroup.Strategy;
using LGroup.Strategy.Model;

namespace LGroup.Strategy.Business
{
    public sealed class SantanderBusiness : IValidacaoStrategy
    {
        /// <summary>
        /// Para cada banco temos suas regras de negócio, validaçao
        /// especificas 
        /// </summary>
        /// <param name="cliente_"></param>
        public void ValidarCamposObrigatorios(ClienteModel cliente_)
        {
            if (String.IsNullOrWhiteSpace(cliente_.Nome))
                throw new ApplicationException("Informe o Nome");

            if (String.IsNullOrWhiteSpace(cliente_.RG))
                throw new ApplicationException("Informe o RG");
        }
    }
}
