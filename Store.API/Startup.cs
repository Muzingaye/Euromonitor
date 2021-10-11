using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace Store.API
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
            services.AddSession();
            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        
        //services.AddCors(option =>
        //    {
        //        option.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
        //    });

            //services.AddCors(o =>
            //{
            //    o.AddPolicy("CorsPolicy", builder =>
            //    {
            //        builder.WithOrigins("localhost:4200");
            //    });
            //});

            services.AddControllers();


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "Store",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "My API",
                        Version = "1",
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/Store/swagger.json",
                    "Store Gateway API");
             //   setupAction.RoutePrefix = "StoreGateway";
            });
          
            app.UseCookiePolicy();
            app.UseSession();

            app.UseCors("CorsPolicy");

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("token", token);
                }

                await next();
            });
            
         
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
           // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Client";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
