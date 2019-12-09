using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///subimos para memoria as namespaces necessarias para trabalhar
///com mapemento de tabelas e campos via DA
///
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LGroup.ActiveRecord.Model
{
    /// <summary>
    /// O primeiro padrao de acesso a dados do mundo
    /// é o Active Record (Martin Fowler) (2003)
    /// Nesse padrao de acesso a dados, as classes de modelo além de armazenar também faz CRUD
    /// MODEL + DAO = ACTIVE RECORD (AR)
    /// As classes de auto crud (auto insere, auto atualiza, auto seleciona, auto deleta)
    /// </summary>
    /// SAO CONFIGURAÇÕES ADICIONAIS, SO OPCIONAIS DAS CLASSES EM CAMPOS
    [Table("TB_SEXO")]
    public sealed class SexoModel : Base.IBaseModel <SexoModel>
    {
        [Key]
        [Column("ID_SEXO", TypeName="INT")]
        public Int32 Codigo { get; set; }

        [Required]
        [MaxLength(09)]
        [Column("DS_SEXO", TypeName="VARCHAR")]
        public string Descricao { get; set; }

        public void Cadastrar()
        {
            throw new NotImplementedException();
        }

        public void Atualizar()
        {
            throw new NotImplementedException();
        }

        public void Deletar()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SexoModel> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
