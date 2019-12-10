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
using BookStats.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using BookStats.Hubs;

namespace BookStats
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Models.User, IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddTransient<IRepository, Repository>();
            services.AddSignalR();

             services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddViewLocalization();
            //AddDataAnnotationsLocalization(options => {
            //    options.DataAnnotationLocalizerProvider = (type, factory) =>
            //        factory.Create(typeof(SharedResource));
            //});

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseSignalR(routes =>
            {
                routes.MapHub<CommentsHub>("/comment");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(null, pattern: "Login",
                    defaults: new { controller = "Auth", action = "Login" });
                endpoints.MapControllerRoute(null, pattern: "Register",
                    defaults: new { controller = "Auth", action = "Register" });
                endpoints.MapControllerRoute(null, pattern: "Books",
                    defaults: new { controller = "Books", action = "Index"});
                endpoints.MapControllerRoute(null, pattern: "Books/Create",
                    defaults: new { controller = "Books", action = "Create" });
                endpoints.MapControllerRoute(null, pattern: "Roles/Edit",
                    defaults: new { controller = "Roles", action = "Edit" });
                endpoints.MapControllerRoute(null, pattern: "Roles/Create",
                    defaults: new { controller = "Roles", action = "Create" });
                endpoints.MapControllerRoute(null, pattern: "Roles/Users",
                        defaults: new { controller = "Roles", action = "UserList" });
                endpoints.MapControllerRoute(null, pattern: "Users",
                    defaults: new { controller = "Users", action = "Index" });

                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(null, pattern: "Privacy", 
                    defaults: new { controller = "Home", action = "Privacy" });
            });
        }
    }
}
