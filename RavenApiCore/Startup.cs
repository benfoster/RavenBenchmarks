using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raven.Client;
using Raven.Client.Document;

namespace RavenApiCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(x => InitStore());

            services.AddMvc(options =>
            {
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.MinimumLevel = LogLevel.Information;
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseMvc(routes =>
            {
                //routes.MapRoute("Default", "values", new { controller = "Values", action = "Get" });
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        private static IDocumentStore InitStore()
        {
            var store = new DocumentStore
            {
                Url = "http://localhost:8090",
                DefaultDatabase = "raven-benchmark"
            };

            store.Initialize();
            return store;
        }
    }
}
