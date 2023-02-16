using System;
using System.IO;
using Aurigma.AssetProcessor;
using Aurigma.AssetStorage;
using Aurigma.DesignAtomsApi;
using Aurigma.StorefrontApi;
using Aurigma.StorefrontApi.Products;
using CustomersCanvasSample.Db;
using CustomersCanvasSample.Services;
using CustomersCanvasSampleMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomersCanvasSample
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
            services.AddTransient<EcommerceDataService>();
            services.AddTransient<DataSourceService>();

            services.AddScoped<Aurigma.AssetStorage.IApiClientConfiguration, Configuration.CustomersCanvasApiClientConfiguration>();
            services.AddScoped<Aurigma.DesignAtomsApi.IApiClientConfiguration, Configuration.CustomersCanvasApiClientConfiguration>();
            services.AddScoped<Aurigma.AssetProcessor.IApiClientConfiguration, Configuration.CustomersCanvasApiClientConfiguration>();
            services.AddScoped<Aurigma.StorefrontApi.IApiClientConfiguration, Configuration.CustomersCanvasApiClientConfiguration>();

            services.AddHttpClient<IDesignsApiClient, DesignsApiClient>();
            services.AddHttpClient<IDesignAtomsServiceApiClient, DesignAtomsServiceApiClient>();
            services.AddHttpClient<IDesignProcessorApiClient, DesignProcessorApiClient>();
            services.AddHttpClient<IProductSpecificationsApiClient, ProductSpecificationsApiClient>();
            services.AddHttpClient<Aurigma.StorefrontApi.IProductReferencesApiClient, Aurigma.StorefrontApi.ProductReferencesApiClient>();
            services.AddHttpClient<IProjectsApiClient, ProjectsApiClient>();
            services.AddHttpClient<IProductsApiClient, ProductsApiClient>();

            services.AddDbContext<EcommerceContext>
                (o => o.UseSqlite(Configuration.GetConnectionString("EcommerceData")));

            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);
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

            // Set up App_Data folder
            string baseDir = env.ContentRootPath;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(baseDir, "App_Data"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
