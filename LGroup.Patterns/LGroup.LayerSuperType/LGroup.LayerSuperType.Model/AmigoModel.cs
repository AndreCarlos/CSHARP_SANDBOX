using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.LayerSuperType.Model
{
    public sealed class AmigoModel : Base.BaseModel
    {
        public string Nome { get; set; }

        public string  Email { get; set; }

        /// <summary>
        /// O casal VIRTUAL E OVERRIDE 
        /// VIRTUAL VAI NO PAI
        /// OVERRIDE VAI NO FILHO 
        /// É uma forma de termos uma implementaçao generica no pai
        /// e habilitaremos as classes filhas para terem as suas proprias implementaçoes 
        /// temos as validaçoes do pai + validaçoes do filho
        /// </summary>
        /// 
        ///Pra disparar erros temos que ser específicos (classe de erro)
        ///para capturar pode ser genérico (EXCEPTION)
        public override void ValidarCamposObrigatorios()
        {
            ///Aqui no C# temos palavras de contextos (De onde buscar algo)
            ///THIS --> para buscar algo da prórpia classe aberta (corrente, filha)
            ///BASE --> para buscar algo do supertipo == classe pai
            ///
            //this.ValidarCamposObrigatorios();
            base.ValidarCamposObrigatorios();

            if (Nome == null)
                throw new ApplicationException("Informe o Nome");

            if (Email == null)
                throw new ApplicationException("Informe o Email");
        }
    }
}
