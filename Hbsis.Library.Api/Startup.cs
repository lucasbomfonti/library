﻿using Hbsis.Library.Application.Mapper;
using Hbsis.Library.CrossCutting.Helper;
using Hbsis.Library.Data.Seed;
using Hbsis.Library.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Hbsis.Library.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDependencyInjections();
            MapperConfig.RegisterMappings();
            DbRunner.UpdateDatabase();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Library", Version = "v1" });
            });

            services.ConfigureSwaggerGen(options =>
            {
                options.DocumentFilter<SecurityRequirementsDocumentFilter>();
                options.AddSecurityDefinition("Authorization",
                    new ApiKeyScheme
                    {
                        Description = "Token received at Login",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(HandleExceptionHelper));
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library");
                c.RoutePrefix = string.Empty;
            });
        }
    }

    public class SecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>
            {
                new Dictionary<string, IEnumerable<string>>
                {
                    { "Authorization", new string[]{ } },
                    { "Value", new string[]{ } },
                }
            };
        }
    }
}