using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using shop.DataAccess;
using shop.DataAccess.Database;

namespace shop.Server.Configuration
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        private readonly Logger<Startup> _logger;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenApiDocument(c =>
                c.PostProcess = document =>
                {
                    document.Info = new NSwag.OpenApiInfo
                    {
                        Title = "Tech Shop API",
                        Version = "v1",
                        Description = "API for Tech Shop",
                        Contact = new NSwag.OpenApiContact
                        {
                            Name = "Tech Shop",
                            Email = ""
                        }
                    };
                }
            );

            services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = 403;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddCors(c =>
            {
                c.AddDefaultPolicy(new CorsPolicyBuilder().WithOrigins(_configuration.GetSection("CorsUrls").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .Build());
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("AppDbContext"))
            );

            services.ConfigureCoreServices();
            services.ConfigureDataAccess();
            services.ConfigureDatabase(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
