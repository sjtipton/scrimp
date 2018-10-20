using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Helpers.Jwt;
using scrimp.Services;
using System;
using System.Text;

namespace scrimp
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;

            BuildAppSettingsProvider();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("Scrimp")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); // TODO is compatibility version necessary?
            services.AddAutoMapper();

            services.AddHttpClient<GreenlitRestApiClient>();

            // JWT Authentication
            // Following https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login
            // as a general guideline for wiring up JWT
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = Configuration["Scrimp:JwtIssuerOptions:Issuer"];
                options.Audience = Configuration["Scrimp:JwtIssuerOptions:Audience"];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            // Token validation
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Scrimp:JwtIssuerOptions:Issuer"],

                ValidateAudience = true,
                ValidAudience = Configuration["Scrimp:JwtIssuerOptions:Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = Configuration["Scrimp:JwtIssuerOptions:Issuer"];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options => {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(JwtClaimIdentifiers.Rol, JwtClaims.ApiAccess));
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IJwtService, JwtService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionAccountService, TransactionAccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IErrorService, ApiErrorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            // TODO Use authentication when ready
            app.UseAuthentication();

            app.UseMvc();
        }

        private void BuildAppSettingsProvider()
        {
            var configuration = new AppSettingsConfiguration
            {
                GreenlitApiUrl = Configuration["Greenlit:ServicePath"]
            };

            AppSettingsProvider.SetGreenlitApiUrl(configuration);
        }
    }
}
