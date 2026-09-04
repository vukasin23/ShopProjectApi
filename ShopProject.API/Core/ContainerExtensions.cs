using ShopProject.Application.Command;
using ShopProject.Implementation;
using ShopProject.Implementation.Command;
using ShopProject.Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;

namespace ShopProject.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //Ovde ce ici svi use case-ovi
            services.AddTransient<UseCaseHandler>();

            //Commands
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
            services.AddTransient<ICreateStoreCommand, EfCreateStoreCommand>();
            services.AddTransient<ICreateAddressCommand, EfCreateAddressCommand>();
            services.AddTransient<ICreateShippingMethods, EfCreateShippingMethodCommand>();
            services.AddTransient<ICreateCouponCommand, EfCreateCouponCommand>();
            services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
            services.AddTransient<ICreateProductImageCommand, EfCreateProductImageCommand>();
            services.AddTransient<ICreateProductSpecificationCommand, EfCreateProductSpecificationCommand>();   
            //Kreirati komande za --- Addresses, Coupons, Shipping Methods, Products
            //Validators
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<CreateStoreValidator>();
            services.AddTransient<CreateAddressValidator>();
            services.AddTransient<CreateShippingMethodValidator>();
            services.AddTransient<CreateCouponValidator>();
            services.AddTransient<CreateProductValidator>();
            services.AddTransient<CreateProductImageValidator>();
            services.AddTransient<CreateProductSpecificationValidator>();
            services.AddTransient<JwtTokenCreator>();
        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null;
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }
}
