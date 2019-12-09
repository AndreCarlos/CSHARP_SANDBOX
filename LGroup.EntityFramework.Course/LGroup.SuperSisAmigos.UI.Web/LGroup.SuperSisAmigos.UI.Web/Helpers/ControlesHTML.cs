using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Importamos uma namespace pra poder visualizar as
//Classes pra geração e manipulação dos HELPERS
using System.Web.Mvc;

namespace LGroup.SuperSisAmigos.UI.Web.Helpers
{
    //As classes dos HELPERS obrigatoriamente precisam
    //Ser estáticas, não precisamos dar NEW nela pra
    //Poder utilizar
    public static class ControlesHTML
    {
        //Quando criarmos HTML HELPERS, o método tem que
        //Ser estático e tem que retornar a classe
        //MVCHTMLSTRING
        public static MvcHtmlString BR(this HtmlHelper classe)
        {
            //Montamos no C# um HELPERS pra gerar <BR/>
            //Utilizando a classe TAGBUILDER
            var quebraLinha = new TagBuilder("br");

            //Descarregamos a TAG BR lá pra VIEW, a enumeração
            //TagRenderMode nos ajuda a retornar a TAG em 1 das
            //Formas abaixo:
            //<p>
            //</p>
            //<p> </p>
            //<br />
            return MvcHtmlString.Create
                (quebraLinha.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString INPUT(this HtmlHelper classe,
            String type, String name,
            String id, String value, String classBootstrap)
        {
            var campo = new TagBuilder("input");

            //Pegamos os parametros de entrada que vieram lá
            //Da página e jogamos eles dentro do HTML em forma
            //De Atributos
            campo.Attributes.Add("type", type);
            campo.Attributes.Add("name", name);
            campo.Attributes.Add("id", id);
            campo.Attributes.Add("value", value);

            //PRa vincular uma classe CSS dentro da TAG que estamos
            //Criando ou chamar o ATTRIBUTES ou chamar ADDCSSCLASS
            campo.AddCssClass(classBootstrap);

            return MvcHtmlString.Create(campo.ToString(TagRenderMode.SelfClosing));
        }

        //Criamos um HTML HELPER pra gerar 2 TAGS
        //1 Dentro da outra
        public static MvcHtmlString LINKWITHIMAGE
          (this HtmlHelper classe, String href, String src)
        {
            //Criamos um HYPERLINK
            var link = new TagBuilder("a");
            link.Attributes.Add("href", href);

            //Criamos uma IMAGEM
            var imagem = new TagBuilder("img");
            imagem.Attributes.Add("src", src);

            //PRa fazer uma tag dentro da outra
            //Concatenamos o html das tags dentro de uma
            //STRINGONA
            var html = link.ToString(TagRenderMode.StartTag);
            html += imagem.ToString(TagRenderMode.SelfClosing);
            html += link.ToString(TagRenderMode.EndTag);

            return MvcHtmlString.Create(html);
        }
    }
}