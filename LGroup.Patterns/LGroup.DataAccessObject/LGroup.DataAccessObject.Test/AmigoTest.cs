using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using LGroup.DataAccessObject.Model;
using LGroup.DataAccessObject.DataAccessLayer.Implementations;


namespace LGroup.DataAccessObject.Test
{
    [TestClass]
    public class AmigoTest
    {
        [TestMethod]
        public void Testar_Cadastro_De_Amigo()
        {
            var novoAmigo = new AmigoModel();
            novoAmigo.Nome = "Nome 01";
            novoAmigo.Email = "Email 01";
            novoAmigo.CodigoEstadoCivil = 1;
            novoAmigo.CodigoSexo = 1;
            novoAmigo.DataNascimento = DateTime.Now;
            novoAmigo.Salario = 190.10M;

            var daddosAmigo = new AmigoDAO();
            daddosAmigo.Cadastrar(novoAmigo);
        }

        [TestMethod]
        public void Testar_Atualizacao_De_Amigo()
        {
            var daddosAmigo = new AmigoDAO();
            var novoAmigo = daddosAmigo.Pesquisar(x => x.Codigo == 1).Single();

            novoAmigo.Nome = "Nome 02";
            novoAmigo.Email = "Email 02";
            novoAmigo.CodigoEstadoCivil = 2;
            novoAmigo.CodigoSexo = 2;
            novoAmigo.DataNascimento = DateTime.Now;
            novoAmigo.Salario = 190.10M;

            daddosAmigo.Atualizar(novoAmigo);
        }

        [TestMethod]
        public void Testar_Exclusao_De_Amigo()
        {
            var daddosAmigo = new AmigoDAO();
            daddosAmigo.Deletar(1);
        }

         [TestMethod]
        public void Testar_Selecao_De_Amigo()
        {
            var dadosAmigo = new AmigoDAO();
            var amigos = dadosAmigo.Listar();
        }
    }
}
