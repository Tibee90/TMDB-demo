using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Repository;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using TMDB_demo.Extensions;
using TMDB_demo.Schedulers;
using TMDB_demo.Services;
using TMDB_demo.Services.Interfaces;
using TMDB_demo.WebApi;

namespace TMDB_demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClient("tmdb", c =>
            {
                c.BaseAddress = new System.Uri(Configuration.GetBaseUrl());
            });

            // services
            services.AddScoped<ITmdbApiService, TmdbApiService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ITmdbDataService, TmdbDataService>();
            services.AddScoped<IUpdateSevice, UpdateService>();

            // repositories
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IGenresRepository, GenresRespository>();
            services.AddScoped<IDirectorsRepository, DirectorsRepository>();

            // Add Quartz services
            services.AddHostedService<QuartzHostedService>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add job
            services.AddSingleton<UpdateDatabaseJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(UpdateDatabaseJob),
                cronExpression: Configuration.GetUpdateDatabaseJobCronExpression()));

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Started...");
                });
            });
        }
    }
}
