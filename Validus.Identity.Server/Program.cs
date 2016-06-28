using Microsoft.Owin.Hosting;
using Serilog;
using System;
using System.Diagnostics;

namespace Validus.Identity.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Validus Identity Server";

            Log.Logger = new LoggerConfiguration()
               .WriteTo
               .LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
               .CreateLogger();

            using (WebApp.Start<Startup>(Config.Host))
            {
                Console.WriteLine($"{Console.Title} running...{Environment.NewLine}");

                Console.WriteLine("Hit the 'Spacebar' key to open the identity server home page...");
                //Console.WriteLine("Hit the 'A' key to open the identity server administration page...");
                Console.WriteLine("Hit the 'M' key to open the identity membership administration page...");

                ConsoleKey key;

                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
                {
                    if (key == ConsoleKey.Spacebar) Process.Start($"{Config.Host}/core");

                    if (key == ConsoleKey.A) Process.Start($"{Config.Host}/admin");
                    if (key == ConsoleKey.M) Process.Start($"{Config.Host}/membership");
                }
            }
        }
    }
}