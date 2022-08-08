using System.Reflection;
using System.Text;
using ChatServer.Migrations;
using ChatServer.Utilities;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ChatServer.Repositories;
using ChatServer.RepositoryInterfaces;
using ChatServer.Hubs;

namespace ChatServer
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audiance"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });

            services.AddSingleton<IAuthRepository, AuthRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IServerRepository, ServerRepository>();

            services.AddMvc();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services
                .AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                .AddPostgres()
                .WithGlobalConnectionString(Configuration["DB:Connection"])
                .ScanIn(Assembly.GetExecutingAssembly()).For.All());

            services.AddSignalR();
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

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
                //Add WS Hub
            });

            Database.EnsureDatabase(Configuration["DB:Connection"],
       "chat_clone");
            app.Migrate();
        }


    }
}
