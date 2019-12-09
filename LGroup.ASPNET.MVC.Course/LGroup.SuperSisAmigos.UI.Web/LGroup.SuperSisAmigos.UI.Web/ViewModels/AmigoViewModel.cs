using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//desde o aspnet mvc 2.0 temos um recurso chamado de DATA ANNOTATIONS (configurações Adicionais)
//que injetamos dentro dos campos, por padrao vem sem, mas podemos colocar para dar um plus (mais properties)
//subimmos a namespace que vai habilitar as classes de ANOTATIONS
//WPF, WCF, WEB API, WEB FORMS
using System.ComponentModel.DataAnnotations;

namespace LGroup.SuperSisAmigos.UI.Web.ViewModels
{
    //Pensando em Arquitetura, em Padrões de Projeto e ate mesmo em Boas Práticas
    //MODEL eh um termo (nome) muito genérico temos que ser mais especificos
    //Existem 2 tipos de MODELS (NODELOS)
    //VIEWMODEL >> Classes que armazenan dados da TELA
    //DOMAINMODEL >> Classes que armazena dados da TABELA
    //Sempre renomear a pasata MODELS para VIEWMODELS
    public class AmigoViewModel
    {
        public Int32 CodigoAmigo { get; set; }

        [Display(Name = "Nome Completo")]
        //A anotaçao REQUIRED obriga o usuario a preencher o campo
        //é um tipo de if, caso nao preencha vai exibir a mensagem abaixo
        [Required(ErrorMessage = "Preencha o Campo Nome")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O Nome deve ter de 3 a 30 caracteres")]
        public String Nome { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Preencha o Campo eMail")]
        //podemos validar os campos da tela através de uma máscara de validação. 
        //É uma expressao de validação (REGEX = Regular Expression)
        [RegularExpression (@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage="E-Mail Inválido")]
        public String Email { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "Preencha o Campo Celular")]
        public String Telefone { get; set; }

        //Anotação display serve para exibir um texto mais legal lá na view
        //sempre que esse campo for carregado num label vai aparecer na tela 
        //o texto que está dentro do display

        //Tem uma outra anotaçao que eh o RANGE
        //Conseguimos definir um nmuimero minino e maximo perimtido
        //Quantidade de filhos, quantidade de produtos
        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Preencha o Campo Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}