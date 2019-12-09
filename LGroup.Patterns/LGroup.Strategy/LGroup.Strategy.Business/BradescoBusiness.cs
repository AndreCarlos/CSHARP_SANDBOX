using System;
using LGroup.Strategy;
using LGroup.Strategy.Model;

namespace LGroup.Strategy.Business
{
    public sealed class BradescoBusiness : IValidacaoStrategy
    {
        public void ValidarCamposObrigatorios(ClienteModel cliente_)
        {
            if (String.IsNullOrWhiteSpace(cliente_.Nome))
                throw new ApplicationException("Informe o Nome");

            if (cliente_.DataCadastro <= DateTime.Now)
                throw new ApplicationException("A data não pode ser maior que a data atual");
        }
    }
}
