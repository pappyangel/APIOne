using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace cocktails
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
                {
                    var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
                    var envKVToken = Environment.GetEnvironmentVariable("AzureServicesAuthConnectionString");

                    var settings = config.Build();

                    // To Do: use Managed Identity when move to Azure

                    var keyVaultEndpoint = settings["AzureKeyVaults:defaultEndpoint"];

                    //var azureServiceTokenProvider;
                    if (!string.IsNullOrEmpty(keyVaultEndpoint))
                    {
                        var azureServiceTokenProvider = new AzureServiceTokenProvider();

                        var keyVaultClient = new KeyVaultClient(
                            new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                        string keyvaultURI = $"https://{keyVaultEndpoint}.vault.azure.net/";

                        config.AddAzureKeyVault(keyvaultURI, keyVaultClient, new DefaultKeyVaultSecretManager());

                    }

                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
