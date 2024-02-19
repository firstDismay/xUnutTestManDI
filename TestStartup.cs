﻿using ManDI;
using ManDI.build;
using ManDI.executor;
using ManDI.extractor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace xUnitTestManDI
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // добавляем поставщика переменных окружения в конфигурацию
            var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
                .Build();

            //Добавляем логирование
            services.AddSingleton<ILoggerFactory>(LoggerFactory.Create(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
            }));

            ManDiOptions options = new ManDiOptions()
            {
                //Получаем значения переменных окружения
                ConnectionStrings = string.Format(
                    @"Host={0};Database={1};Port={2}; Username={3};Password={4};",
                configuration["SERVER"],
                configuration["DATABASE"],
                configuration["PORT"],
                configuration["USER"],
                configuration["PASSWORD"])
            };

            // Add services to the container.
            services.AddManDI(options,
               services => services.AddScoped<ISignatureExtractor, SignatureExtractorForComposite>(),
               services => services.AddScoped<ICommandExecutor, command_executor>(),
               services => services.AddScoped<IExportExecutor, export_executor>());

        }
    }

}
