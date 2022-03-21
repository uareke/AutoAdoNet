using System;
using System.IO;
using AutoAdoNet.Services.Services.Helper.Services;
using AutoAdoNet.Services.Services.User.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AutoAdoNet.Console
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {

            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
            DisposeServices();
            System.Console.WriteLine("Iniciando app");
            System.Console.ReadKey();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            IConfiguration configuration = builder.Build();
            services.AddSingleton(configuration);

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IHelperService, HelperService>();
            services.AddSingleton<IConfiguration>();
            services.AddSingleton<ConsoleApplication>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }


    }
}
