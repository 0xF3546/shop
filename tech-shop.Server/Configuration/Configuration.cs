using Microsoft.AspNetCore.Identity;
using shop.DataAccess.Database;
using shop.DataAccess.User;

namespace shop.Server.Configuration
{
    public static class Configuration
    {
        public static void ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddIdentity<ShopUser, IdentityRole>()
                .AddSignInManager<SignInManager<ShopUser>>()
                .AddDefaultTokenProviders();
        }
    }
}
