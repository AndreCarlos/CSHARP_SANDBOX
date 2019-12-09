using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// O padrão MVC (model view controller) foi o primeiro padrao visual, 1970, Small Talk
/// Proposta do padrao é separar as telas das regras de negócio 
/// TELAS == VIEWS
/// REGRAS DE NEGÓCIO E DADOS == MODEL
/// CONTROLLER == Programamos e abrimos as telas 


///No MVC MODEL é muito genérico, normalmente associamos MODEL a ACESSO A DADOS 
///O melhor nome da classe que LEVA e TRAZ DADOS da view é VIEWMODEL
///viewmodel == dados específicos da TELA (Enviamos para Tela)
///VIEWMODEL não é um padrão de projeto é um TERMO ARQUITETURAL
///VIEWMODEL veio de um padrao de projeto do Martin Fowler chamado PRESENTATION MODEL
///NUNCA DEIXE MODEL 

namespace LGroup.SisClientes.UI.Web.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ClienteViewModel
    {
        public Int32 Codigo { get; set; }

        public String Nome { get; set; }

        public String Email { get; set; }

        public String Telefone { get; set; }
    }
}
