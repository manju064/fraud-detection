// Copyright (c) Friss. All rights reserved.

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Friss.FraudDetection.Api.Infrastructure.Middleware;
using Friss.FraudDetection.Api.Modules;
using Friss.FraudDetection.Contracts.Settings;
using Friss.FraudDetection.DataAccess;
using Friss.FraudDetection.Main.Person;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Friss.FraudDetection.Api
{
    /// <summary>
    /// Start up class for the api.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">configuration of application.</param>
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        /// <summary>
        /// Gets application Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets autofacContainer.
        /// </summary>
        public ILifetimeScope AutofacContainer { get; private set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">services collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<FraudDetectionContext>(opt =>
            {
                var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
                var connection = new SqliteConnection(connectionString);

                opt.UseSqlite(connection, x => x.MigrationsAssembly("Friss.FraudDetection.DataAccess"));
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_CONNECTIONSTRING"]);

        }

        /// <summary>
        /// ConfigureContainer is where you can register things directly
        /// with Autofac. This runs after ConfigureServices so the things
        /// here will override registrations made in ConfigureServices.
        /// Don't build the container; that gets done for you by the factory.
        /// </summary>
        /// <param name="builder">autofac builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new RulesModule());

            builder.Register(context =>
            {
                var factory = new RulesSettings.Factory(context.Resolve<IConfiguration>());
                var settings = factory.Build();

                return settings;
            }).SingleInstance();

            // register custom services
            builder.RegisterType<PersonService>().As<IPersonService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">app builder instance.</param>
        /// <param name="env">environment of the app.</param>
        /// <param name="appLifetime">IApplicationLifetime.</param>
        /// <param name="loggerFactory">ILoggerFactory.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime, ILoggerFactory loggerFactory)
        {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddSerilog();

                // Ensure any buffered events are sent at shutdown
                appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);

                loggerFactory.AddFile(this.Configuration.GetSection("Logging:Serilog"));
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

            // add middleware
            app.UseMiddleware<LogResponseMiddleware>();
            app.UseMiddleware<LogRequestMiddleware>();

            // ErrorWrappingMiddleware should be last one in MW pipeline
            app.UseMiddleware<ErrorWrappingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
