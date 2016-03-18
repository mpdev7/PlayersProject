using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using PlayersProject.Models;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(PlayersProject.Startup))]
namespace PlayersProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }
    }
}
