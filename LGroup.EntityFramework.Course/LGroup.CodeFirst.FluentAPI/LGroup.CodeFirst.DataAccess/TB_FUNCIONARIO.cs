//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LGroup.CodeFirst.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_FUNCIONARIO
    {
        public TB_FUNCIONARIO()
        {
            this.TB_PONTO = new HashSet<TB_PONTO>();
        }
    
        public int ID_FUNCIONARIO { get; set; }
        public string NM_FUNCIONARIO { get; set; }
        public string NR_TELEFONE { get; set; }
    
        public virtual ICollection<TB_PONTO> TB_PONTO { get; set; }
    }
}
