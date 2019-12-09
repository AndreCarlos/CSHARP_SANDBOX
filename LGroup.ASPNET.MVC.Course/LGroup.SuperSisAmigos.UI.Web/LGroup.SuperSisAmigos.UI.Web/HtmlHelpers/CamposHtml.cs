using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Inmportamos esta namespace para poder visualizar as classes de manipulação e geração dos HTML HELPERS:
//HtmlHelper e MvcHtmlString
using System.Web.Mvc;

namespace LGroup.SuperSisAmigos.UI.Web.HtmlHelpers
{
    //No MVC já temos diversos HTML HELPERS, são comandos em RAZOR (C#) que viram HTML, é uma forma mais produtiva
    //de desenhar a tela só que nem tudo tem HTML HELPER (div, table, br, button, ul, li) tudo que não tem html helper
    //tem que montar na raça com html puro
    //A classe que vai armazenar o html helpers obrigatoriamente tem que ser estática., significa que não precisa dar 
    //NEW para usá-la
    public static class CamposHtml
    {
        //Obrigatoriamente todos os html helpers customizados tem que retornar um MvcHtmlString (elemento html que 
        //estamos montando em formato de string)
        //Para os html helpers grudarem nas classes temos que tranformá-los em métodos de extensão 
        public static MvcHtmlString Br(this HtmlHelper classeHospedeira)
        {
            //Criamos a tag <br>
            //A classe tabbuilder serve para criar(montar) um elemento html, numca colcoar os sinais < / >, <br>
            //podemos montar qualquer elemento
            var quebraLinha = new TagBuilder("br");

            //Após montar o comando br, o mandamos lá para tela (view) retornmanoos para o navegador
            //É nesse momento que ele coloca os sinais da TAG <  >  /
            //Ao mandar o elemento para tela podemos configurar como ele vai se fechar
            //SelfClosing >> Auto fecha <br/>
            //StartTag >> somente abre <br>
            //EndTag >> fecha <br/>
            //Normal >> ele sabre para depois fechar <br> <br/>
            return MvcHtmlString.Create(quebraLinha.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Button(this HtmlHelper classeHospedeira, string nomeBotao, string classeCSS, string tipoBotao,
                                           string textoBotao)
        {
            var botao = new TagBuilder("input");
     
            //pegamos as informações que viram da tela via paramentro de entrada e geraos os atributos do botao
            botao.Attributes.Add("name", nomeBotao);
            botao.Attributes.Add("id", nomeBotao);
            botao.Attributes.Add("value", textoBotao);
            botao.Attributes.Add("type", tipoBotao);

            //Como a classe CSS é mais importante que os demais atributos, temos um comando exclusivo para adicionar a classe CSS
            botao.AddCssClass(classeCSS);

            return MvcHtmlString.Create(botao.ToString(TagRenderMode.SelfClosing));
        }
    }
}