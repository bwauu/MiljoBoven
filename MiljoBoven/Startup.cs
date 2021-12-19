using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MiljoBoven.Models;

namespace MiljoBoven
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config) => Configuration = config;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>
                ().AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddTransient<IEnvironmentCrimeRepository, EFEnvironmentCrimeRepository>();  // Här talar vi om att vi vill arbeta med Interfacet och implementeringen
            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting(); // Hittar middleware.
            // auth & author måste komma innan userouting men efter endpoints
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

    }
}