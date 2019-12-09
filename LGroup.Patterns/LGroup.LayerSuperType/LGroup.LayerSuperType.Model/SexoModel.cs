using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.LayerSuperType.Model
{
    public sealed class SexoModel : Base.BaseModel
    {
        public string Descricao { get; set; }

        /// <summary>
        /// a base da orientaçao a objeto é
        /// estados e comportamentos
        /// Ou seja as nossas classes tem que ter estados 
        /// Estado = variáveis e propriedade
        /// Comportamentos = métodos e eventos 
        /// Politicamente está mais correto agora
        /// Modelo anêmico = só tem propriedades
        /// </summary>
        public override void ValidarCamposObrigatorios()
        {
            ///Pra disparar erros temos que ser específicos (classe de erro)
            ///para capturar pode ser genérico (EXCEPTION)
            ///
            base.ValidarCamposObrigatorios();
            
            if (Descricao == null)
                 throw new ApplicationException("Informe a Descrição");
        }
    }
}
