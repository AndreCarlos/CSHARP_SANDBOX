using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///O padrao Builder também chamado de construtor
///doFactory -> 3 de utilização
///A proposta do padrao é montar uma classe, objeto em etapas(blocos)
///Abas, modais, accordion (cenários clássicos de Builder)
///Vamos alimentamos a classe em pedacinhos
///Exemplo de Hospital (atendimento, triagem, médico)
///as informaçoes foram preenchidas em etapas, por pessoas diferentes
///1 Classe Builder POR CLASSE DE MODELO
namespace LGroup.Builder.Model.Builder
{
    //cada aba teria um comando pra preencher a aba
    public sealed class ClienteBuilder
    {
        ///a builder sempre tem que dar new e construir uma classe de modelo
        ///
        private ClienteModel _novoCliente = new ClienteModel();

        //1 aba, 
        //cada aba tem um comando de salvar, gravar os dados dela
        //Os metodos sempre retornam a propria classe. A classe se AUTO CHAMA (RETORNO)
        public ClienteBuilder SetarInformacoesPessoais(string nome_, string email_, DateTime data_)
        {
            _novoCliente.Nome = nome_;
            _novoCliente.Email = email_;
            _novoCliente.DataNascimento = data_;

            //É palavra de contexto(pra pegar a classe corrente = aberta)
            return this;
        }

        //2 aba
        public ClienteBuilder SetarTelefone(string ddd_, string numero_)
        {
            var novoTelefone = new TelefoneModel { DDD = ddd_, Numero = numero_ };

            //apos armazenar o telefone, jogamos pra dentro da lista de telefone
            _novoCliente.Telefones.Add(novoTelefone);

            return this;
        }

        //3 aba
        public ClienteBuilder SetarFoto(string caminho_, string extensao_, string nome_)
        {
            var novaFoto = new FotoModel { Caminho = caminho_, Extensao = extensao_, Nome = nome_ };
            _novoCliente.Fotos.Add(novaFoto);

            return this;
        }

        //criamos um comando para retornar o cliente preenchido(populado por etapas)
        public ClienteModel Gerar()
        {
            //Sempre no final do Builder tem que ter um return para devolver a classe
            //de Modelo que foi montada em etapas
            return _novoCliente;
        }
    }
}
