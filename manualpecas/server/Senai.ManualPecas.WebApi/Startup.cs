﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Senai.ManualPecas.WebApi
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            //configurar Mvc, Swagger, Authorizes ...
            services.AddMvc()
               .AddJsonOptions(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
               })
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
                   c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                   {
                       Title = "ManualPecas API",
                       Version = "v1"
                   })
            );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chave-autenticacao-manual-pecas")),
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidIssuer = "ManualPecas.WebApi",
                    ValidAudience = "ManualPecas.WebApi"
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Usar Mvc, Swagger, Authorizes ...
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ManualPecas API V1");
            });
            app.UseMvc();
        }
    }
}
