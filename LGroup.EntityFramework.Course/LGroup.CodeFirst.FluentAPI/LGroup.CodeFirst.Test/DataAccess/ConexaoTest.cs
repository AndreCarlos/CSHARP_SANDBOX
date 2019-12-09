using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//subimos para memoria a DLL de testes NUnit
using NUnit.Framework;

//subimos a referencia
using LGroup.CodeFirst.DataAccess;

namespace LGroup.CodeFirst.Test.DataAccess
{
    [TestFixture]
    public sealed class ConexaoTest
    {
        [Test]
        public void TESTAR_CRIACAO_DO_BANCO()
        {
            var conexao = new Conexao();
            conexao.CriarBanco();
        }

        [Test]
        public void TESTAR_EXCLUSAO_DO_BANCO()
        {
            var conexao = new Conexao();
            conexao.ExcluirBanco();
        }
    }
}
