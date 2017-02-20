using Swashbuckle.Application;
using System;
using System.Net.Http;
using System.Web.Http;
using WebApiTier.App_Start;
using WebApiTier.Helpers.Swagger;

namespace WebApiTier
{
    /// <summary>
    /// Class SwaggerConfig.
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.RootUrl(req => new Uri(req.RequestUri, req.GetRequestContext().VirtualPathRoot).ToString());
                c.UseFullTypeNameInSchemaIds();
                c.SingleApiVersion("v1", "TitanNetwork API");
                c.OperationFilter<AssignOAuth2SecurityRequirements>();
                c.IncludeXmlComments($"{AppDomain.CurrentDomain.BaseDirectory}/bin/Documentation.XML");
            })
            .EnableSwaggerUi("docs/ui/{*assetPath}", c =>
            {
                c.InjectJavaScript(typeof(Startup).Assembly, "WebApiTier.Helpers.Swagger.SwaggerOAuthRegistration.js");
            });
        }      
    }
}