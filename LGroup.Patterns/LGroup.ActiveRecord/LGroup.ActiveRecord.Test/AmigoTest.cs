using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LGroup.ActiveRecord.Model;
using System.Linq;

namespace LGroup.ActiveRecord.Test
{
    [TestClass]
    public class AmigoTest
    {
        [TestMethod]
        public void Testar_Cadastro()
        {
            var novoAmigo = new AmigoModel();
            //novoAmigo.Nome = "Nome 01";
            //novoAmigo.Email = "Email 01";
            //novoAmigo.DataNascimento = DateTime.Now;
            //novoAmigo.CodigoSexo = 1;

            novoAmigo.Cadastrar();
        }

        [TestMethod]
        public void Testar_Atualizacao()
        {
            var amigo = new AmigoModel();
            var registro = amigo.Listar().First();

            amigo.Nome = "Nome 02";
            amigo.Email = "Email 02";

            amigo.Atualizar();
        }

        [TestMethod]
        public void Testar_Exclusao()
        {
            var amigo = new AmigoModel();
            amigo.Codigo = 2;

            amigo.Deletar();
        }

        [TestMethod]
        public void Testar_Selecao()
        {
            var amigos = new AmigoModel().Listar();      
        }
    }
}
