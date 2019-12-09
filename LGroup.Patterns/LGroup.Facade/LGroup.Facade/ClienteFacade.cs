using System;
using LGroup.Business;
using LGroup.Helper;
using LGroup.Model;


//Um dos padrões GoF mais utilizados no mundo, é o padrao Facade
//O padrao Facade (FACHADA)
//Ele encapsula chamadas para diversas DLLs ou classes(metodos) dentro de 1 único local
//Fica menos complexo para o programador. Ele encapsula chamadas mais complexas
//É um simplificador de chamadas. REF DOFACTORY (1 a 5) é 5
namespace LGroup.Facade
{
    public sealed class ClienteFacade
    {
        public void IniciarCadastro(string nome_, string email_, string telefone_, DateTime data_)
        {
            //1-etapa de armazenamento
            var novoCliente = new ClienteModel();
            novoCliente.Nome = nome_;
            novoCliente.Email = email_;
            novoCliente.Telefone = telefone_;
            novoCliente.DataNascimento = data_;

            //2-etapa de validação
            var negocioCliente = new ClienteBusiness();
            negocioCliente.ValidarCamposObrigatorios(novoCliente);

            //3-etapa enviar email
            EmailHelper.Enviar("andre.leite.carlos@gmail.com", "andre.leite.carlos@gmail.com", "Novo Cliente Cadastrado com Sucesso", "Alguém inseriu um novo cliente sistema");

            //4-Gerar um txt de sucesso de LOG
            ArquivoHelper.Gerar(@"C:\processamento\log.txt", "Cliente Cadastrado com Sucesso");
        }
    }
}
