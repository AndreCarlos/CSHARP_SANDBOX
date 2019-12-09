//descemos via nuget  o kernel (DLL) do E.F.
//quando descemos via neget, ele desce a versao mais rescente 6.1.2
//nao tem nada visual, nao tem EDMX
using System.Data.Entity;

using LGroup.CodeFirst.Model;

namespace LGroup.CodeFirst.DataAccess
{
    //temos que transformar a nossa classe normal em uma classe
    //de conexão com o BD atraves do EF
    //a classe de conexao do EF eh a classe DbContext
    public class conexao : DbContext //classe de conexao do EF
    {
        //No construtor, passamos a string de conexao
        //ctor tab tab
        public conexao() : base("Data Source=AndreCarlos-msi\\Desenv2008;Initial Catalog=ANNOTATIONS;Integrated Security=true")
        { }

        public void CriarBanco()
        {
            //o comando database desceu da classe dbcontext
            Database.Create();
        }

        public void DeletarBanco()
        {
            Database.Delete();
        }

        //para mandar gerar as tabelas e acessa-las precisa utilizar a classe dbset
        public DbSet<SexoModel> Sexos { get; set; }

        public DbSet<EstadoCivilModel> EstadosCivis { get; set; }

        public DbSet<AmigoModel> Amigos { get; set; }
    }
}
