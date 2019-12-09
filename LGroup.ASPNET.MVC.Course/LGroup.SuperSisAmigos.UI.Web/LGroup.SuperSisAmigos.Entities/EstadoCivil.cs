using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities
{
    /// <summary>
    /// essas entidades de dominio devem ser PUBLIC 
    /// elas devem ser visualizadas de todos os projetos
    /// quando nao tem nada na frente eh INTERNAL (só é visivel dentro dessa dll)
    /// </summary>
    public sealed class EstadoCivil : Base.Entidade
    {

        public string Descricao { get; set; }
    }
}
