using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Medicine.Startup))]
namespace Medicine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
