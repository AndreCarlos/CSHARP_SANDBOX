using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///subimos para memoria o framework de geração de registros fake
///que vio do java nbuilder
using FizzWare.NBuilder;

///tem um padrao do Martin Fowler (2002) que nos ajuda a tratar os dados 
///de classes ou tabelas relacionadas
///AMIGO (SEXO, ESTADO CIVIL)
///PEDIDO (ITENS PEDIDO, PRODUTO, CLIENTE)
///Lazy Loading (trazemos primeiro os dados da classe ou tabela principal)
///para depois buscar os dados da tabela ou classe secundária 
///quando nao usamos lazy loading, o nome é EAGER LOADING
///
///EAGER LOADING (carregamento antecipado), trazemos tudo de uma vez e ao mesmo tempo os dados das
///tabelas ou classes principais e secundárias
///Isso deixa lento, as vezes trazemos muito mais do que precisamos 
///
///LAZY LOADING (Carregamento tardio, demorado) 
///trazemos somente os dados da classe ou tabela principal e quando for necessario,
///trazemos os dados secundários 

namespace LGroup.LazyLoad.Model
{
    public sealed class AmigoModel
    {
        public Int32 Codigo { get; set; }
        public string  Nome { get; set; }
        public string Email { get; set; }


        /// <summary>
        /// Campos relacionados (classes, tabelas)
        /// </summary>
        public Int32 CodigoSexo { get; set; }
        public Int32 CodigoEstadoCivil { get; set; }


        //classes relacionadas (classes ou tabelas)
        private SexoModel _sexo;
        public SexoModel Sexo 
        { 
            get 
            {
                if (_sexo == null)
                    _sexo = new SexoModel();

                _sexo.Codigo = this.CodigoSexo;
                _sexo.Descricao = "Feminino";

                return _sexo;
            }
        }

        private EstadoCivilModel _estadocivil;
        public EstadoCivilModel EstadoCivil
        {
            get
            {
                ///Quando implementamos o lazy loading deixamos somente leitura
                ///GET (tratamos somente a exibição)
                
                ///verificamos se a variavel foi inicializada
                if (_estadocivil == null)
                    _estadocivil = new EstadoCivilModel();

                ///trouxemos os dados e jogamos nessa classe secundaria
                ///classe relacionada
                ///alimentamos a classe secundária em uma segundo momento, etapa 
                _estadocivil.Codigo = this.CodigoEstadoCivil;
                _estadocivil.Descricao = "Casado";

                return _estadocivil;
            }
        }

        /// <summary>
        /// forma padrao - classicona do dia-a-dia
        /// carregamento antecipado
        /// tecnica de programaçao (trazer todas as classes, tabelas ou mesmo)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AmigoModel> ListarAmigosEagerLoading()
        { 
            ///é classico no dia-a-dia precisarmos uma lista simbolica com registros de teste
            ///(carregar um grid, um combo, um grafico, um relatorio) resolver com FOR (LOOPING)
            ///para nao ficar braçal montar na unha vamos usar um framework de geraçao de massa
            ///de dados
            
            //var amigos = Builder<AmigoModel>.CreateListOfSize(10).Build();

            var amigos = new List<AmigoModel>();
            for (int i = 0; i < 10; i++)
            {
                var novoAmigo = new AmigoModel();

                novoAmigo.Codigo = i;
                novoAmigo.Nome = "Nome" + i;
                novoAmigo.Email = "Email" + i;

                novoAmigo.CodigoSexo = 1;
                novoAmigo.CodigoEstadoCivil = 1;

                ///relacionamentos - classes ou tabelas relacionadas
                //novoAmigo.Sexo = new SexoModel();
                //novoAmigo.Sexo.Codigo = 1;
                //novoAmigo.Sexo.Descricao = "Feminino";

                //novoAmigo.EstadoCivil = new EstadoCivilModel();
                //novoAmigo.EstadoCivil.Codigo = 1;
                //novoAmigo.EstadoCivil.Descricao = "Casado";

                amigos.Add(novoAmigo);
            }
            return amigos;
        }

        public IEnumerable<AmigoModel> ListarAmigosLazyLoading()
        {
            var amigos = new List<AmigoModel>();
            for (int i = 0; i < 10; i++)
            {
                var novoAmigo = new AmigoModel();

                novoAmigo.Codigo = i;
                novoAmigo.Nome = "Nome" + i;
                novoAmigo.Email = "Email" + i;

                novoAmigo.CodigoSexo = 1;
                novoAmigo.CodigoEstadoCivil = 1;

                ///no lazy loading, trazemos apenas os dados da classe principal
                ///nao precisa trazer das classes relacionadas (secundárias)
                ///isso é performance (exibimos somente o necessario) e quando precisamos
                ///
                amigos.Add(novoAmigo);
            }
            return amigos;
        }
    }
}
