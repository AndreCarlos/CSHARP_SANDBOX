using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.DataAccessObject.Model
{
    public class AmigoModel
    {
        public Int32 Codigo { get; set; }
        public Int32 CodigoEstadoCivil { get; set; }
        public Int32 CodigoSexo { get; set; }
        public string Nome { get; set; }
        public string  Email { get; set; }
        public Decimal Salario { get; set; }
        public DateTime DataNascimento { get; set; }


        ///Criamos os relacionamentos entre as classes
        ///O EF por padrao nao resolve automaticamente os relacionamentos
        ///para fazermos o auto join entre tabelas utilizamos o VIRTUAL 
        ///Internamente ele cria um proxy (referencia) para tabelas relacionadas
        public virtual SexoModel Sexo { get; set; }

        public virtual EstadoCivilModel EstadoCivil { get; set; }
    }
}
