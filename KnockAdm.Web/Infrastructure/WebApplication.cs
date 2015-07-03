using System.Web.Http;

namespace KnockAdm.Web
{
    public class WebApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                AutoMapperConfig.Register();
                BindingConfig.Register(config);
                CompositionRootConfig.Register(config);
                RoutingConfig.Register(config);

                config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new ValidatedPrimitiveConverter());
            });
        }
    }
}
