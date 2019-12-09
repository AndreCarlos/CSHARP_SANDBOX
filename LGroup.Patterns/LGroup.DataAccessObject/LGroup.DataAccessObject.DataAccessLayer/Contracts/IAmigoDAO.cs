using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGroup.DataAccessObject.Model;

namespace LGroup.DataAccessObject.DataAccessLayer.Contracts
{
    /// <summary>
    /// Para tabela de amigo queremos comandos de leitura, pesquisa gravaçao
    /// a classe que armazena dados de amigos é a AMIGOMODEL
    /// </summary>
    public interface IAmigoDAO : Base.ILeituraDAO<AmigoModel>, 
                                 Base.IPesquisaDAO<AmigoModel>,
                                 Base.IGravacaoDAO<AmigoModel>
    {

    }
}
