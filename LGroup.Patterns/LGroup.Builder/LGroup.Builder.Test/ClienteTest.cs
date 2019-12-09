using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.Builder.Model;
using LGroup.Builder.Model.Builder;
using NUnit.Framework;

namespace LGroup.Builder.Test
{
    [TestFixture]
    public sealed class ClienteTest
    {
        [Test]
        public void Testar_Builder_Forma_01()
        {
            ///forma 1 nao é recursiva
            ///
            var builderCliente = new ClienteBuilder();

            //abal
            builderCliente.SetarInformacoesPessoais("zina", "zina@ig.com.br", DateTime.Now);

            //aba2
            builderCliente.SetarTelefone("11", "(11) 99898-2323");

            //aba3
            builderCliente.SetarFoto(@"C:\fotos", "png","Casamento");

            //apos passar todas as etapas, retornamos o cliente devidamente populado
            var cliente = builderCliente.Gerar();
        }
        [Test]
        public void Testar_Builder_Forma_02()
        {
            var builderCliente = new ClienteBuilder();

            //chamamos o builder de forma RECURSIVA
            //Questao de gosto(+ Sênior)

            
            //this e o return na própria CLIENTEBUILDER
            var cliente = builderCliente.SetarInformacoesPessoais("zina", "zina@ig.com.br", DateTime.Now)
                                        .SetarTelefone("11", "(11) 99898-2323")
                                        .SetarFoto(@"C:\fotos", "png", "Casamento")
                                        .SetarFoto(@"C:\fotos", "png", "Casamento")
                                        .Gerar();
        }
    }
}