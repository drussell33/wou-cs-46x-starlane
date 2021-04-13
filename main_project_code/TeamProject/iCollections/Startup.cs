using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using iCollections.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using iCollections.Models;

namespace iCollections
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
            var authBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AuthenticationConnection"));
            var appBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("ICollectionsConnection"));
            authBuilder.Password = Configuration["ICollections:ServerPassword"];
            appBuilder.Password = Configuration["ICollections:ServerPassword"];
            //authBuilder.Password = "nju8*ikm";
            //appBuilder.Password = "nju8*ikm";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(authBuilder.ConnectionString));
                        //Configuration.GetConnectionString("AuthenticationConnection"));
            services.AddDbContext<ICollectionsDbContext>(options =>
                 options.UseSqlServer(appBuilder.ConnectionString));
                       //Configuration.GetConnectionString("ICollectionsConnection"));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            // Customize some settings that Identity uses
            services.Configure<IdentityOptions>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                
            });
            services.AddControllersWithViews();
            // Added to enable runtime compilation.
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseDeveloperExceptionPage();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"thumbnails",
                    pattern: "api/image/thumbnail/{id?}",
                    defaults: new { controller = "ImageApi", action = "Thumbnail"}                
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
