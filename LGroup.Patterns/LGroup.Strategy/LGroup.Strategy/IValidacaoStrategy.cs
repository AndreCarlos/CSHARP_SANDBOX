using System;

using LGroup.Strategy.Model;


namespace LGroup.Strategy
{
    /// <summary>
    /// O padrao Strategy obrigatoriamente tem que ter INTERFACES
    /// Interfaces servem para montar famílias 
    /// FAMILIA SILVA
    /// FAMILIA SOUZA
    /// FAMILIA SANTOS
    /// 
    /// Criamos uma estratégia de validaçao multibano, as regras
    /// de negocio sao de acordo com o banco
    /// Montamos um componente de negócio utilizando o padrao STRATEGY
    /// </summary>
    public interface IValidacaoStrategy
    {
        void ValidarCamposObrigatorios(ClienteModel cliente_);
    }
}
