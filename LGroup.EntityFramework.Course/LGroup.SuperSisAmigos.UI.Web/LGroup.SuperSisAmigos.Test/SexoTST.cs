using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LGroup.SuperSisAmigos.Repository;


//VISUAL STUDIO UNIT TEST FRAMEWORK - Conjunto de recursos integrados ao VS pra testar Código, Tela, Http e Banco de Dados
namespace LGroup.SuperSisAmigos.Test
{
    //Pro VS e TFS reconhecerem essa classe como CLASSE DE TESTES DE UNIDADE, temos que colocar o atribibuto TESTCLASS
    [TestClass]
    public class SexoTST
    {
        //Transformamos o nosso método em um CASO DE TESTES
        [TestMethod]
        public void LISTAR_SEXOS()
        {
            var repositorio = new SexoREP();
            var sexos = repositorio.Listar();

            Assert.AreEqual(2, sexos.Count, "Deveria retornar 2(CORRETO)");
        }
    }
}
