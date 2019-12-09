using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities
{
    /// <summary>
    /// apos modelas as entidades de dominio fizemos um CODE REFACTORING 
    /// refatoracao de codigo, significa melhorar o codigo pensando em boas práticas, performance
    /// e padroes de projeto
    /// </summary>
    public class Amigo : Base.Entidade
    {
        ///para cada campo que queremos armazenar dados, criamos 
        ///1 propriedade 
 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }

        /// Na entidade de amigo, temos que ter o ID_SEXO e O ID_ESTADO_CIVIL
        /// são numeros (dados) que virão de outras tabelas (TB_SEXO, TB_ESTADO_CIVIL)
        public Int32 CodigoSexo { get; set; }
        public Int32 CodigoEstadoCivil { get; set; }

        ///Quando montamos umn relacionamento entre classes 
        ///AMIGO -> SEXO
        ///AMIGO -> ESTADOCIVIL
        ///
        ///Se estivermos utilizando as práticas, filosofias do DDD
        ///o termo técnico é RAIZ DE AGREGAÇÃO OU AGGREGATE ROOT
        ///São classes SECUNDÁRIAS que dependem de uma classe PRIMÁRIA, de uma CLASSE PRINCIPAL


        /// Por padrão quando buscamos dados de uma entidade raiz, de uma entidade principal, ele nao traz
        /// os dados das entidades agregadas, ele não faz join sozinho, você tem que fazer na raça
        /// Para fazer join sozinho, no automático, assim que trazer o amigo, traga também o sexo e estado civil
        /// colocar o VIRTUAL (INNER JOIN)
        public virtual Sexo Sexo { get; set; }

        public virtual EstadoCivil EstadoCivil { get; set; }

        /// <summary>
        /// Estamos simulando um cenário em que o banco já está em produção
        /// e algum usuário liga e pede para incluir mais dois campos
        /// </summary>
        //public string  Endereco { get; set; }

        //public string Numero { get; set; }

        //public string CEP { get; set; }
    }
}
