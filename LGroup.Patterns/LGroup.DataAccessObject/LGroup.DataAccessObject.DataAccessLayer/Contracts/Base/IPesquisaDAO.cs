using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///importamos esta namespace pr habilitar a classe EXPRESSION
///a classe expression nos auxilia a montar expressoes, lambdas (x=>x)
///
using System.Linq.Expressions;

namespace LGroup.DataAccessObject.DataAccessLayer.Contracts.Base
{
    /// <summary>
    /// quase todo mundo (*) no dia-a-dia cria diversos comandos de pesquisa
    /// PesquisarTodos, PesquisarPorData, PesquisaEmailSenha...
    /// Fica poluido e dificil de dar manutençao
    /// vamos criar um único pesquisr que fica genérico e flexível para atender
    /// qualquer tipo de pesquisa os parametros de entrada devem ficar flexíveis
    /// </summary>
    public interface IPesquisaDAO<TModelo >
    {
        /// <summary>
        /// Pesquisar (x => x.Codigo == 1)
        /// 
        /// Desmembrando
        /// EXPRESSION - para montar a lambda (x => x)
        /// FUNC - para definir o nome da classe, tabela que queremos pesquisar
        /// TMODELO - Nome da tabela ou classe
        /// BOOLEAN - como estamos fazendo filtros (comparação)
        /// Isso é um boolean, se a condição for verdadeira é true se nao false
        /// </summary>
        /// <returns></returns>
        IEnumerable<TModelo> Pesquisar(Expression<Func<TModelo, Boolean>> filtro_);
    }
}