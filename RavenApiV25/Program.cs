using Microsoft.Owin.Hosting;
using System;

namespace RavenApiV25
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:60000";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"RavenApiV2.5 running on {url}");
                Console.WriteLine("Hit <enter> to quit");
                Console.ReadLine();
            }
        }
    }
}
