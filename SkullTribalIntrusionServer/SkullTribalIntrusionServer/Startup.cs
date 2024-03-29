﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkullTribalIntrusionServer.CoreBase;
using SkullTribalIntrusionServer.ServerSystems;
using SkullTribalIntrusionServer.ServerSystems.Languages;

namespace SkullTribalIntrusionServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Language.CreateLanguage();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSignalR();
            services.AddDbContext<GameDBContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
        pattern: "{controller=Message}/{action=Get2}");

        //        endpoints.MapControllerRoute(
        //name: "default",
        //pattern: "{controller=SyncData}/{action=Get}");
            });

            //Khởi tạo automapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfig());
            });
            var mapper = config.CreateMapper();

            Systems.Mapper = new Mapper(config);
        }
    }
}
