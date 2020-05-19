using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoreApi
{
    public class Startup
    {
        readonly string MyLocalOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string tokenAudience = Configuration.GetValue<string>("TokenAudiences");
            string tokenIssuer = Configuration.GetValue<string>("TokenIssuer");


            services.AddCors(options =>
            {
                options.AddPolicy(MyLocalOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:44310",
                                                          "http://localhost:44310")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(60);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
            });

            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CoreApi")));

            services.AddIdentity<AppUser, AppRole>(opt => opt.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            //password policy should be more strict in prod 
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            byte[] SecretKey = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("TokenKey"));
            services.AddAuthentication(auth =>
            {
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                        ValidateIssuer = true,
                        ValidIssuer = tokenIssuer,// "https://localhost:44353",
                        ValidateAudience = true,
                        ValidAudience = tokenAudience,// "https://localhost:44353", 
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                })                ;
            services.AddControllers();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseHttpsRedirection();
           
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(MyLocalOrigins);

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
