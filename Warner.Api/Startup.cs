using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Warner.Api.DependencyInjection;
using Warner.Api.Mappers;
using Warner.Persistency;
using Warner.Persistency.Options;

namespace Warner.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ProfileConfiguration());
            });
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddAutoMapper(typeof(ProfileConfiguration)); // provide assembly marker type
            string connectionString =
                Configuration.GetSection("ConnectionStrings")["WarnerDatabase"];
            services.AddDbContext<ApplicationDataContext>(
                options => options.UseSqlServer(connectionString));

            var builder = new ContainerBuilder();
            builder.RegisterModule<CqrsModule>();
            builder.RegisterModule<PersistencyModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<AutoMapperModule>();
            builder.Populate(services);

            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            // wire up exceptions handling first of all
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            // then turn on all other middleware
            app.UseMvc();
            app.UseStatusCodePages();
        }
    }
}
