using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace vehicles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // services.Configure<Microsoft.AspNetCore.Http.CookiePolicyOptions>(options =>
            // {
            //     options.CheckConsentNeeded = context => true;
            //     options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            // });

            // Замість AddMvc використовуємо AddControllersWithViews()
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/VehicleRegistration/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization(); // Додай, якщо потрібна авторизація

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=VehicleRegistration}/{action=Index}/{id?}");
            });
        }
    }
}
