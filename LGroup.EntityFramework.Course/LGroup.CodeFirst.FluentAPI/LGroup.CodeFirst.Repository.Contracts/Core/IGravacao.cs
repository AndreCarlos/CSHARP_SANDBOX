using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.CodeFirst.Repository.Contracts.Core
{
   //criamos um tipo generico na interface de gravacao. Quem for herdar dessa interface
    //vai ter que mandar um parametro pra dentro dela 
    public interface IGravacao<TModelo>
    {
        void Cadastrar(TModelo dadosTela);

        void Atualizar(TModelo dadosTela);

        void Deletar(Int32 codigo);
    }
}
