using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.CodeFirst.Repository;
using LGroup.CodeFirst.Repository.Contracts;

namespace LGroup.CodeFirst.UnitOfWork
{
    //a unidade de trabalho (UOW) é um padrao de projeto. Eh um padrao arquitetural. A ideia dela é agrupar
    //todas as classes de repositorio facilitando as chamadas. Todas as classes de repositorio envilidada em 
    //1 pedido, agrupando toos os repositorios na UOW
    //fluxo ==> CONTROLLER -> "BUSINNES ACCESS" -> UNIT OF WORK -> REPOSITORY -> DATAACCESS
    public sealed class PedidoUnitOfWork
    {
        //Essas variaveis vao armazenas os objetos que vieram inicializados do Ninject
        private IClienteRepository _repositorioCliente;
        private IPedidoRepository _repositorioPedido;
        private IProdutoRepository _repositorioProduto;
        private IItemPedidoRepository _repositorioItem;

        //pensando no IoC (inversao de controle) já estruturamso de forma que os repositorios já venham
        //injetados (NINJECT)
        //Um dos beneficios eh a facilidade para manipular as tabelas e fazer o CRUD
        public PedidoUnitOfWork(IClienteRepository repositorioCliente_, IPedidoRepository repositorioPedido_,
                                IProdutoRepository repositorioProduto_, ItemPedidoRepository repositorioItem_)
        {
            _repositorioCliente = repositorioCliente_;
            _repositorioPedido = repositorioPedido_;
            _repositorioProduto = repositorioProduto_;
            _repositorioItem = repositorioItem_;
        }

        //Criamos propriedades para acessar os repositorios. Quando o controller acionar a unidade
        //de trabalho ele visualiza todos os repositorios que sao manipulados a partir da UOW e que foram
        //Inicializados no moldulo BOOTSTRAPPER do NINJECT

        //deixamos somente a leitura, nao precisa da gravacao SET pois a inicializaçao
        //Está sendo feito no Ninject
        public IClienteRepository RepositorioCliente
        {
            get
            {
                return _repositorioCliente;
            }
        }
        public IPedidoRepository RepositorioPedido
        {
            get
            {
                return _repositorioPedido;
            }
        }
        public IProdutoRepository RepositorioProduto
        {
            get
            {
                return _repositorioProduto;
            }
        }
        public IItemPedidoRepository RepositorioItem
        {
            get
            {
                return _repositorioItem;
            }
        }
    }
}