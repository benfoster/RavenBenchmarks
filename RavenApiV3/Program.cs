using Microsoft.Owin.Hosting;
using System;

namespace RavenApiV3
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:60001";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"RavenApiV3 running on {url}");
                Console.WriteLine("Hit <enter> to quit");
                Console.ReadLine();
            }
        }
    }
}
