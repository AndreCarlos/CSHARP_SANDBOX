using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.RequestResponse.ValueObject;

namespace LGroup.RequestResponse.DataTransferObject.Delivery
{
    /// <summary>
    /// É a  classe que volta (reposta do serviço)
    /// </summary>
    public sealed class ResponseDTO
    {
        public IEnumerable<ClienteDTO> Clientes { get; set; }
        public IEnumerable<FornecedorDTO> Fornecedores { get; set; }


        ///complexidade desnecessária (seniorzao)
        ///campos de monitoramento (rastreamento, log)
        public DateTime DataExecucao { get; set; }

        public TimeSpan TempoExecucao { get; set; }

        public string  Mensagem { get; set; }

        /// <summary>
        /// tipo de opçoes (sucesso, erro, aviso)
        /// </summary>
        public TipoMensagemVO TipoMensagem { get; set; }


    }
}
