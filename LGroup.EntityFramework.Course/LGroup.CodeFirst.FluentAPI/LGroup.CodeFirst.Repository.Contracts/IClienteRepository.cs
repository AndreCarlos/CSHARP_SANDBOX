using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGroup.CodeFirst.Model;

namespace LGroup.CodeFirst.Repository.Contracts
{
    //O Padrao - DESIGN PATTERN chamado REPOSITORY
    //Serve para isolar a programaçao do dominio. A programacao
    //das tabelas (CRUD) é dentro dos repositorios, onde colocamos o CRUD das tabelas
    //DATA ACCESS - conexao e mapeamento de tabelas
    //REPOSITORY - CRUD
    //cada tabela vai ter uma interface - CONTRATO
    //É no contrato (INTERFACE) que definimos os cmomandos que devem ser
    //implementados para uma determinada tabela
    public interface IClienteRepository : Core.ILeitura<ClienteModel>, 
                                          Core.IGravacao<ClienteModel>
    {

    }
}
