using System;
using LGroup.Strategy;
using LGroup.Strategy.Model;

///O padrao Strategy é um padrão GoF
///DoFactory -> nível 4
///Com esse padrão conseguimos criar componentes flexíveis == DLL flexiveis
///DLLs que se adequam ao cliente
///Conseguimos criar regras de negócio flexíveis que se adequam de acordo
///com o cliente. 
///A idéia desse padrao é NÃO USAR IFs, ElseIfs e Elses
///e sim usar interface, classes (herança)
///ITAU -> Regras de Negócio do Itau
///

namespace LGroup.Strategy.Business
{
    public sealed class ItauBusiness : IValidacaoStrategy
    {
        public void ValidarCamposObrigatorios(ClienteModel cliente_)
        {
            if (String.IsNullOrWhiteSpace(cliente_.Nome))
                throw new ApplicationException("Informe o Nome");

            if (String.IsNullOrWhiteSpace(cliente_.RG))
                throw new ApplicationException("Informe o RG");

            if (cliente_.DataCadastro >= DateTime.Now)
                throw new ApplicationException("A data não pode ser maior que a data atual");
        }
    }
}
