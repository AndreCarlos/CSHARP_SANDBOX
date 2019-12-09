using System;

//subimos a DLL para memoria
///temos duas formas de chamarmos(iniciarmos) as dlls:
///EARLY BINDING
///Add reference o nome seniorzão é early binding. Importamos a dll
///antes de executar o .exe(sistema)
///
///LATE BINDING
///É quando usamos reflection (reflexao). É quando importamos a dll
///ao rodos o exe(sistema). 

using LGroup.Model;

///Business não é um padrao de projeto
///Business é um termos arquitetural (nome clássico)
///É onde ficam as consistências, regras de negócio e validações
///
namespace LGroup.Business
{
    /// <summary>
    /// Quando bloqueamos uma classe com sealed, alem de evitarmos que 
    /// ninguém herde dela, por trás dos panos ganhamos performance. A máquina
    /// virtual (CLR) otimiza, acessa mais rapido clases seladas 
    /// Performance de milisegundos, talvez nanosegundos
    /// SOCA SEALED EM TUDO!
    /// </summary>
    public sealed class ClienteBusiness
    {
        /// <summary>
        /// No dia-a-dia existem naming standards ou name conventions 
        /// sao termos da arquitetura que significam boas práticas de nomenclatura
        /// 1- Padronização de Nomes
        /// Parametro de entrada termina com _
        /// </summary>
        /// <param name="cliente_"></param>
        public void ValidarCamposObrigatorios(ClienteModel cliente_)
        {
            ///tres ifs em um. Internamente ele faz 3 validaççoes
            ///1- verifica se é null
            ///2- verifica se é empty
            ///3- verifica se foi preenchido com espaços em branco
            ///

            ///Nunca dar um throw em exception, ela é a classe pai
            ///das classes filhas de erro(5000)
            ///Sempre que formos dar um erro proposital (porque eu quis)
            ///Colocar sempre ApplicationException
            if (string.IsNullOrWhiteSpace(cliente_.Nome))
                throw new ApplicationException("O campo nome não foi preenchido!");

            if (string.IsNullOrWhiteSpace(cliente_.Email))
                throw new ApplicationException("O campo Email não foi preenchido!");
        }
    }
}
