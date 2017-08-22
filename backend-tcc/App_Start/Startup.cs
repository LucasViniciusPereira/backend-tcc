using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(backend_tcc.api.App_Start.Startup))]

namespace backend_tcc.api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DbConnectionString");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
