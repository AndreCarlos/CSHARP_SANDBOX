using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Importamos a Namespace das anotações
using System.ComponentModel.DataAnnotations;

namespace LGroup.SuperSisAmigos.Model
{
    public sealed class LoginMOD
    {
        [Required(ErrorMessage = "Preencha o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha a Senha")]
        public string Senha { get; set; }
    }
}
