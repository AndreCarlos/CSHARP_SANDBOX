using System;

///Para tudo existem padroes (vida, informatica, transito, igreja)
///Padroes sao melhores formas, formas conceituradas, de resolver um determinado
///Problema ou situaçao no codigo
///Podemos chamar também de DESIGN PATTERNS (padroes de projeto)
///Na programaçao existem tres categorias 
///1 - Padroes de Projeto
///2- Técnicas e Principios de OO
///3 - Termos Arquiteturais
///4-  Boas Práticas
///

///MODEL
///Model sozinho nao é um DP
///Para o Model ser um DP, tm que estar incorporado no MVC, MVVM ou MVP
///Model sozinho é um termo arquitetural
///Model é armazenamento de dados, consistencias e regras de negócios e acesso a dados 
///
namespace LGroup.Model
{
    /// <summary>
    /// quando uma classe nao tem nada na frente dela, ela é INTERNAL, não é Private
    /// Mudamdos a visibilidade dela para Public
    /// </summary>
    public sealed class ClienteModel : Base.BaseModel
    {
       public string Nome { get; set; }

        public string Email { get; set; }

        public string  Telefone { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
