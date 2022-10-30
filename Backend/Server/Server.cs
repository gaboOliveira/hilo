using Domain.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

namespace Server
{
    public static class Server
    {
        public static IHostBuilder SetServerUP(this WebApplicationBuilder hostBuilder)
        {
            return hostBuilder.Host.UseOrleans((context, siloBuilder) =>
            {
                siloBuilder
                    .UseLocalhostClustering()
                    .UseDashboard(options =>
                    {
                        options.Username = context.Configuration[ConstantSettings.Orleans.Dashboard.UserName];
                        options.Password = context.Configuration[ConstantSettings.Orleans.Dashboard.Password];
                        options.Port = context.Configuration.GetValue<int>(ConstantSettings.Orleans.Dashboard.Port);
                        options.CounterUpdateIntervalMs = 10000;
                    });
            });
        }
    }
}
