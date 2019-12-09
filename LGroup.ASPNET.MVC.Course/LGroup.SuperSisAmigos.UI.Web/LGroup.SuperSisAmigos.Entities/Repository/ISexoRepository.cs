using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities.Repository
{
    /// <summary>
    /// Para cada ENTIDADE DE DOMÍNIO (classe que armazena dados), criamos uma 
    /// interface (SERVIÇO DE DOMÍNIO) (INTERFACE == PAI)
    /// Sexo -> ISexoRepository
    /// Amigo -> IAmigoRepository
    /// É no serviço de domínio (interface) que vamos definir quais comandos do CRUD devem
    /// ser codificados (implementados, programados)
    /// </summary>
    public interface ISexoRepository : Base.ILeituraRepository<Sexo>
    {

    }
}
