using System;
using System.Collections.Generic;


namespace LGroup.Builder.Model
{
    public sealed class ClienteModel
    {
        /// <summary>
        /// clienteModel internamente dá um new nas listas
        /// </summary>
        public ClienteModel()
        {
            ///inicializamos as listas de telefones e fotos
            ///
            Telefones = new List<TelefoneModel>();
            Fotos = new List<FotoModel>();
        }

        //1- aba informações pessoais
        public Int32 Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        //2-aba telefones de contto
        public ICollection<TelefoneModel> Telefones { get; set; }

        //3-aba fotos
        public ICollection<FotoModel> Fotos { get; set; }
    }
}
