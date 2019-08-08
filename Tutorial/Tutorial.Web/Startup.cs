using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Tutorial.Web.Data;
using Tutorial.Web.Models;
using Tutorial.Web.IServices;
using Tutorial.Web.Services;

namespace Tutorial.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //var connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options => { options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));});
            services.AddScoped<IRepository<Student>, EFCoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //app.UseFileServer();
            //app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/node_Modules",
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_Modules"))
            });

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(builder => 
            {
                builder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            //logger.LogInformation("--------Configure");
            //app.Use(next=> 
            //{
            //    logger.LogInformation("app.Use()........");
            //    return async httpContext =>
            //    {
            //        logger.LogInformation("-------async httpContext");
            //        if (httpContext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            logger.LogInformation("---------/first");
            //            await httpContext.Response.WriteAsync("First!!!");
            //        }
            //        else
            //        {
            //            logger.LogInformation("--------- next(httpContext)");
            //            await next(httpContext);
            //        }
            //    };
            //});

            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/welcome"
            //});
        }
    }
}
