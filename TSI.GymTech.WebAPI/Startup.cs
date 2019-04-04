using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TSI.GymTech.WebAPI.Startup))]
namespace TSI.GymTech.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}