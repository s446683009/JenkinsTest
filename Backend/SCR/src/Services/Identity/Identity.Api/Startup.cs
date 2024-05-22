
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Identity.Api.Rquirements;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Identity.Application;
using System.Reflection;
using System.Threading.Tasks;
using Identity.Infrastructure.RDB;
using Identity.Api.Handers;
using Identity.Api.Handlers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Identity.Api.Models.Api;
using Microsoft.OpenApi.Models;
using Identity.Domain.Aggregates.User;
using Identity.Domain.Aggregates.Company;
using Identity.Domain.Aggregates.Role;
using Identity.Infrastructure.RDB.Repository;
using Identity.Application.Queries;
using Identity.Api.Configurations;
using Identity.IApplication;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Services.Common;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var jwtSettings= Configuration.GetSection("JwtSetttings").Get<JwtSetting>();
            var identitySetting= Configuration.GetSection("IdentitySettings").Get<IdentityServerConfig>();
            var connectString = Configuration.GetConnectionString("Identity");
            services.AddSingleton(jwtSettings);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
            
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //

            services.AddHttpLogging(option =>
            {
                option.LoggingFields = HttpLoggingFields.All;
            });
            //添加认证
            services.AddAuthentication(options=>
                {
                  
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //添加后未登录302 变更为401
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    //
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options=>
            {
           
                options.TokenValidationParameters= new TokenValidationParameters()
                {
                    
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
                options.SaveToken = true;
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
               
            });
           
            //添加授权
            services.AddAuthorization(options=> {
          
                options.AddPolicy("permission",p=> {
                    p.Requirements.Add(new PemissionRequirement());
                });
            });
            // Adds IdentityServer
            services.AddIdentityServer(x =>
            {
               
                //使用AddCookieAuthentication 会默认赋值 UserInteraction 
                //使用jwt 则需要额外配置
                x.UserInteraction.LoginUrl = identitySetting.UserInteraction?.LoginUrl;
                x.UserInteraction.LogoutUrl = identitySetting.UserInteraction?.LogoutUrl;
          
                x.UserInteraction.LoginReturnUrlParameter = identitySetting.UserInteraction?.LoginReturnUrlParameter;
                x.IssuerUri = identitySetting.IssueUrl;
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
            services.AddDbContext<IdentityContext>(t => {
                t.UseNpgsql(connectString, options => {
                    options.MigrationsAssembly(typeof(IdentityContext).GetTypeInfo().Assembly.GetName().Name);
                });
            });

         
            services.AddCors(options =>
            {
                // Policy 名稱 CorsPolicy 是自訂的，可以自己改
                options.AddPolicy("CorsPolicy", policy =>
                {
                    // 設定允許跨域的來源，有多個的話可以用 `,` 隔開
                    policy.AllowAnyHeader().AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            //services.AddControllersWithViews();
            //services.AddRazorPages();
            AddCustomSwagger(services,Configuration);

            services.AddTransient<IAuthorizationHandler,PermissionAuthorizationHandler>();
            services.AddScoped<IUserApplication,UserApplication>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<UserService,UserService>();
            services.AddSingleton(jwtSettings);
            var types = typeof(IQuery).Assembly.GetTypes();
            services.AddQueries( types);
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
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHttpLogging();
            app.UseRouting();
            app.UseCors("qwer");
            app.UseAuthentication();
        
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {   // 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
           
            app.UseSwagger()
         .UseSwaggerUI(c =>
         {
             c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Identity.API V1");
             c.OAuthClientId("Identity");
             c.OAuthAppName("Identity Swagger UI");
         });
           
     
     
        }

        public  void AddCustomSwagger( IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
          
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    
                    Title = "SCR Identity HTTP API",
                    Version = "v1",
                    Description = "SCR Identity HTTP API"
                });
                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows()
                //    {
                //        Implicit = new OpenApiOAuthFlow()
                //        {
                //            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
                //            TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
                //            Scopes = new Dictionary<string, string>()
                //            {
                //                { "orders", "Ordering API" }
                //            }
                //        }
                //    }
                //});

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            
        }

    }
}
