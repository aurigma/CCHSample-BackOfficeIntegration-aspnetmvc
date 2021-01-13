using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aurigma.AssetStorage;
using Aurigma.AssetProcessor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesignBrowserMVC
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

            services.Configure<CustomersCanvasOptions>(Configuration.GetSection(CustomersCanvasOptions.SectionName));
            services.AddSingleton<TokenService>();

            services.AddScoped<Aurigma.AssetStorage.IApiClientConfiguration, Configuration.MyAssetStorageApiClientConfiguration>();
            services.AddScoped<Aurigma.AssetProcessor.IApiClientConfiguration, Configuration.MyAssetStorageApiClientConfiguration>();

            services.AddHttpClient<IDesignsApiClient, DesignsApiClient>();
            services.AddHttpClient<IDesignProcessorApiClient, DesignProcessorApiClient>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Designs}/{action=Index}/{id?}");
            });
        }
    }
}
