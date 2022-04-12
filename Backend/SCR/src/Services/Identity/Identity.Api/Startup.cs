using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Identity.Api.Rquirements;
using Identity.Api.Models.Configs;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdentityServer4.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Identity.Api.AuthorizeHanders;
using System.Reflection;
using Microsoft.eShopOnContainers.Services.Identity.API.Data;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Identity.Api
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

            var jwtSettings= Configuration.GetSection("JwtSetttings").Get<JwtSetting>();
            var connectString = Configuration.GetConnectionString("Identity");
            services.AddSingleton(jwtSettings);
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //
            services.AddControllersWithViews();
            services.AddAuthentication(options=> {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options=> {
                options.TokenValidationParameters= new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    
                };
                options.SaveToken = true;
            });

            services.AddAuthorization(options=> {
                options.AddPolicy("permission",p=> {
                    p.Requirements.Add(new PemissionRequirement());
                });
            });
            // Adds IdentityServer
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "";
                x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })


            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>builder.UseNpgsql(connectString,
                     sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(migrationsAssembly);
           
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                    });
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectString,
                     sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                    });
            });
            //.Services.AddTransient<IProfileService, ProfileService>();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();


            services.AddTransient<IAuthorizationHandler,PermissionAuthorizationHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider provider,IWebHostEnvironment env,IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {   // �ƶ�·�ɺͿ�������ƥ�����
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
     
            //var context = provider.GetRequiredService<ConfigurationDbContext>();
       
            //context.Database.Migrate();
            //var psContext = provider.GetRequiredService<PersistedGrantDbContext>();
            //psContext.Database.Migrate();
            //new ConfigurationDbContextSeed().SeedAsync(context,configuration).Wait(); 
           
        }
    }
}
