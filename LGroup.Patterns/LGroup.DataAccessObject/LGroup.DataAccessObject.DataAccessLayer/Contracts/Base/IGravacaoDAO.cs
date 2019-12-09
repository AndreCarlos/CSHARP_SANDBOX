using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Nem todas as tabelas precisam de todos os comandos do CRUD 
///por exemplo, tem tabelas que só queremos dar select, tem tabelas
///que so queremos add, update, delete
///Isso implementado no código, se chama SEGREGAÇAO DE INTERFACE (ISP)
///Princípio de Orientaçao a Objetos
///A idéia desse Princípio é agrupar os comandos por categoria, por funcionalidade
///por papel, por responsabilidade
///É o oposto da classe genérica de acesso a dados 
///Ou vai de classe genérica (tem tudo la dentro (CRUD completo))
///Ou vai de segregação de interface 

namespace LGroup.DataAccessObject.DataAccessLayer.Contracts.Base
{
    /// <summary>
    /// Para poder cadastrar, atualizar e deletar em qualquer tabela
    /// temos que deixar genérico (Polimórfico)<>
    /// </summary>
    /// <typeparam name="?"></typeparam>
    public interface IGravacaoDAO<TModelo> 
    {
        void Cadastrar(TModelo modelo_);
        void Atualizar(TModelo modelo_);
        void Deletar(Int32 codigo_);
    }
}
