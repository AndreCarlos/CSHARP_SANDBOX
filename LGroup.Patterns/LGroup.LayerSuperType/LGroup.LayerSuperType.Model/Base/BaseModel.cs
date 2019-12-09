using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///O padrao Layer Super Type é um padrao do Martin Fowler (2002)
///A proposta do padrao eh criar um super tipo (classe, interface) == PAI
///E esse supoer tipo va ser o pai de todas as classes dessa DLL (componente)
///
///Para ser Layer Super Type todas as classes devem ter o mesmo pai (super tipo) 
///Se 1 ou outra classe não herdar, já era, nao eh Layer Super Type, é uma herança normal
namespace LGroup.LayerSuperType.Model.Base
{
    public abstract class BaseModel
    {
        public Int32 Codigo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }

        public Boolean Status { get; set; }

        /// <summary>
        /// Quando criamos um metodo com virtual significa que as classes filhas
        /// podem sobrescrever esse método, podem colocar a sua propria implementaçao
        /// palavra reservada OVERRIDE
        /// </summary>
        public virtual void ValidarCamposObrigatorios()
        {
            if (DataCadastro > DateTime.Now)
                throw new ApplicationException("Data Invalida");

            if (!Status)
                throw new ApplicationException("O Cadastro Deve Ser Ativo ");
        }
    }
}
