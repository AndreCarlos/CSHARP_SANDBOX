using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.RequestResponse.DataTransferObject;
using LGroup.RequestResponse.DataTransferObject.Delivery;
using LGroup.RequestResponse.ValueObject;
using LGroup.RequestResponse.Service;
using LGroup.RequestResponse.Service.Contracts;

///namespace necessaria para trabalhar com cronometros
using System.Diagnostics;

namespace LGroup.RequestResponse.Service.Implementations
{
    public sealed class ClienteService : Contracts.IClienteService
    {
        /// <summary>
        /// como as respostas sao padronizadas (sempre volta a mesma classe)
        /// </summary>
        /// <param name="pedido_"></param>
        /// <returns></returns>
        private readonly ResponseDTO _resposta = new ResponseDTO();

        public ResponseDTO Cadastrar(RequestDTO pedido_)
        {
            ///assim que entrou no cadastrar ligamos o cronometro (temporizador=timer)
            var cronometro = new Stopwatch();
            cronometro.Start();


            //sao os campos de monitoramento (log)
            _resposta.DataExecucao = DateTime.Now;
            _resposta.Mensagem = "Cliente Cadastrado com Sucesso";
            _resposta.TipoMensagem = TipoMensagemVO.Sucesso;

            if(pedido_.Cliente.Nome == null)
            {
                _resposta.Mensagem = "Informe o Nome do Cliente";
                _resposta.TipoMensagem = TipoMensagemVO.Aviso;
            }

            ///desligamos o cronometro
            cronometro.Stop();

            ///capturamos o tempo de execucao
            _resposta.TempoExecucao = cronometro.Elapsed;

            return _resposta;
        }

        public ResponseDTO Listar(RequestDTO pedido_)
        {
            var cronometro = new Stopwatch();
            cronometro.Start();

            _resposta.DataExecucao = DateTime.Now;
            _resposta.Mensagem = "Cliente Exibidos com Sucesso";
            _resposta.TipoMensagem = TipoMensagemVO.Sucesso;

            _resposta.Clientes = new List<ClienteDTO>
            {
                new ClienteDTO{
                    Codigo = 1,
                    Nome = "Cliente 1",
                    DataNascimento = DateTime.Now
                },

                new ClienteDTO{
                    Codigo = 2,
                    Nome = "Cliente 2",
                    DataNascimento = DateTime.Now
                },
                    new ClienteDTO{
                    Codigo = 3,
                    Nome = "Cliente 3",
                    DataNascimento = DateTime.Now
                 },

                    new ClienteDTO{
                    Codigo = 4,
                    Nome = "Cliente 4",
                    DataNascimento = DateTime.Now
                  }
            };

            cronometro.Stop();
            _resposta.TempoExecucao = cronometro.Elapsed;

            return _resposta;
        }
    }
}
