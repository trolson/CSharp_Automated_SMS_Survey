using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SMS_Example_Survey
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            //FinishStartup.Start();
            host.Run();

            
        }
    }
}
