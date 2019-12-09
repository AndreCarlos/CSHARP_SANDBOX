using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Para poder criar as expressoes lambdas (x => x) temos que importar essa namespace:
using System.Linq.Expressions;

namespace LGroup.CodeFirst.Repository.Contracts.Core
{
    //Uma boa pratica de repositorio eh separar os comandos
    //em interface (Leitura e Gravacao)
    //Leitura - Listar e Pesquisar
    //Gravacao - Cadastrar, atualizar e deletar
    //Principio de Segragação de Interface = SOLID [ISP]
    //ISP - uma classe nao dever possuir nenhum metodo que nao vai ser usado
    //Se você colocar tudo dentro de uma unica interface (repository) você estaria obrigando
    //as classes a sempre levarem todos os comandos da interface
    public interface ILeitura<TModelo>
    {
        IEnumerable<TModelo> Listar();

        //Pra nao ter que ficar criando varios comandos de pesquisa, ex: PesquisarPorCodigo,
        //PesquisarPorData, PesquisarPorNome....
        //Criar um unico PESQUISAR e pra ficar dinamico, criamos um parametro de entrada do tipo
        //EXPRESSION. 
        //EXPRESSION -> eh pra receber uma lambda (x => x)
        //FUNC -> serve pra definir a classe que vamos pesquisar, consultar
        //quando mandarmos a lambda x=>x.Nome_Campo vao subir os campos da classe que definimos
        // dentro do FUNC
        //WHERE (x=>x.ID_AMIGO == 1)
        //como queremos comparar campos com conteudos isso eh um boolean
        //se ele encontrou algum eh TRUE
        //se nao encontrou nenhum campo com aquele conteudo eh FALSE
        IEnumerable<TModelo> Pesquisar(Expression<Func<TModelo, Boolean>> Filtro);
    }
}
