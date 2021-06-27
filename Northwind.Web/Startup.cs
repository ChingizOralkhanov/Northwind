using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Db.Models;
using Northwind.Db.Repositories;
using Northwind.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web
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
            services.AddControllersWithViews();
            services.AddDbContext<NorthwindDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("NorthWind")));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            RegistrationMapper(services);

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegistrationMapper(IServiceCollection services)
        {
            var configurationExpression = new AutoMapper.Configuration.MapperConfigurationExpression();
            configurationExpression.CreateMap<Category, CategoryViewModel>();
            configurationExpression.CreateMap<CategoryViewModel, Category>();

            configurationExpression.CreateMap<Supplier, SupplierViewModel>();
            configurationExpression.CreateMap<SupplierViewModel, Supplier>();

            configurationExpression.CreateMap<Product, ProductViewModel>();
            configurationExpression.CreateMap<ProductViewModel, Product>();



            var mapperConfiguration = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(mapperConfiguration);
            services.AddScoped<IMapper>(s => mapper);
        }

    }
}
