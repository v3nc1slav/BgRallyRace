using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BgRallyRace.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BgRallyRace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string[] name = new string[]{
        "Ivan",
        "Petyr"
        };

            for (int i = 0; i < name.Length; i++)
            {
                
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
