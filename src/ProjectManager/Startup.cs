using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using ProjectManager.Data;
using ProjectManager.Models;
using ProjectManager.Entities;
using ProjectManager.Services;
using ProjectManager.Extensions;
using AutoMapper;

namespace ProjectManager
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ProjectManagerContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Add framework services.
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                .AddMvcOptions(o => o.InputFormatters.Add(new XmlDataContractSerializerInputFormatter()));

            services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            ProjectManagerContext projectManagerContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            projectManagerContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Project, ProjectDto>();
                cfg.CreateMap<Project, ProjectWithoutUserStoriesDto>();
                cfg.CreateMap<UserStory, UserStoryDto>();
                cfg.CreateMap<CreateUserStoryDto, UserStory>();
                cfg.CreateMap<UpdateUserStoryDto, UserStory>();
                cfg.CreateMap<CreateProjectDto, Project>();
            });

            app.UseMvc();
        }
    }
}
