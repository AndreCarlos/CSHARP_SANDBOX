using System;

namespace LGroup.Model
{
    public sealed class EstadoCivilModel : Base.BaseModel
    {
        /// <summary>
        /// Na arquitetura, DLLs são chamadas de componentes
        /// 
        /// Existe uma etapa no desenvolvimento de software chamada CODE REFACTORING (refatoraçao de codigo)
        /// Temos que analisar e melhorar o nosso codigo visando boas práticas, arquitetura e performance
        /// </summary>
        public string  Descricao { get; set; }
    }
}
