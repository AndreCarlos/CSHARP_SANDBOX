using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Tem um padrão do MARTIN FOWLER = 2010
///Value Object = VO
///São as informaçoes secundárias que normalmente nao vao para tabela
///quase sempre só usadas na memoria
///Alguns tipos:
///CONSTANT = VO
///ENUM = VO

///Temos pelo menos 5 melhores nomes pra armazenar dados
///1 - Model = Projeto com telas (MVC, MVVM, MVP)
///2 - DTO = Projeto sem telas 
///3 - VO = Secundária que nao vai pro banco
///4 - VIEWMODEL = São dados exclusivos de tels (VIEW)
///5 - *DOMAIN ENTITY = Se tiver DDD o model lá no DDD chamamos de ENTIDADE DE DOMÍNIO
namespace LGroup.RequestResponse.ValueObject
{
    /// <summary>
    /// Enum é um lista pré-definida com as possiveis informaçoes
    /// </summary>
    public enum TipoMensagemVO
    {
        Aviso,
        Erro,
        Sucesso
    }
}
