using ShopProject.Application.Command;
using ShopProject.Application.Query;
using ShopProject.Implementation;
using ShopProject.Implementation.Command;
using ShopProject.Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;
using ShopProject.Application;
using ShopProject.Implementation.Logging.Errors;
using ShopProject.Implementation.Logging.UseCases;
using ShopProject.Implementation.Query;

namespace ShopProject.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            //Ovde ce ici svi use case-ovi
            services.AddTransient<UseCaseHandler>();
            services.AddTransient<IErrorLogger, EfErrorLogger>();
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
            services.AddTransient<ICreateInventoryCommand, EfCreateInventoryCommand>();
            services.AddTransient<ICreateWishlistItemCommand, EfCreateWishlistItemCommand>();
            services.AddTransient<ICreateCartCommand, EfCreateCartCommand>();
            services.AddTransient<ICreateCartItemCommand, EfCreateCartItemCommand >();
            services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
            services.AddTransient<ICreateOrderLineCommand, EfCreateOrderLineCommand>();
            services.AddTransient<IDeleteCartItemCommand, EfDeleteCartItemCommand>();
            services.AddTransient<IDeleteWishlistItemCommand, EfDeleteWishlistItemCommand>();
            services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
            services.AddTransient<IDeleteStoreCommand, EfDeleteStoreCommand>();
            services.AddTransient<IDeleteShippingMethodCommand, EfDeleteShippingMethodCommand>();
            services.AddTransient<IDeleteCouponCommand, EfDeleteCouponCommand>();
            services.AddTransient<IDeleteProductImageCommand, EfDeleteProductImageCommand>();
            services.AddTransient<IDeleteProductSpecificationCommand, EfDeleteProductSpecificationCommand>();
            services.AddTransient<IDeleteInventoryCommand, EfDeleteInventoryCommand>();
            services.AddTransient<IDeleteProductCommand, EfDeleteProductCommand>();
            services.AddTransient<IDeleteAddressCommand, EfDeleteAddressCommand>();
            services.AddTransient<IDeleteOrderLineCommand, EfDeleteOrderLineCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<IUpdateStoreCommand, EfUpdateStoreCommand>();
            services.AddTransient<IUpdateShippingMethodCommand, EfUpdateShippingMethodCommand>();
            services.AddTransient<IUpdateCouponCommand, EfUpdateCouponCommand>();
            services.AddTransient<IUpdateInventoryCommand, EfUpdateInventoryCommand>();
            services.AddTransient<IUpdateProductCommand, EfUpdateProductCommand>();
            services.AddTransient<IUpdateProductImageCommand, EfUpdateProductImageCommand>();
            services.AddTransient<IUpdateProductSpecificationCommand, EfUpdateProductSpecificationCommand>();
            services.AddTransient<IUpdateAddressCommand, EfUpdateAddressCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IUpdateOrderCommand, EfUpdateOrderCommand>();
            services.AddTransient<IUseCaseLogger, EfUsecaseLogger>();
            //Queries
            services.AddTransient<IGetAllProductsQuery, EfGetProductsQuery>();
            services.AddTransient<IGetCouponsQuery, EfGetCouponsQuery>();
            services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
            services.AddTransient<IGetStoresQuery, EfGetStoresQuery>();
            services.AddTransient<IGetShippingMethodsQuery, EfGetShippingMethodsQuery>();
            services.AddTransient<IGetAddressesQuery, EfGetAddressesQuery>();
            services.AddTransient<IGetInventoriesQuery, EfGetInventoriesQuery>();
            services.AddTransient<IGetCartsQuery, EfGetCartsQuery>();
            services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();
            services.AddTransient<IGetStoreQuery, EfGetStoreQuery>();
            services.AddTransient<IGetShippingMethodQuery, EfGetShippingMethodQuery>();
            services.AddTransient<IGetInventoryQuery, EfGetInventoryQuery>();
            services.AddTransient<IGetCouponQuery, EfGetCouponQuery>();
            services.AddTransient<IGetCategoryQuery,  EfGetCategoryQuery>();   
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            services.AddTransient<IGetWishlistitemsQuery, EfGetWishlistitemsQuery>();
            services.AddTransient<IGetAddresQuery, EfGetAddressQuery>();
            services.AddTransient<IGetProductQuery, EfGetProductQuery>();
            services.AddTransient<IGetProductImagesQuery, EfGetProductImagesQuery>();
            services.AddTransient<IGetProductImageQuery, EfGetProductImageQuery>();
            services.AddTransient<IGetProductSpecificationsQuery, EfGetProductSpecificationsQuery>();
            services.AddTransient<IGetProductSpecificationQuery, EfGetProductSpecificationQuery>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetProfileCartQuery, EfGetProfileCartQuery>();
            services.AddTransient<IGetProfileOrdersQuery, EfGetProfileOrdersQuery>();
            services.AddTransient<IGetProfileWishlistitemsQuery, EfGetProfileWishlistitemsQuery>();
            //Kreirati komande za --- Addresses, Coupons, Shipping Methods, Products
            //Validators
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<CreateStoreValidator>();
            services.AddTransient<CreateAddressValidator>();
            services.AddTransient<CreateShippingMethodValidator>();
            services.AddTransient<CreateCouponValidator>();
            services.AddTransient<CreateProductValidator>();
            services.AddTransient<CreateProductImageValidator>();
            services.AddTransient<CreateProductSpecificationValidator>();
            services.AddTransient<UpdateCategoryValidator>();
            services.AddTransient<UpdateStoreValidator>();
            services.AddTransient<UpdateShippingMethodValidator>();
            services.AddTransient<UpdateCouponValidator>();
            services.AddTransient<UpdateInventoryValidator>();
            services.AddTransient<UpdateProductValidator>();
            services.AddTransient<UpdateProductImageValidator>();
            services.AddTransient<UpdateProductSpecificationValidator>();
            services.AddTransient<UpdateAddressValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<UpdateOrderStatusValidator>();
            services.AddTransient<CreateInventoryValidator>();
            services.AddTransient<CreateWishlistItemValidator>();
            services.AddTransient<CreateCartValidator>();
            services.AddTransient<CreateCartItemValidator>();
            services.AddTransient<CreateOrderValidator>();
            services.AddTransient<CreateOrderLineValidator>();
            services.AddTransient<OrderSearchValidator>();
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
