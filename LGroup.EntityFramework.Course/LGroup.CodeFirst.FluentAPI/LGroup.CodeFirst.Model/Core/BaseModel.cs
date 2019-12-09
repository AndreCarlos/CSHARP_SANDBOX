using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.CodeFirst.Model.Core
{
    //uma boa pratica de desenvolvimento eh sempre refatorar o codigo visando performance,
    //conceitos POO e boas práticas em geral

    //quando criamos uma classe, podemos consumi-la de duas formas
    //1. via Instância => NEW; para bloquear a instancia colocar ABSTRACT
    //2. via Herancça => : ; pra bloquear a herança, colcoar o SEALED. O sealed sela a classe (fecha a classe para herança) para ninguém herdar
    //e o bacana eh que por trás dos panos ganhamos performance. A máquina virtual (CLR) OTIMIZA o acesso à classes fechadas e sao acessadas 
    //de forma mais rápida. Sealed e abstract são opostos...
    public abstract class BaseModel
    {
        //para não replicar este campo entre classes, o levamos para o super tipo. Para classe pai
        public Int32 Codigo { get; set; }
    }
}