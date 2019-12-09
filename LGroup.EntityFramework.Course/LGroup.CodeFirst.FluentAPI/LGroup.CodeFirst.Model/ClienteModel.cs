using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.CodeFirst.Model
{
    //desde o EF 4.1 temos a estratégia do mapenamento de banco de dados
    //chamada CODE FIRST (o CÓIDGO primeiro), as classes virarão tabelas (ENGENHARIA REVERSA)
    //podemos fazer de 2 formas:
    //DATA ANNOTATIONS => atribuição feita em cima das classes e dos campos {ATRIBUTOS}
    //FLUENT API => quando mandamos lambdas (x => x). Muito mais sênior, temos um controle muito maior do codigo, sem limitações
    public sealed class ClienteModel : Core.BaseModel
    {
        public String Nome { get; set; }

        public String Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public String  Endereco { get; set; }

        public String CPF { get; set; }
    }
}
