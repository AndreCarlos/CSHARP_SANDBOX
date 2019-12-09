using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

///Quando fazemos CODE FIRST temos 2 formas
///FLUENT API
///mais avançada, sem limitações , mais nova e a preferida pelos DEVS
///
///DATA ANNOTATIONS
///mais antiga, tem limitaçoes, mais simples, mais bosta 
///1) Com data annotations nao tem como mapear procedure
///2) Nao tem como mapear relacionamento de N pra N
///3) Nao tem como mapear campos decimais (valor)
///4) Nao tem como configurar a exclusao em cascata 
///
namespace LGroup.ActiveRecord.Model.Database
{
    public sealed class Conexao : DbContext
    {
        /// <summary>
        /// Tudo que é parametrizável no código e fica fixo, chamamos de hard coded (difícil de manter, de dar manutenção)
        /// </summary>
        public Conexao() : base("Data Source= LGROUP05\\SQLEXPRESS; Initial Catalog=SISAMIGOSDA; Integrated Security=True")
        {

        }

        ///A classe DBSET é quem acessa e faz crud nas tabelas
        ///
        public DbSet<AmigoModel> Amigo { get; set; }
        public DbSet<SexoModel> Sexo { get; set; }

    }
}
