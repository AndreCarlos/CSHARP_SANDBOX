using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LGroup.TemplateMethod.Model;
using LGroup.TemplateMethod.Business;


///A Microsoft tem seu proprio framework de teste que se chama VSUT - visual studio test framework
///ja vem com o visual studio, nao precisa baixar nada e tudo se integra
///
namespace LGroup.TemplateMethod.Test
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void Testar_Fluxo_Cliente_Sem_Tempplate_Method()
        {
            var novoCliente = new ClienteModel();
            novoCliente.Nome = "None 01";
            novoCliente.Telefone = "Telefone 01";

            var negocioCliente = new ClienteBusiness();

            ///Antes de mudar para protected era o código abaixo
            
            ///Deixando o codigo sem template method pode-se errar 
            ///alguem pode fazer 3 tipos de cagada
            ///1º inverter a orde de comandos (cadastrar -> validar)
            ///2º esquecer de chamar os comandos (validar)
            ///3º Pode-se errar na consistência, integridade (sem o IF == REGRA)
            //if(negocioCliente.ValidarCamposObrigatorios(novoCliente))
            //    negocioCliente.Cadastrar(novoCliente);
        }

        [TestMethod]
        public void Testar_Fluxo_Cliente_Com_Tempplate_Method()
        {
            ///O padrao template method (GoF = 3)
            ///a proposta do padrao  é definir uma sequencia de comandos que serão executados um após
            ///o outro. Definimos a ordem de execução dos comandos
            ///1º metodoA , 2º metodoB, 3º metodoC
            ///conseguimos garantir que todos os programadores sempre chamem
            ///Nessa sequencia e ninguem erre a ordem
            ///

            var novoCliente = new ClienteModel();
            novoCliente.Nome = "None 01";
            novoCliente.Telefone = "Telefone 01";

            ///Essa é a chamada do padrao template method
            ///Visualizamos um único comando (Iniciar)
            ///E lá dentro dele é que está definido o ESQUELETO (SEQUENCIA)
            ///
            ///No TEMPLATE METHOD a classe pai desce e aciona os comandos
            ///das classes filhas(classe corrente == Está Aberta)
            var negocioCliente = new ClienteBusiness();
            negocioCliente.Iniciar(novoCliente);
        }
    }
}
