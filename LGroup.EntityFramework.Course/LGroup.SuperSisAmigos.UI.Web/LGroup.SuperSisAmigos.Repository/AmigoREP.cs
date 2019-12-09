using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LGroup.SuperSisAmigos.Model;
using LGroup.SuperSisAmigos.DataAccess;

namespace LGroup.SuperSisAmigos.Repository
{
    public sealed class AmigoREP : Base.IBaseREP<AmigoMOD>
    {
        private AmigoMOD _dadosTela;

        //Criamos um conatrutor (INICIALIZADOR) da nossa classe, quem for dar um NEW nwla, vai ter que passar pra dentro dela uma
        //classe de modelo
        public AmigoREP(AmigoMOD dadosTela_)
        {
            _dadosTela = dadosTela_;
        }

        public AmigoREP()
        {
                
        }

        public void Cadastrar()
        {
            //Abrimos a conexão com o banco de dados
            using (var conexao = new SISAMIGOSEntities())
            {
                //Criamos uam variável apontando pra tabela
                var novoAmigo = new TB_AMIGO();

                novoAmigo.NM_AMIGO = _dadosTela.Nome;
                novoAmigo.DS_EMAIL = _dadosTela.Email;
                novoAmigo.DT_NASCIMENTO = _dadosTela.DataNascimento;
                novoAmigo.NR_TELEFONE = _dadosTela.Telefone;
                novoAmigo.ID_SEXO = _dadosTela.Sexo.Codigo;

                conexao.TB_AMIGO.Add(novoAmigo);
                conexao.SaveChanges();
            }
        }

        public void Atualizar()
        {
            using (var conexao = new SISAMIGOSEntities())
            {
                var amigoEditado = conexao.TB_AMIGO.Single(x => x.ID_AMIGO == _dadosTela.Codigo);

                amigoEditado.NM_AMIGO = _dadosTela.Nome;
                amigoEditado.DS_EMAIL = _dadosTela.Email;
                amigoEditado.DT_NASCIMENTO = _dadosTela.DataNascimento;
                amigoEditado.NR_TELEFONE = _dadosTela.Telefone;
                amigoEditado.ID_SEXO = _dadosTela.Sexo.Codigo;

                conexao.SaveChanges();
            }
        }

        public void Remover(int codigo)
        {
            //VIEW > CONTROLLER > REPOSITORY > DATAACCESS > EF > BANCO
            using (var conexao = new SISAMIGOSEntities())
            {
                var amigoDeletado = conexao.TB_AMIGO.Single(x => x.ID_AMIGO == codigo);

                conexao.TB_AMIGO.Remove(amigoDeletado);
                conexao.SaveChanges();
            }
        }

        public List<AmigoMOD> Listar()
        {
            //Sempre que quisermos fazer um looping pra pegar ps registros da tabela e jogar no MODEL, 
            //ou usamos o FOREACH ou o CONVERTALL
            using (var conexao = new SISAMIGOSEntities())
            {
                return conexao.TB_AMIGO.ToList().ConvertAll<AmigoMOD>(tabela =>
                {
                    return new AmigoMOD
                    {
                        Codigo = tabela.ID_AMIGO,
                        Nome = tabela.NM_AMIGO,
                        Email = tabela.DS_EMAIL,
                        Telefone = tabela.NR_TELEFONE,
                        DataNascimento = tabela.DT_NASCIMENTO,
                        Sexo = new SexoMOD
                        {
                            Codigo = tabela.TB_SEXO.ID_SEXO,
                            Descricao = tabela.TB_SEXO.DS_SEXO
                        }
                    };
                });
            }
        }
    }
}
