using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

//Esses 3 usings servem para abrir, manipular o protocolo oData(EDMX)
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using LGroup.Service.EdmxRemoto.DataAccess;

namespace LGroup.Service.EdmxRemoto
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //The WebApiConfig class may require additional changes to add a route for this controller. 
            //Merge these statements into the Register method of the WebApiConfig class as applicable. 
            //Note that OData URLs are case sensitive.

            //Todas essas linhas de codigo sao referentes ao protocolo OData
            //Eh aqui que ele define quais tableas vao ser acessadas remotamente
            //Literalmente quando utilizamos o oData transformamos o HTTP em uma tabela e podemos
            //efetuar consulta diretamente no HTTP (ORDER BY, FILTER)
             ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            //Quando transmitimos uma tabela via oData ele nao sabe qual campo eh PK
            //chave primária(quando damos o Add Service Reference = dá ero!)
            //definimos manualmente no oData a PK da table de produto
             builder.EntitySet<TB_PRODUTO>("TB_PRODUTO").EntityType.HasKey(x => x.ID_PRODUTO);

            //Esse nome oData eh o nome que temos que passar na URL(ROTA)
             config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
