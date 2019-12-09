using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities.Repository.Base
{
    /// <summary>
    /// Para deixar a interface flexível, adaptável para qualquer tabela 
    /// temos que utilizar a técnica de tipos genéricos de criar um parâmetro
    /// de entrada na INTEFACE
    /// criamos uma interface que se adequa a qualquer classe que armazena dados
    /// </summary>
    /// <typeparam name="TEntidade"></typeparam>
    public interface IGravacaoRepository<TEntidade>
    {
        void Cadastrar(TEntidade novoRegistro);
        void Atualizar(TEntidade registroAlterado);
        void Deletar(int codigoRegistro);
    }
}
