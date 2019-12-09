using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.DataAccessObject.Model;

///Os principais padroes de acesso a dados sao
///Repository, DAO, Active Record e Unit of Work
///Padrões de acesso a dados so novos, o mais velho é de 2003 (Active Record)
///DAL é diferente de DAO
///DAL == Data Access Layer (termo arquitetural = Camadas de Acesso a Dados)
///DAO == Data Access Object = Padrao de projeto
///no livro da Sun CORE2E de 2007
///Proposta do padrao DAO é ter um local isolado
///de manipulacao de banco e tabelas
///Criamos uma DAL utilizando o DAO
///Contracts (Interfaces) e Implementations (classe) sao termos arquiteturais
///

namespace LGroup.DataAccessObject.DataAccessLayer.Contracts
{
    /// <summary>
    /// nos padroes DAO
    /// nas interfaces definimos os comandos das tabelas
    /// tb_sexo => teremos comandos de leitura
    /// </summary>
    public interface ISexoDAO : Base.ILeituraDAO<SexoModel>
    {

    }
}
