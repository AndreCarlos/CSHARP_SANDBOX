using System;

///existem diversos frameworks de teste no mercado, por exemplo,
///Watim, Selenium, moq, nsubstitute, rhyno mocks
///a própria Microsoft tem um framework de testes embutido (no vs) que é o 
///visual studio Unit Test Framework (VSUT), o principal e o mais usado
///no mundo é o NUnit (NUGET)
using NUnit.Framework;

using LGroup.Business;
using LGroup.Helper;
using LGroup.Model;
using LGroup.Facade;


///Para que o VS reconheça o robo de testes (NUNIT)
///temos que baixar o INTEGRDOR (Nunit Test Adapter) 
///É ele quem faz a prte do VS com o Nunit
namespace LGroup.Test
{
    /// <summary>
    /// transformamos a classe normal em um classe de testes (virou um robo)
    /// </summary>
    [TestFixture]
    public sealed class PadraoFacadeTest
    {
        /// <summary>
        /// Existem dois tipos de testes:
        /// TESTES MANUAIS
        /// Roaeiro de testes, evidencias, cenário de testes
        /// alguém testando manualmente o software
        /// 
        /// TESTES AUTOMATIZADOS
        /// Vamos criar um robo para testar o nosso código
        /// Codigo testando código
        /// è o robo quem testa e fala se está certo ou errado
        /// </summary>
        /// 
        ///criamos as operaçoes (comandos) que o robo vai executar
        [Test]
        public void Testar_Fluxo_Sem_Facade()
        {
            ///Requisitos, as etapas (fluxo)
            ///1- armazenar os dados
            ///2- validar os dados
            ///3- enviar emails
            ///4- gerar arquivo de texto

            //1-etapa de armazenamento
            var novoCliente = new ClienteModel();
            novoCliente.Nome = "Fulano da Silva";
            novoCliente.Email = "fulano@gmail.com";
            novoCliente.Telefone = "(11) 99876-3433";
            novoCliente.DataNascimento = DateTime.Now;

            //2-etapa de validação
            var negocioCliente = new ClienteBusiness();
            negocioCliente.ValidarCamposObrigatorios(novoCliente);

            //3-etapa enviar email
            EmailHelper.Enviar("andre.leite.carlos@gmail.com", "andre.leite.carlos@gmail.com", "Novo Cliente Cadastrado com Sucesso", "Alguém inseriu um novo cliente sistema");

            //4-Gerar um txt de sucesso de LOG
            ArquivoHelper.Gerar(@"C:\processamento\log.txt", "Cliente Cadastrado com Sucesso");
        }

        [Test]
        public void Testar_Fluxo_Com_Facade()
        {
            ///acionamos a classe do facade, fachada== simplificadora
            ///Uma classe que aciona várias classes
            ///
            var facadeCliente = new ClienteFacade();
            facadeCliente.IniciarCadastro("Zina", "zina@ig.com.br", "(11) 9887-8323", DateTime.Now);
        }
    }
}
