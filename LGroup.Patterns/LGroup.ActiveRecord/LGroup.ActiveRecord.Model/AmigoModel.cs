using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

///subimos para memória a pasta database (conexao)
using LGroup.ActiveRecord.Model.Database;

namespace LGroup.ActiveRecord.Model
{
    [Table("TB_AMIGO")]
    public sealed class AmigoModel : Base.IBaseModel<AmigoModel>
    {
        /// <summary>
        /// Inicializamos uma conexao com o banco de dados
        /// </summary>
        private readonly Conexao _conexao = new Conexao();

        [Key]
        [Column("ID_AMIGO", TypeName = "INT")]
        public Int32 Codigo { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("NOME", TypeName = "VARCHAR")]
        public string  Nome { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("DS_EMAIL", TypeName = "VARCHAR")]
        public string  Email { get; set; }

        /// <summary>
        /// datetime por ser um tipo de valor, numca fica em branco, valor padrao é 01/01/0001
        /// Para que esse campo fique em branco, transformamos num NULLABLE TYPE
        /// </summary>
        [Required]
        [Column("DT_NASCIMENTO", TypeName = "DATE")]
        public DateTime? DataNascimento { get; set; }

        [Required]
        [Column("ID_SEXO", TypeName = "INT")]
        public Int32? CodigoSexo { get; set; }

        /// <summary>
        /// A anotação FK é a única que colocamos o nome da propriedade. O resto
        /// é tudo o nome do campo da tabela
        /// </summary>
        [ForeignKey("CodigoSexo")]
        public SexoModel Sexo { get; set; }

        /// <summary>
        /// Daqui pra baixo é crud (AR)
        /// o AR é um model verdadeiro
        /// ARMAZENA DADOS E ACESSA DADOS
        /// </summary>
        public void Cadastrar()
        {
            ///Como a maioria dos campos da tabela, classe sao obrigatorios
            ///nao podemos enviar em branco pra o banco de dados
            ///temos que consistir, validar com IFs
            ///com if se tivermos muitos campos nas tabelas, teremos que colocar muitos IFs
            ///É um recurso exclusivo dos data annotations
            
            ///Acionamos a classe de gerenciamento de validações
            ///na classe corrente = this (quero me auto validar)
            var contextoValidacao = new ValidationContext(this);

            ///criamos uma lista de erros de validação. Cada 1 requered igual a 1 erro de validação
            var erros = new List<ValidationResult>();

            ///mandamos disparar as validaçoes (requered)
            ///todas as validaçoes que colocamos via data annotations
            ///IFs sem IFs (dinamico)
            Validator.TryValidateObject(this, contextoValidacao, erros);

            ///Só pode salvar se não tiver erros
            ///COUNT - conta todos os registros
            ///ANY - assim que ele encontrou o primeiro registro, ele pára de procurar
            if(!erros.Any())
            {
                ///a classe se auto insert - autocadastra
                ///nao tem parametros de entrada, pois os dados já ficam na própria classe de modelo. É duplicar
                ///código, criar parametro de entrada
                _conexao.Amigo.Add(this);
                _conexao.SaveChanges();
            }
        }

        public void Atualizar()
        {
            _conexao.Entry(this).State = EntityState.Modified;
            _conexao.SaveChanges();
        }

        public void Deletar()
        {
            var amigoExcluido = _conexao.Amigo.Find(Codigo);

            _conexao.Amigo.Remove(amigoExcluido);
            _conexao.SaveChanges();
        }

        public IEnumerable<AmigoModel> Listar()
        {
            return _conexao.Amigo.ToList();
        }
    }
}
