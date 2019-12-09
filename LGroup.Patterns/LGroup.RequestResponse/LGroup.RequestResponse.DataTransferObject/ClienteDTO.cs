using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///O padrao DTO (data transfer object)
///Martin Fowler (2010)
///A proposta desse padrao é armazenar dados e transferir esses dados entre
///as DLLs (camadas) ou pelo Serviço (protocolo)

///Lembra muito um MODEL. Só podemos chamar de model se for algum padrao visual (MVC,
///MVP, MVVM) = MODEL
///SE NÃO TIVER telas (padrao visual) o nome certo é DTO

///Um DTO só armazenamos dados (não pode ter regras, nem inteligência, nem acessar dados)
///É uma classe anemica, burra que só serve para armzenar dados

///MODEL (armazenamento de dados, regras de negócio, acesso a dados)
///É o Oposto do DTO

///ASP.NET MVC
///O Model é para lear e trazer dados da view (tela)
///para levar pro banco de dados é o DTO
///Pegamos do model e levamos pro DTO

namespace LGroup.RequestResponse.DataTransferObject
{
    /// <summary>
    /// Dados Temporários 
    /// </summary>
    public sealed class ClienteDTO
    {
        public Int32 Codigo { get; set; }
        public string  Nome { get; set; }
        public string  Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
