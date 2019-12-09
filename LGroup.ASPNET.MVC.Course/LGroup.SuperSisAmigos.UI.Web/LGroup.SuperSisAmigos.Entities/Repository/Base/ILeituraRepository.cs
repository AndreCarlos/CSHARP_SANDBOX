using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities.Repository.Base
{
    /// <summary>
    /// É uma boa prática sempre separar os comandos de tabela de leitura e de gravação
    /// quem precisar de comandos de leitura usa o ILeituraRepository
    /// quem precisar de comandos de gravação usa o IGravacaoRepository
    /// Isso se chama PRINCÍPIO DE SEGREGAÇÃO DE INTERFACE
    /// LEITURA -> Pesquisar e Listar
    /// GRAVAÇÃO -> Cadastrar, Atualizar e Deletar
    /// Segregando fica mais organizado
    /// 
    /// Deixamos a interface flexível. É tipo um gerador de código
    /// É um replace, onde ele encontrar a palavra ele troca pela que você mandou
    /// </summary>
    public interface ILeituraRepository<TEntidade>
    {
        /// <summary>
        /// Os 3 principais tipos de lista são:
        /// Ilist -> leitura, gravação e pesquisa (métodos como where, orderby...)
        /// IEnumerable -> somente comandos de leitura
        /// IQueryable -> somente comandos de leitura e pesquisa
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntidade> Listar();
        TEntidade Pesquisar(int codigoRegistro);
    }
}
