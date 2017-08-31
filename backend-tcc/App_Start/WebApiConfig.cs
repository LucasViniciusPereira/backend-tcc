using System.Globalization;
using System.Net.Http.Formatting;
using System.Web.Http;
using backend_tcc.api.App_Start;
using System.Web.Http.Cors;

namespace backend_tcc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("*", "Content-Type", "GET, POST, PUT, DELETE", "Token");
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API configuration and services

            config.MessageHandlers.Add(new PreflightRequestsHandler());
            config.MessageHandlers.Add(new TokenInspector());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #region MediaTypeFormatter

            //config.Formatters.Clear();

            //XML
            //config.Formatters.Add(new XmlMediaTypeFormatter());
            //config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "text/xml");
            //config.Formatters.XmlFormatter.Indent = true;

            //JSON
            //config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Culture = CultureInfo.GetCultureInfo("pt-BR"),
                DateFormatString = "dd/MM/yyyy",
                Formatting = Newtonsoft.Json.Formatting.Indented
            };

            #endregion

            SwaggerConfig.Register();


        }
    }
}
