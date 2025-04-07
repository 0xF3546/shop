using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Category;
using shop.Core.Product;
using shop.DataAccess.Category;
using shop.DataAccess.Database;
using shop.DataAccess.Product;

namespace shop.DataAccess
{
    public static class Configuration
    {
        public static void ConfigureDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IProductCrud, ProductRepository>();
            services.AddScoped<ICategoryCrud, CategoryRepository>();
        }
    }
}
