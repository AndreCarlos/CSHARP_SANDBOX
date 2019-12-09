
//subimos para memoria a dll do NUnit, ela vai habilitar os comandos que vao nos auxiliar a testar o projeto
using NUnit.Framework;

//subimos pra memoria a dll de conexao com o banco (EF)
using LGroup.CodeFirst.DataAccess;

//subimos para memoria a dll de definicao de tabelas

//o visual studio soh resolve por padrao referencias de primeiro nivel
//se uma DLL chama uma outra DLL ele nao consegue resolver. Somos obrigados a importar todas as DLLs da segunda
//DLL em diante. Nao precisa nem dar using

namespace LGroup.CodeFirst.Test.DataAccess
{
    //a microsoft posuui um framework de teste integrado ao visual studio que eh chamado de visual studio
    //unit test framework. O framework de testes mais usado no munndo eh o NUnit (C++, Java, Ruby)
    //eh  um framework de testes disponivel em todas as linguagens de programacao
    [TestFixture]
    public class conexaoTest
    {
        [Test]
        public void TESTAR_CRIACAO_DO_BANCO()
        {
            var conexao = new conexao();
            conexao.CriarBanco();
        }

        [Test]
        public void TESTAR_REMOCAO_DO_BANCO()
        {
            var conexao = new conexao();
            conexao.DeletarBanco();
        }
    }
}
