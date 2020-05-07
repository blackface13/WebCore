using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGame.CoreBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace APIGame
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

            services.AddDbContext<GameDBContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(o =>
            //    {

            //    });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=AutoUpdate}/{action=Get}/{id?}");


                endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=HourReward}/{action=GetHourReward}/{id?}");

                endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Player}/{action=GetPlayer}/{id?}");

                endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Chatbox}/{action=Index}");

            });
        }
    }
}
