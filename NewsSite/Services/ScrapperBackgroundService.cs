using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewsSite.Data;
using NewsSite.Scrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsSite.Services
{
    public class ScrapperBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public ScrapperBackgroundService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {          
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var scrapper = scope.ServiceProvider.GetRequiredService<IScrapperService>();
                    Console.WriteLine("Running scrapper..." + DateTime.Now.ToString());
                    await scrapper.RunSegaScrapper(DateTime.Now.AddDays(-10), DateTime.Now);
                    Console.WriteLine("Finished scrapper..." + DateTime.Now.ToString());
                }

                await Task.Delay(1800000, stoppingToken);
            }
        }
    }
}
