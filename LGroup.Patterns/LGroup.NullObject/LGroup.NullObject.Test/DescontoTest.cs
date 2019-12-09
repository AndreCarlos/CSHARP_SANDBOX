using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LGroup.NullObject.Business;
using LGroup.NullObject.Model;
using LGroup.NullObject.Business.Base;

namespace LGroup.NullObject.Test
{
    [TestClass]
    public class DescontoTest
    {
        [TestMethod]
        public void Testar_Desconto_Produto()
        {
            var novoProduto = new ProdutoModel();

            novoProduto.Nome = "Rexona";
            novoProduto.Valor = 200;

            ///dependendo do dia, damos 1 desconto diferente
            ///classe de negocio para acionar
            ///
            var dataCorrente = new DateTime(2016, 04, 09);

            ///ele eh o pai das classes de negocio
            ///tem um principio de orientaçao a objeto faz parte do SOLID
            ///L = prinicipio de Liskov
            ///pro codigo ficar flexivel e facil de dar manutençao 
            ///um pai pode ser substituido por um filho
            ///aonde tem um pai podemos dar um new em uma classe filha
            IDesconto negocio = null;

            if(dataCorrente.ToString("ddMM") == "2512")
            {
                negocio = new DescontoNatalBusiness();
            }
            else if(dataCorrente.ToString("ddMM") == "0911")
            {
                negocio = new DescontoBlackFridayBusiness();
            }
            else
            {
                negocio = new DescontoNullObjectBusiness();
            }

            ///calculamos o desconto
            ///no dia a dia é classico tomar erros de null reference
            ///significa que nao inicializamos a variavel (nao damos new)
            ///e quando tentamos usar um variável que é null tomamos erro
            ///e para resolver colocamos try catch == porco
            ///ou colocamos if == null (classicao)
            var novoValor = negocio.Calcular(novoProduto.Valor);
        }
    }
}