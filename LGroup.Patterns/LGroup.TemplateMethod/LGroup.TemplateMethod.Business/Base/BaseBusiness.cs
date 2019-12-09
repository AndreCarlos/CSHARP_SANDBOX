using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.TemplateMethod.Business.Base
{
    /// <summary>
    /// Cenário::
    /// independentte se é um cliente ou pedido 
    /// 1º validar
    /// Se tudo foi devidamente preenchido
    /// 2º Cadastrar
    /// </summary>
    public abstract class BaseBusiness<TModelo>
    {
        /// <summary>
        /// Qundo implementamos o padrao template method (definir uma sequencia)
        /// se deixarmos os comandos abertos (public) damos a liberdade de algum
        /// programador fazer merda e errar a ordem dos comandos
        /// Pra tirar essa liberdade, colocamos os métodos como protected
        /// Protected só é visível na classe que herdou
        /// Quem dar NEW NÃO ENXERGAM
        /// </summary>
        /// <param name="modelo_"></param>
        /// <returns></returns>
        protected abstract Boolean ValidarCamposObrigatorios(TModelo modelo_);

        protected abstract void Cadastrar(TModelo modelo_);

        ///essa parte é o padrao template method
        ///foi aqui que definimos a sequencia dos comandos
        ///
        public void Iniciar(TModelo modelo_)
        {
            if (ValidarCamposObrigatorios(modelo_))
                Cadastrar(modelo_);
        }
    }
}
