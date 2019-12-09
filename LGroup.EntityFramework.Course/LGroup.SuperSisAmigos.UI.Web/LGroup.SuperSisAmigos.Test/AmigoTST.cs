using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LGroup.SuperSisAmigos.Repository;

namespace LGroup.SuperSisAmigos.Test
{
    [TestClass]
    public class AmigoTST
    {
        [TestMethod]
        public void LISTAR_SEXO_TABELA_TB_AMIGO()
        {
            var repositorio = new AmigoREP();
            var amigos = repositorio.Listar();

            //Fizemos uma análise em cima dos dados pra saber se funcionou ou não
            Assert.AreEqual(7, amigos.Count);
        }
    }
}
