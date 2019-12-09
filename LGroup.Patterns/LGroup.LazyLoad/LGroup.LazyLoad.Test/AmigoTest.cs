using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LGroup.LazyLoad.Model;
using System.Linq;

namespace LGroup.LazyLoad.Test
{
    [TestClass]
    public class AmigoTest
    {
        [TestMethod]
        public void Testar_Amigo_Com_EagerLoading()
        {
            ///Isso é lento (totalmente desnecessário) mas todo mundo faz!
            ///eager e lazy sao para exibiçao (leitura de dados)
            var amigos = new AmigoModel().ListarAmigosEagerLoading();
        }

        [TestMethod]
        public void Testar_Amigo_Com_LazyLoading()
        {
            var amigos = new AmigoModel().ListarAmigosLazyLoading();

            ///Pegamos o primeiro amigo
            var primeiroAmigo = amigos.First();

            //Fizemos o lazy loading e leitura dos dados, classes e tabelas
            //Relacionadas
            //Quando precisamos dos dados relacionados mandamos buscr
            var sexo = primeiroAmigo.Sexo;
            var estadoCivil = primeiroAmigo.EstadoCivil;

        }

        [TestMethod]
        public void Testar_Tipo_Lazy()
        {
        
            //quando declaramos uma variavel, naquele momento el sobre pra memoria
            //Podemos estar utilizando memoram RAM no momento
            //Para inicializar ela somente quando for necessaria
            //Declaramos com LAZY
            //quando acessamos alguma informação da classe
            //ai sim ela sobe para memoria
            var amigo = new Lazy<AmigoModel>();

            //nao subiu para memoria
            var status01 = amigo.IsValueCreated;

            amigo.Value.Nome = "Nome no Lazy";

            //como usmos algo da classe ela subiu para memoria
            var status02 = amigo.IsValueCreated;
        }
    }
}
