using System;
using LGroup.Strategy;
using LGroup.Strategy.Model;

namespace LGroup.Strategy.Business.Contexts
{
    /// <summary>
    /// apos criar a estrategia (interface), temos que criar a classe de 
    /// contexto. É uma classe de incializaçao das estrategias. E ela que 
    /// vai acionar as classes de negócio
    /// </summary>
    public sealed class ValidacaoContext
    {
        private IValidacaoStrategy _estrategia;
        /// <summary>
        /// Quem for acionar a classe de contexto, injeta uma estrategia
        /// BradescoBusiness, SantanderBusiness, ItauBusiness
        /// Sempre que tivermos um super tipo (interface ou classe) (pai)
        /// sempre colocar o pai assim fica polimorfico(abstrato)
        /// podemos passar qualquer classe filha
        /// </summary>
        /// <param name="estrategia_"></param>
        public ValidacaoContext(IValidacaoStrategy estrategia_)
        {
            _estrategia = estrategia_;
        }

        public void Validar(ClienteModel cliente_)
        {
            ///Todas as classes de negocio possuem o comando abaixo(veio do pai == inteface)
            ///neste momento acionamos o comando de validaçao para validar os clientes de 
            ///um determinado banco (o banco que foi enviado no construtor)
            ///IValidacaoStrategy = new ItauBusiness();
            ///IvalidacaoStrategy = new SantanderBusiness();
            _estrategia.ValidarCamposObrigatorios(cliente_);
        }
    }
}
