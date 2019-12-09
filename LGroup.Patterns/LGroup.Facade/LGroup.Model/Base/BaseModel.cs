using System;

///Base é um termo arquitetural, significa que é um local
///Onde vamos armazenar os super tipos == pais
///Classe ou Interfaces PAI
namespace LGroup.Model.Base
{
    public abstract class BaseModel
    {
        /// <summary>
        /// Pontos de refatoraçao
        /// 1- Criamos um super tipo para agrupar todos os membros repetidos(no caso, código)
        /// 2- Bloqueamos a classe pai para nao ser instanciada, ela só pode ser herdada 
        /// 3- Bloqueamos as classes filhas para nao serem herdadas
        /// 4- Removemos as namespaces nao utilizadas, visando as performances. Trazer as DLLs
        /// para o projeto não deixa lento, o que deixa lento é dar using. Quando damos o 
        /// using, submimos as dlls para memória (quando mais, mais lento)
        /// 
        /// </summary>
        public Int32 Codigo { get; set; }
    }
}
