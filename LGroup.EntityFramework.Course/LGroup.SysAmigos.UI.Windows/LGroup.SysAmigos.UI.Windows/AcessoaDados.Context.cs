﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LGroup.SysAmigos.UI.Windows
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EFEntities : DbContext
    {
        public EFEntities()
            : base("name=EFEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_AMIGO> TB_AMIGO { get; set; }
        public virtual DbSet<TB_SEXO> TB_SEXO { get; set; }
        public virtual DbSet<TB_USUARIO> TB_USUARIO { get; set; }
    
        public virtual int CADASTRAR_AMIGOS(string pNM_AMIGO, string pDS_EMAIL, string pNR_TELEFONE, Nullable<System.DateTime> pDT_NASCIMENTO, Nullable<int> pID_SEXO)
        {
            var pNM_AMIGOParameter = pNM_AMIGO != null ?
                new ObjectParameter("pNM_AMIGO", pNM_AMIGO) :
                new ObjectParameter("pNM_AMIGO", typeof(string));
    
            var pDS_EMAILParameter = pDS_EMAIL != null ?
                new ObjectParameter("pDS_EMAIL", pDS_EMAIL) :
                new ObjectParameter("pDS_EMAIL", typeof(string));
    
            var pNR_TELEFONEParameter = pNR_TELEFONE != null ?
                new ObjectParameter("pNR_TELEFONE", pNR_TELEFONE) :
                new ObjectParameter("pNR_TELEFONE", typeof(string));
    
            var pDT_NASCIMENTOParameter = pDT_NASCIMENTO.HasValue ?
                new ObjectParameter("PDT_NASCIMENTO", pDT_NASCIMENTO) :
                new ObjectParameter("PDT_NASCIMENTO", typeof(System.DateTime));
    
            var pID_SEXOParameter = pID_SEXO.HasValue ?
                new ObjectParameter("pID_SEXO", pID_SEXO) :
                new ObjectParameter("pID_SEXO", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CADASTRAR_AMIGOS", pNM_AMIGOParameter, pDS_EMAILParameter, pNR_TELEFONEParameter, pDT_NASCIMENTOParameter, pID_SEXOParameter);
        }
    
        public virtual ObjectResult<TB_AMIGO> LISTAR_AMIGOS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TB_AMIGO>("LISTAR_AMIGOS");
        }
    
        public virtual ObjectResult<TB_AMIGO> LISTAR_AMIGOS(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TB_AMIGO>("LISTAR_AMIGOS", mergeOption);
        }
    }
}