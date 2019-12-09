using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.RequestResponse.DataTransferObject;
using LGroup.RequestResponse.DataTransferObject.Delivery;

///subimos pra memoria a dll com as classes para construção
///do serviço utilizando WCF (Windows Communication Foundation)
using System.ServiceModel;


namespace LGroup.RequestResponse.Service.Contracts
{
    /// <summary>
    /// wcf - falamos que  interface é um serviço wcf
    /// </summary>
    [ServiceContract]
    public interface IClienteService
    {
        /// <summary>
        /// Implementamos as classes do request-response
        /// padronizamos as entradas e saidas dos métodos
        /// sempre entra requestdto e sempre sai responsedto
        /// </summary>
        /// <param name="pedido_"></param>
        /// <returns></returns>
        
        [OperationContract]
        ResponseDTO Cadastrar(RequestDTO pedido_);

        /// <summary>
        /// falamos que os comandos vao ser acessados remotamente
        /// que sao comandos do serviço
        /// utilizamos a classe do WCF OperationContract
        /// </summary>
        /// <param name="pedido_"></param>
        /// <returns></returns>
        [OperationContract]
        ResponseDTO Listar(RequestDTO pedido_);
    }
}
