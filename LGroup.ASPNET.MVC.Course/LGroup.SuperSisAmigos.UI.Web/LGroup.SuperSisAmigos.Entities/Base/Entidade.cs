using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities.Base
{
    /// <summary>
    /// no DDD as classes ou intefaces pai (super tipos) devem ficar dentro da pasta base
    /// tudo que for igual em todas as entidades, jogar dentro dessa classe, afinal ela eh 
    /// uma classe pai tudo que tiver igual bem pra cá.
    /// 
    /// Quando criamos uma classe podemos consumi-la de duas formas 
    /// Herança   -> :, para bloquear a herança colcoar SEALED
    /// Instância -> new, para bloquear a instância colocar ABSTRACT
    /// normalmente classes pai foram feitas para serem herdadas, sempre bloquear a instancia (abstract)
    /// </summary>
    public abstract class Entidade
    {
        public Int32 Codigo { get; set; }
    }
}
