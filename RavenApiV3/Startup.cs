using Owin;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace RavenApiV3
{
    using Microsoft.Owin;
    using System.Diagnostics;

    public class Startup
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(InitStore);

        public static IDocumentStore Store { get; } = store.Value;

        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.Use<TimingMiddleware>();

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);

            app.UseWebApi(configuration);
        }

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

    public class TimingMiddleware : OwinMiddleware
    {
        public TimingMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            context.Response.OnSendingHeaders(x =>
            {
                var elapsed = sw.ElapsedMilliseconds;
                context.Response.Headers.Append("X-Elapsed-Time", $"{elapsed}ms");
                Console.WriteLine($"Completed request to {context.Request.Path} in {elapsed}ms");
            }, null);

            await Next.Invoke(context);
        }
    }
}
