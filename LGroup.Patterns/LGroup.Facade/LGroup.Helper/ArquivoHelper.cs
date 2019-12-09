using System;

//subimos para memoria a namespace de menipulaçao de pastas e arquivos
using System.IO;

namespace LGroup.Helper
{
    /// <summary>
    /// Se a classe nao armazenar nada dentro dela, se ela nao tiver variaveis ou propriedades
    /// (comandos) podem e devem ser estatico (não precisa dar new na classe)
    /// </summary>
    public sealed class ArquivoHelper
    {
        public static void Gerar(string caminho_, string conteudo_)
        {
            if (!Directory.Exists(@"C:\processamento"))
                Directory.CreateDirectory(@"C:\processamento");

            using (var arquivo = new StreamWriter(caminho_))
            {
                arquivo.WriteLine(conteudo_);
                arquivo.Close();
            }
        }
    }
}
