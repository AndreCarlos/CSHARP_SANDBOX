using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

///A microsoft já tem um pacote que nos ajuda a fazer o bundle e o minification dos arquivos
///Bundle = agrupamento de arquivos
///Minification = é a tecnica de compactar os arquivos. È a tecnica de reduzir as linhas de código
///(mata os espaços em branco, mata os comentários, mata as quebras de linha e ainda reduz o nome das
///variáveis)
using System.Web.Optimization;

namespace LGroup.SuperSisAmigos.UI.Web.App_Start
{
    /// <summary>
    /// Para cada arquivo CSS ou JS que você importou na página o navegador envia um pedido HTTP para
    /// o servidor
    /// 1 arquivo -> um pedido. Isso deixa a aplicaçao lenta, quanto mais CSS e JS estiverem importados na
    /// página, mais lenta vai ficar 
    /// Para ficar mais rápido (performance) diminui a quantidade de idas e vindas do servidor. Vamos fazer o 
    /// BUNDLE
    /// BUNDLE -> Agrupamento de arquivos
    /// BUNDLE DE CSS
    /// BUNDLE DE JS
    /// 
    /// Se não fosse Microsoft, poderíamnos fazer isso com GRUNT/GULP
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Quando baixarmos o pacote Web Optimization, lembrar de baixar também
        /// o pacote mais rescente do Json.Net, como estamos com o MVC 5.2 ele precisa do
        /// pacote mais rescente do Json.Net
        /// </summary>
        
        public static void RegisterBundles(BundleCollection agrupador)
        {
            ///fizemos o bundle dos CSS
            ///Ao agrupar os arquivos, temos que colocar em uma pasta que não existe no projeto (CSS)
            ///quando executarmos, é criado internamente
            var meusEstilos = new StyleBundle("~/CSS");
            meusEstilos.Include("~/Content/bootstrap.min.css",
                                "~/Content/bootstrap-theme.min.css");

            ///fizemos o bundle dos scripts
            ///
            ///Se um dia atualizar a versão do jquery e do jquery ui
            ///Temos que mexer no codigo (bundle) par nao ter que mexer colocar o comando {version} 
            ///Ele descobre automaticament o versao do jquery e do jquery ui
            var meusScripts = new ScriptBundle("~/JS");
            meusScripts.Include("~/Scripts/jquery-{version}.min.js",
                                 "~/Scripts/bootstrap.min.js",
                                "~/Scripts/jquery.maskedinput.min.js",
                                "~/Scripts/jquery-ui-{version}.min.js",
                                "~/Scripts/jquery.validate.min.js",
                                "~/Scripts/jquery.validate.unobtrusive.min.js");

            ///depois de agrupar os arquivos, os jogamos dentro do agrupador, dentro do local de agrupamento
            ///de arquivos
            agrupador.Add(meusEstilos);
            agrupador.Add(meusScripts);

            ///Foi nesse momento que ele agrupou todos os arquivos e fez a compactação (minification)
            ///
            ///Quando agrupamos os arquivos eles receberam um id, hash hexadecimal grande, esse hash
            ///só muda quando colocarmos ou tirarmos algum arquivo do bundle (CSS ou JS)
            ///é pelo hash que o asp.net sabe se o bundle foi modificado ou não
            ///Ele fica no cache do serviodr por 1 ano 
            BundleTable.EnableOptimizations = true;
        }
    }
}