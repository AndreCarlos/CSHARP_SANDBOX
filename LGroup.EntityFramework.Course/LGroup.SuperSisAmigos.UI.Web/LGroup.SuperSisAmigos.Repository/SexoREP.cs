using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Importamos a namespace de modelos pra manipular os DADOS
using LGroup.SuperSisAmigos.Model;

//Importamos a namespace de ACESSO A DADOS
using LGroup.SuperSisAmigos.DataAccess;

namespace LGroup.SuperSisAmigos.Repository
{
    //Aplicamos a herança de código, fomos obrigados
    //A implementar todos os comandos que foram declarados
    //Na Interface
    //Nesse momento aplicamos uma espécie de POLIMORFISMO
    //Mandamos uma informação pra dentro do PAI
    public sealed class SexoREP: Base.IBaseREP<SexoMOD>
    {
        public void Cadastrar()
        {
            throw new NotImplementedException();
        }

        public void Atualizar()
        {
            throw new NotImplementedException();
        }

        public void Remover(int codigo)
        {
            throw new NotImplementedException();
        }

        public List<SexoMOD> Listar()
        {
            //Criamos uma coleção para armazenar os
            //Sexos Convertidos (SEXOS QUE VIERAM DA TABELA
            //PRA DENTRO DO MODELO)
            var sexos = new List<SexoMOD>();

            using (var conexao = new SISAMIGOSEntities())
            {
                //Pegamos todos os registros da tabela
                //E trouxemos pra memória
                var registros = conexao.TB_SEXO.ToList();

                //FIzemos um looping, percorremos registro
                //Por registro
                foreach (var sexoCorrente in registros)
                {
                    //Pra cada sexo que encontrarmos
                    //Temos que jogar os dados dele
                    //Dentro da classe de modelo
                    var modelo = new SexoMOD();

                    modelo.Codigo = sexoCorrente.ID_SEXO;
                    modelo.Descricao = sexoCorrente.DS_SEXO;

                    sexos.Add(modelo);
                }//FOREACH
            }//USING


            //Devolvemos pra quem nos chamou os dados
            //De Sexo
            return sexos;
        }
    }
}
