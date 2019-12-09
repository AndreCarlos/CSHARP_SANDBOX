using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGroup.MVVM.Model
{
    public sealed class AmigoModel
    {
        public string Nome { get; set; }

        public string  Email { get; set; }

        public string Telefone { get; set; }

        public DateTime? DataNascimento { get; set; }
    }
}