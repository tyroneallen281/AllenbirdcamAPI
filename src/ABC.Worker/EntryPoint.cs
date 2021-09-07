using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using ABC.Data.DatabaseContext;
using Quartz;
using Quartz.Impl;
using ABC.DomainService.Services;
using ABC.DomainService.Interfaces;
using ABC.Repository.Repos;
using ABC.Worker.Interfaces;
using ABC.Worker.Worker;
using ABC.Domain.Models;
using System.Threading.Tasks;

namespace ABC.Worker
{
    public class EntryPoint
    {
        public static void Startup()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //construct a scheduler factory
            //NameValueCollection props = new NameValueCollection
            //{
            //    { "quartz.serializer.type", "binary" }
            //};

            //StdSchedulerFactory factory = new StdSchedulerFactory(props);

            IConfiguration config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", true, true)
                  .Build();

            //IScheduler scheduler = factory.GetScheduler().Result;

            serviceCollection.AddDbContext<ABCDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("ConnectionString"), b => b.UseNetTopologySuite()));
            serviceCollection.AddScoped<IImageRepository, ImageRepository>();
            serviceCollection.AddScoped<ISightingRepository, SightingRepository>();
            serviceCollection.AddScoped<IVoteRepository, VoteRepository>();

            serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IImageService, ImageService>();
            serviceCollection.AddScoped<IVoteService, VoteService>();
            serviceCollection.AddSingleton<IImportAIJob, ImportAIJob>();
            serviceCollection.AddSingleton<IOutputAIJob, OutputAIJob>();
            // serviceCollection.AddTransient<WorkerManager>(sp => new WorkerManager(scheduler,));
            config.GetSection("AppSettings").Bind(new AppSettings());
         
            // create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            //var jobFactory = new JobFactory(serviceProvider);

            //scheduler.JobFactory = jobFactory;


            // entry to run app
            var importAIJob = serviceProvider.GetService<IImportAIJob>();
            var outputAIJob = serviceProvider.GetService<IOutputAIJob>();
            Task.Run(() =>
            {
                     //  importAIJob.ImportFolderImagesAsync();
                 outputAIJob.OutputValidSigntings();
            }); 

        }
    }
}
