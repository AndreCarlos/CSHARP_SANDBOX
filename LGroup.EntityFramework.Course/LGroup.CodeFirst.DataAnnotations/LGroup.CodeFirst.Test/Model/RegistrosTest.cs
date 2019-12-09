using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//submios para memoria a bilbioteca NUNIT - testes
using NUnit.Framework;

using LGroup.CodeFirst.DataAccess;
using LGroup.CodeFirst.Model;

//subimos para memoria a biblioteca de geraçao de massa de dados
//alternativa ao FOR
using FizzWare.NBuilder;

namespace LGroup.CodeFirst.Test.Model
{
    [TestFixture]
    public class RegistrosTest
    {
        [Test]
        public void TESTAR_CRIACAO_DE_SEXOS()
        {
            var sexos = new List<SexoModel>();
            sexos.Add(new SexoModel { Descricao = "Feminino" });
            sexos.Add(new SexoModel { Descricao = "Masculino" });

            //EF novidade do ef 6.0 podemos inserir varios registros de uma unica vez
            //não precisa mais ficar fazendo FOR
            var conexao = new conexao();
            conexao.Sexos.AddRange(sexos);

            conexao.SaveChanges();
        }

        [Test]
        public void TESTAR_CRIACAO_DE_ESTADOS_CIVIS()
        {
            var civis = new List<EstadoCivilModel>();
            civis.Add(new EstadoCivilModel { Descricao = "Casado" });
            civis.Add(new EstadoCivilModel { Descricao = "Divorciado" });
            civis.Add(new EstadoCivilModel { Descricao = "Solteiro" });

            //EF novidade do ef 6.0 podemos inserir varios registros de uma unica vez
            //não precisa mais ficar fazendo FOR
            var conexao = new conexao();
            conexao.EstadosCivis.AddRange(civis);

            conexao.SaveChanges();
        }

        [Test]
        public void TESTAR_CRIACAO_DE_AMIGOS()
        {
            //sempre que voce precisar gerar uma massa de dados pra testar
            //um Grid, um combobox em uma ferramenta de geracao de dados chamado
            //NBUILDER, eh uma maneira mais elegante pra nao usar FOR nem inseir
            //qualquer merda na tabela

            //pelo NBUIDER ou vc deixa ele gerar sozinho os dados ele monto no padrao dele
            //CAMPONUMERO (NOME01, NOME02,..) (1,2) (10/10/2010)
            ///ou vc monta manualmente os ddos atraves dos comandos THE (FIRST, PREVIOUS, LAST)
            //seguido de WITH e AND

            var amigos = Builder<AmigoModel>.CreateListOfSize(100)
                                                .All()
                                                    .TheFirst(20)
                                                        .With(x => x.Nome = "Zina")
                                                            .And(x => x.Email = "zina@ig.com.br")
                                                                .And(x => x.Telefone = "zina123")
                                                                    .And(x => x.CodigoSexo = 2)
                                                                       .And(x => x.CodigoEstadoCivil = 1)
                                                .TheNext(80)
                                                    .With(x => x.CodigoSexo = 1)
                                                        .And(x => x.CodigoEstadoCivil = 3)
                                                .Build();

            var conexao = new conexao();
            conexao.Amigos.AddRange(amigos);
            conexao.SaveChanges();
        }
    }
}
