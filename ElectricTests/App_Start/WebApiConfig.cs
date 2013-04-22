using System.Web.Http;
using ElectricTests.Model;

namespace ElectricTests
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Return api action result as JSON
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //ODataModelBuilder modelBuilder = new ODataModelBuilder();
            //modelBuilder.EntitySet<Question>("Questions");

            //Microsoft.Data.Edm.IEEdmModel model = modelBuilder.GetEdmModel();
            //config.Routes.MapODataRoute("ODataRoute", "odata", model);
        }
    }
}
