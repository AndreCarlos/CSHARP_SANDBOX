using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.SuperSisAmigos.Entities
{
    /// Estamos modelnado o nosso projeto utilizando DDD - Domain Driven Design (desenvolvimento
    /// dirigido ao negócio). Significa fazer um projeto pensando nos DADOS e nas REGRAS de negócio
    /// DDD não é um padrao de projeto, não é uma tecnologia. 
    /// DDD é um conjunto de boas práticas, filosoifa e melhores nomes que nos auxiliam a focar no negócio (daos e regras)
    /// //Quando criamos um projeto utilizamos do DDD temos temos que dividir o projeto em 4 camadas
    /// Presentation Infraestructure Application Domain 
    /// PRESENTATION -> camada visual (MVC)
    /// DOMAIN -> é a principal camada(DLL) do DDD onde ficam os dados e as regras de negócio a grosso modo
    /// é o model e os business 
    /// Cada classe que armazena dados se chama ENTITIDADE DE DOMÍNIO ;
    /// o domínio eh o que a empresa FAZ ou VENDE. Treinaemntos: vender treinamentos; ecommerce: vender produtos; pizzaria: vender pizza
    /// essa análise orientada a objetos é o dominio; é a analise que fizemos em cima do que a empresa está vendendo ou fazendo
    /// o nosso domínio eh um sistema de gerenciamento de amigos

    ///quando colocamos sealed em numa classe, nos fechamos (selamos) ela 
    ///falamos que ela nao vai ser pai de nenhuma outra classe, tiramso sua capacidade de herança
    ///por tras dos panos a maquina virtual (CLR) otimiza o acesso a classes seladas, elas serão acessadas mais rápido
    public sealed class Sexo : Base.Entidade
    {
        public string Descricao { get; set; }
    }
}
