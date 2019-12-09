using System;

/// existem diversos frameworks, bibliotecas de testes no mercado
/// NUNIT, MOQ, RHINO, MOCKS
/// estamos utilizando a da propria Microsoft(visual studio 2008)
/// é o VSUT (visual studio unit test framework)
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// subimos a namespace do projeto de acesso a dados, pois queremos
/// testar a classe de conexao
using LGroup.SuperSisAmigos.DataAcces;

///Subimos esta namespace para habilitar os comandos de pesquisa nas tabelas
using System.Linq;

/// sempre temos que testar nosso projeto, ou da forma manual, braçal do dia a dia
/// algume va olhar o codigo ou da forma automatizada onde criamos um robo (projeto de testes)
/// para testar o nosso codigo, muito menos braçal
/// código testando código

namespace LGroup.SuperSisAmigos.Test.DataAcces
{
    /// <summary>
    /// Foi essa classe que transformou essa classe em um robo de testes 
    /// </summary>
    [TestClass]
    public class ConexaoTest
    {
        /// <summary>
        /// é a classe testMethod que indica que o comando deve ser visivel dentro do robo 
        /// e como se fosse o "public" do robo
        /// </summary>
        [TestMethod]
        public void Testar_Criacao_Do_Banco_De_Dados()
        {
            ///nesse comando de testes queremos testar se o banco vai ser montado com sucesso
            var conexao = new Conexao();
            conexao.GerarBanco();
        }
        [TestMethod]
        public void Testar_Exclusao_Do_Banco_De_Dados()
        {
            /// o visual studio por padrao so consegue visualizar uma DLL conversando com outra 1 para 1
            /// se 1 dll chama outra que chama outra não funciona
            /// ele só consegue visualizar a primeira, da segunda em diante tem que dar ADD Reference.
            var conexao = new Conexao();
            conexao.DeletarBanco();
        }

        [TestMethod]
        public void Testar_Selecao_De_Amigos()
        {
            var conexao = new Conexao();

            //o comando tolist equivale a um select * from tb_amigo
            //o tolist serve para buscar os dados da tabela
            var amigos = conexao.Amigos.ToList();
        }
    }
}
