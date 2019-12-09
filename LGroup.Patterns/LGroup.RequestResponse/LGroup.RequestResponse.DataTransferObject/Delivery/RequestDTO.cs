using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.RequestResponse.DataTransferObject.Delivery
{
    /// <summary>
    /// Quando falamos de serviços sempre enviamos alguma informaçao e sempre
    /// recebemos alguma informação 
    /// ENIAMOS = PEDIDOS = REQUEST = É o que vai pro serviço
    /// RECEBEMOS = RESPOSTA = RESPONSE = É o que volta do serviço
    /// 
    /// Da mesma forma que tem padrões visuais (MVC, MVVM, MVP)
    /// Da mesma forma que tem padroes de acesso a dados (ACTIVE RECORD, DAO) 
    /// Tmbém tem pdroes de integraçao (SERVIÇOS)
    /// Tem um padrão do MARTIN FOWLER = 2010 para construção de serviços
    /// REQUEST-RESPONSE -> REQUEST-REPLAY (pedido e resposta)
    /// padronizamos as entradas e saídas do serviço
    /// sempre entre a mesma classe e sempre volta a mesma classe
    /// 
    /// </summary>
    
    public sealed class RequestDTO
    {
        /// <summary>
        /// A classe de entrada (envio) agrupa as subclasses (dados)
        /// </summary>
        public ClienteDTO Cliente { get; set; }
        public FornecedorDTO Fornecedor { get; set; }
    }
}
