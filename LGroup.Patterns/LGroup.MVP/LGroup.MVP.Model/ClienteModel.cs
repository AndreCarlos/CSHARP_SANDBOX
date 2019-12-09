using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Os 3 principais padroes visuais são
///Todos Eles tem model (armazenamento, negocio e acesso  dados)
///
///MVC -> ASP.NET MVC
///
///
///MVP 
///O padrão model view presenter é o padrao recomendado pra windows e web forms
///existe desde 1990 e popularizou em 2006 (Martin Fowler, Microsoft) 
///e ele é derivado do MVC
///Criar views  (interfaces = código) acoplaveis
///Podemos de forma fácil migrar o mesmo código de um projeto windows pra web e vice-versa
///Sem ter que dar muita manutenção
///Não tem como meter MVC no windows e web forms
///
///
///MVVM -> Tecnologias Baseadas em XAML




namespace LGroup.MVP.Model
{
    public sealed class ClienteModel
    {
        public Int32 Codigo { get; set; }
        public string  Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
