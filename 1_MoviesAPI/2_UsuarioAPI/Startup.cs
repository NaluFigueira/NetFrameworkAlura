﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2_UsuarioAPI.Data;
using _2_UsuarioAPI.Models;
using _2_UsuarioAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace _2_UserAPI
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

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<UserDBContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("UserConnection"))
            );
            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                    opt => opt.SignIn.RequireConfirmedEmail = true
                )
                .AddEntityFrameworkStores<UserDBContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<SignUpService, SignUpService>();
            services.AddScoped<SignInService, SignInService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<SignOutService, SignOutService>();
            services.AddScoped<EmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
