using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Repository.Base
{
    //Criamos uma interface (PAI) tudo que colocarmos
    //No pai, deve ser implementado(levado) pras classes
    //Filhas
    //O ruim de uma interface é que elas TE OBRIGAM
    //A levar os comandos do jeitinho que nós declaramos
    //Pra deixar algum comando FLEXIVEL, criar um parametro
    //De entrada na INTERFACE(TIPO GENERICO)
    public interface IBaseREP<Tipagem>
    {
        //Em uma interface criamos somente a ASSINATURA
        //Dos comandos (COBERTURA) o RECHEIO o CÓDIGO
        //Que vai dentro do comando, deve ser colocado
        //Na classe FILHA
        void Cadastrar();

        void Atualizar();

        void Remover(Int32 codigo);

        //Criamos um comando que retorna os registros
        //Da tabela
        List<Tipagem> Listar();
    }
}
