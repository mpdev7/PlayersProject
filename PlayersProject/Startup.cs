using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using PlayersProject.Models;
using System.Web.Http;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(PlayersProject.Startup))]
namespace PlayersProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var builder = new CreateContainer();         
            var container = builder.GetContainer();




            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = depe
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            app.UseAutofacMiddleware(container);
            

        }
    }
}
