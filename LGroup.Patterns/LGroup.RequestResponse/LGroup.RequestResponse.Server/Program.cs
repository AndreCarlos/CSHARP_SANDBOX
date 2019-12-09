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
using LGroup.RequestResponse.Service.Implementations;
using System.ServiceModel;

///Pra subir as classes que vao liberar, habilitar os metadados dos serviços
///importamos essa namespace
using System.ServiceModel.Description;

///aqui no servidor vamos subir o serviço pra memoria
///para nao usr o iis(pesado , tem muitos modulos)
///wcf self-hosting nos montamos o nosso proprio servidor
///pra subir o serviço (ganhamos performance)
///montamos o nosso propiro "iis"

///OWIN  é uma especificaçao aberta, conjunto de principios e termos
///que nos auxiliam  a montar um servidor web (autonomo, stand alone, self hosting)
///que funciona em qualquer plataforma (windows, linux e OSX)
///projeto KATANA (OWIN DA MICROSOFT)
///OWIN + .NET CORE 1.0

///Serviços sao componentes remotos (dll remota)
///colocamos uma dll em alguma maquina ao redor do mundo
///e acessamos el atraves de algum protocolo (HTTP, TCP, MSMQ)
///Quando trabalhamos com serviços estamos utilizando SOA (SERVICE
///ORIENTED ARCHITECTURE)
///MICROSERVICES (Desmembrar, modularizar os serviços)
///para cada serviço (1DL, 1 MODEL, 1BLL, 1 BANCO DE DADOS)
///ClienteService (1DL, 1 MODEL, 1BLL, 1 BANCO DE DADOS)
///FornecedorService (1DL, 1 MODEL, 1BLL, 1 BANCO DE DADOS)
///PedidoService (1DL, 1 MODEL, 1BLL, 1 BANCO DE DADOS = PEDIDO, ITENS, PRODUTO)

///TECNOLOGIAS MICROSOFT PRA CONSTRUÇAO DE SERVIÇOS
///2001 A 2005 = WEB SERVICES (*.asmx) == LIXO (Por compatibilidade com versoes antigas do .NET)
///2006 a 2012 = WCF SERVICES (*.svc) == (HTTP, TCP, MSMQ E NAMED PIPES (UDP))
///2012 pra cá = ASP.NET WEB API == SOMENTE HTTP (MAIS FÁCIL)
///

namespace LGroup.RequestResponse.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ///DEFINIMOS uma url pra o serviço
            ///montamos um servidor web na unha (raça)
            var url = new Uri("http://localhost:12345/Api/LGroup");

            ///com wcf montamos o nosso iis
            ///Servidor pra hospedar o serviço
            ///WAS - windows activation service
            ///com o WAS habilitamos outros protocolos no iis (tcp, msmq)
            var servidor = new ServiceHost(typeof(ClienteService), url);

            ///Quando subimos para memoria um servico wcf, temos que vincular
            ///as 2 arquivos (armarrar) os 2 arquivos (CLASSE E INTERFACE) 
            ///Classe = Implementation
            ///Interface = Contracts
            ///Com wcf temos vários protocolos (HTTP, TCP, MSMQ, NAMED PIPES(UDP))
            ///É na linha abaixo que definimos o tipo de protocolo e suas configuraçoes
            ///O protocolo HTTP simples, padrão é o wsHttpBinding
            servidor.AddServiceEndpoint(typeof(IClienteService), new WSHttpBinding(), "");

            ///As aplicações cliente (as que vão chamar, cionar o WCF) precisam
            ///saber de tudo que temos ali dentro, quais comandos elas podem 
            ///chamar (listar e cadastrar)
            ///Pra que elas consigam visualizar os comandos temos que habilitar os 
            ///METADADOS (As informações do serviços = todos os comandos disponíveis)
            ///Como se fosse o PUBLIC dos comandos
            
            ///Liberamos os metadados
            servidor.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            ///Após configurar o servidor (o ligamos)
            ///subimos para memória
            servidor.Open();


            ///Pra nao fechar com tudo, esperamos uma tecla
            Console.ReadKey();
        }
    }
}
