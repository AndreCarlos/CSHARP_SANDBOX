using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///importamos as classes que armazenam dados 
using LGroup.SuperSisAmigos.Entities;

///importamos as classes que possuem a programação das tabelas (CRUD)
using LGroup.SuperSisAmigos.DataAcces.Repository;

namespace LGroup.SuperSisAmigos.Service
{
    /// <summary>
    /// DDD é o conjunto de princípios, técnicas e boas práticas que nos auxiliam
    /// a focar nos DADOS e nas REGRAS DE NEGÓCIO
    /// basicamente dividimos o projeto em 4 CAMADAS
    /// DOMAIN -> Entidades de Domínio e os Serviços de Domínio
    /// INFRASTRUCTURE -> Conexão, Migrations (Backup de Tableas) e o CRUD
    /// APPLICATION :
    /// Dentro da camada de aplicação criamos classes de serviço
    /// Não tem nada a ver com WEB SERVICES, WCF, WEB API
    /// Todos os dados deo DOMAIN e DA INFRA são acessados pela camada de APPLICATION
    /// Ela é o ponto de entrada do DDD
    /// PRESENTATION -> APPLICATION -> INFRAESTRUCTURE -> DOMAIN
    /// UI -> BLL -> DAL -> MODEL
    /// </summary>
    public sealed class AmigoService
    {
        /// <summary>
        /// dados da classe principal e das agregadas são tratados aqui
        /// 
        /// A classe de SERVIÇO aciona os REPOSITORIOS 
        /// AMIGOSERVICE -> SexoRepository, AmigoRepository, EstadoCivilRepository
        /// 
        /// Criamos 3 variáveis para acessar os 3 repositórios
        /// </summary>
        
        private AmigoRepository _repositoryAmigo = new AmigoRepository();
        private SexoRepository _repositorySexo = new SexoRepository();
        private EstadoCivilRepository _repositoryCivil = new EstadoCivilRepository();

        public IEnumerable<Amigo> ListarAmigos()
        {
            return _repositoryAmigo.Listar();
        }
        public IEnumerable<Sexo> ListarSexos()
        {
            return _repositorySexo.Listar();
        }
        public IEnumerable<EstadoCivil> ListarEstadosCivis()
        {
            return _repositoryCivil.Listar();
        }
        public void CadastrarAmigo(Amigo novoAmigo)
        {
            _repositoryAmigo.Cadastrar(novoAmigo);
        }
        public void AtualizarAmigo(Amigo amigoAtualizado)
        {
            _repositoryAmigo.Atualizar(amigoAtualizado);
        }
        public void ExcluirAmigo(int codigoAmigo)
        {
            _repositoryAmigo.Deletar(codigoAmigo);
        }
        public Amigo PesquisarAmigo(int codigoAmigo)
        {
            return _repositoryAmigo.Pesquisar(codigoAmigo);
        }
    }
}
