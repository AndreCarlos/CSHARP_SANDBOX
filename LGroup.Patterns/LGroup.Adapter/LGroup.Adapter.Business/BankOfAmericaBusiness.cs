using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Adapter.Model;

///subimos para memoria a dll da emissao de boleto do BOA
using BankOfAmerica;

namespace LGroup.Adapter.Business
{
    /// <summary>
    /// Quando utilizamos o padrao adapter para cada dll de terceiros
    /// temos que criar uma classe
    /// como temos 2 bancos gringos (2 dlls) = 2 classes
    /// mesmo sendo uma classe de boleto gringo ainda assim tem que fazer parte da familia
    /// </summary>
    public sealed class BankOfAmericaBusiness : Base.IBoletoBusiness
    {

        /// <summary>
        /// começamos a fazer a adaptação
        /// a nossa classe de negocio, aciona a classe verdadeira (BOA)
        /// </summary>
        /// <param name="boleto_"></param>
        /// 

        ///com  a palavra readonly, uma vez que dermos um new na classe, não podemos
        ///dar new de novo, ele bloqueia futuras inicializações e por traz dos panos
        ///a CLR (maquina virtual) sempre joga a variavel no mesmo endereçador de memoria (
        ///espaço de memoria) (gaveta)
        private readonly Billet _boletoBOA = new Billet();


        /// <summary>
        /// nao importa se é um boleto nacional ou gringo, o comando de EmitirBoleto e ele
        /// por tras dos panos aciona o comando da classe verdadeira
        /// isso é uma adaptaçao. Os dados chegaram como boletomodel e sairam como decimal e datetime
        /// </summary>
        /// <param name="boleto_"></param>
        public void EmitirBoleto(BoletoModel boleto_)
        {
            ///para gente boleto é BOLETOMODEL
            ///para os gringos é BILLET e outros dados
            ///sao classes totalmente incompatíveis, nomes 
            ///
            _boletoBOA.Send(boleto_.Valor, boleto_.DataVencimento);
        }
    }
}
