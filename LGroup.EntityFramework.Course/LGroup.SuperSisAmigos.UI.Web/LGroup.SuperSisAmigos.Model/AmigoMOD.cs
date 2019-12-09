using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Importamos a namespace das anotações de campos, classes
//De configurações adicionais dentro dos campos, classes
using System.ComponentModel.DataAnnotations;

namespace LGroup.SuperSisAmigos.Model
{
    //O sealed serve pra FECHAR A CLASSE, pra falar
    //Que ela não vai ser pai de ninguém, ela não pode
    //Ser herdada (CASTROU A CLASSE)
    //Por tras dos panos a maquina virtual otimiza o acesso
    //A classes SELADAS a classes que não podem ser herdadas
    public sealed class AmigoMOD
    {
        [Display(Name = "Código")]
        public Int32 Codigo { get; set; }

        [Required(ErrorMessage = "Preencha o Nome")]
        //Adicionamos uma configuração adicional para
        //Exibir um melhor texto lá no HTML
        [Display(Name = "Nome Completo")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Preencha o E-Mail")]
        [Display(Name = "E-Mail")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Preencha o Telefone")]
        [Display(Name = "Telefone Residencial")]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "Preencha a Data de Nascimento")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        //Da mesma forma que as tabelas se relacionam
        //Temos que relacionam as CLASSES
        //CLASSE conversando com CLASSE
        //Nome BUnitão COMPOSIÇÃO
        public SexoMOD Sexo { get; set; }
    }
}
