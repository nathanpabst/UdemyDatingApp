using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UdemyDatingApp.API.Data;

namespace UdemyDatingApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // create a ref to create host builder
            var host = CreateHostBuilder(args).Build();
            // get an instance of data context
            // add try/catch for access to error handling
            // assign data context to the context variable
            // use the migrate method to apply any pending migrations to the db and creates the db if necessary
            // NOTE: Migrate() method will check for any pending migrations each time the dotnet watch run is called
            // ..and seed the db with users
            // log any exceptions
            // run dotnet ef database drop in the .API, select Y, then dotnet watch run to build the new db
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    Seed.SeedUsers(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
