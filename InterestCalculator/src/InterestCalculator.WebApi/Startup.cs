using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace InterestCalculator.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Info swaggerApiInformation = new Info
            {
                Title = "Interest Calculator Api",
                Description = "This project has develop in order to attend a technical test requested by SoftPlan company.",
                Version = "v1",
                Contact = new Contact
                {
                    Name = "Rafael Corrêa",
                    Email = "rafael.c@outlook.com"
                }
            };

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", swaggerApiInformation);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //bind appsettings.json section into this object
            Options.SwaggerOptions swaggerOptions = new Options.SwaggerOptions();
            Configuration.GetSection(nameof(Options.SwaggerOptions))
                            .Bind(swaggerOptions);

            if (swaggerOptions.JsonRoute != null &&
                swaggerOptions.UIEndpoint != null)
            {
                app.UseSwagger(option =>
                {
                    option.RouteTemplate = swaggerOptions.JsonRoute;
                });

                app.UseSwaggerUI(option =>
                {
                    option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
                });
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
