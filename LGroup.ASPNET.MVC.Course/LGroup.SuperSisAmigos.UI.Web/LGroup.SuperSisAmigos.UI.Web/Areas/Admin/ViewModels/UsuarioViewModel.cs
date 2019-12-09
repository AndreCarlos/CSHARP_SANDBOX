using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Para poder obrigar o usuário a preencher os campos, utilizamos as
//anotações, configurações adicionais
using System.ComponentModel.DataAnnotations;

namespace LGroup.SuperSisAmigos.UI.Web.Areas.Admin.ViewModels
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        [Required(ErrorMessage="Preencha o Login")]
        public string Login { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage="Preencha a Senha")]
        public string Senha { get; set; }
    }
}