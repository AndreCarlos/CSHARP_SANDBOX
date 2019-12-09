using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Strategy;
using LGroup.Strategy.Business;
using LGroup.Strategy.Model;
using NUnit.Framework;
using LGroup.Strategy.Business.Contexts;

namespace LGroup.Strategy.Test
{
    [TestFixture]
    public sealed class ValidacaoTest
    {
        [Test]
        public void Testar_Regras_Validacao_Itau()
        {
            var novoCliente = new ClienteModel();
            novoCliente.Nome = "Nome 01";
            novoCliente.RG = "RG 111";
            novoCliente.DataCadastro = DateTime.Now;

            //Acionamos a classe de contexto (Strategy)
            //A classe de contexto inicializou a estratégia de
            //Validação de clientes do itau
            //Solução mais elegante pra não usar IFS, ELSES IFS

            //Quando trabalhamos com o padrão STRATEGY
            //Automaticamente estamos trabalhando com UM PRINCPIO DE ORIENTAÇÃO A OBJETOS
            //OPEN CLOSED PRINCIPLE (OCP) (Princpio do Aberto e Fechado)
            //Nosso código tem que estar fechado pra modificações e aberto pra novas implementaçoes
            //Não mexe no que já está pronto, só podemos fazer novas implementações (novos
            //Códigos, novas funcionalidades)

            var contexto = new ValidacaoContext(new ItauBusiness());
            contexto.Validar(novoCliente);
        }

        [Test]
        public void Testar_Regras_Validacao_Santander()
        {
            var novoCliente = new ClienteModel();
            novoCliente.Nome = "Nome 01";
            novoCliente.RG = "RG 111";
            novoCliente.DataCadastro = DateTime.Now;
        }

        [Test]
        public void Testar_Regras_Validacao_Bradesco()
        {
            var novoCliente = new ClienteModel();
            novoCliente.Nome = "Nome 01";
            novoCliente.RG = "RG 111";
            novoCliente.DataCadastro = DateTime.Now;

            ///Inversao de controle (IOC)
            ///uma classe internamente nao pode dar new em outra classe
            ///os news (inicializações) devem vir de fora
            ///quem for dar new injeta as subclasses
            ///
            var contexto = new ValidacaoContext(new BradescoBusiness());
            contexto.Validar(novoCliente);
        }

        [Test]
        public void Testar_Desconto_Natal()
        {
            var produto = new ProdutoModel();
            produto.Nome = "TV 3D";
            produto.Valor = 1.500M;

            var contexto = new DescontoContext(new NatalBusiness());
            var valorFinal = contexto.Calcular(produto.Valor);
        }

        [Test]
        public void Testar_Desconto_Black_Friday()
        {
            var produto = new ProdutoModel();
            produto.Nome = "TV 3D";
            produto.Valor = 1000M;

            var contexto = new DescontoContext(new BlackFridayBusiness());
            var valorFinal = contexto.Calcular(produto.Valor);

            ///fizemos uma asserçao (conferencia de resultado) pra saber se
            ///o valor calculado está correto
            ///Nao precisa coloar breakpoint pra testar e conferir o resultado
            Assert.AreEqual(1300, valorFinal);
        }
    }
}
