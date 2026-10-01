using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Domain;
using ShopProject.Implementation;
using ShopProject.Implementation.Command;
using System;
using System.Linq;
using Xunit;

namespace ShopProject.Tests.Commands
{
    // Every test runs inside a transaction that is rolled back on dispose, so the dev database is left untouched.
    public class DeleteCommandsTests : IDisposable
    {
        private readonly ShopProjectContext _context;
        private readonly Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction _transaction;

        public DeleteCommandsTests()
        {
            _context = new ShopProjectContext();
            _transaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _context.Dispose();
        }

        private User CreateUser()
        {
            var unique = Guid.NewGuid().ToString("N");
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Username = unique,
                Email = unique + "@test.com",
                PasswordHash = "x",
                PhoneNumber = "000"
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        private Product CreateProduct()
        {
            var category = new Category { Name = "Cat " + Guid.NewGuid().ToString("N"), Description = "d" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var product = new Product
            {
                Name = "Prod " + Guid.NewGuid().ToString("N"),
                Description = "description",
                Price = 10,
                CategoryId = category.Id
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        private Address CreateAddress(User user)
        {
            var address = new Address
            {
                UserId = user.Id,
                Street = "s",
                City = "c",
                State = "st",
                ZipCode = "1",
                Country = "co",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return address;
        }

        private ShippingMethod CreateShippingMethod()
        {
            var method = new ShippingMethod { Name = "m", Description = "d", Price = 1 };
            _context.ShippingMethods.Add(method);
            _context.SaveChanges();
            return method;
        }

        private Order CreateOrder(User user, Address address, ShippingMethod method, Coupon? coupon = null)
        {
            var order = new Order
            {
                UserId = user.Id,
                AddressId = address.Id,
                ShippingMethodId = method.Id,
                CouponId = coupon?.Id,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        // ---------- Product (soft delete) ----------

        [Fact]
        public void DeleteProduct_HidesProductButKeepsRow()
        {
            var product = CreateProduct();

            new EfDeleteProductCommand(_context).Execute(product.Id);

            Assert.False(_context.Products.Any(p => p.Id == product.Id));
            Assert.True(_context.Products.IgnoreQueryFilters().Single(p => p.Id == product.Id).IsDeleted);
        }

        [Fact]
        public void DeleteProduct_RemovesItFromCartsAndWishlists()
        {
            var user = CreateUser();
            var product = CreateProduct();
            var cart = new Cart { UserId = user.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            _context.CartItems.Add(new CartItem { CartId = cart.Id, ProductId = product.Id, Quantity = 1, AddedAt = DateTime.Now });
            _context.WishlistItems.Add(new WishlistItem { UserId = user.Id, ProductId = product.Id, AddedAt = DateTime.Now });
            _context.SaveChanges();

            new EfDeleteProductCommand(_context).Execute(product.Id);

            Assert.False(_context.CartItems.Any(ci => ci.ProductId == product.Id));
            Assert.False(_context.WishlistItems.Any(w => w.ProductId == product.Id));
        }

        [Fact]
        public void DeleteProduct_WhenAlreadyDeletedOrMissing_ThrowsNotFound()
        {
            var product = CreateProduct();
            var command = new EfDeleteProductCommand(_context);
            command.Execute(product.Id);

            Assert.Throws<EntityNotFoundException>(() => command.Execute(product.Id));
            Assert.Throws<EntityNotFoundException>(() => command.Execute(int.MaxValue));
        }

        // ---------- Category ----------

        [Fact]
        public void DeleteCategory_WithProducts_ThrowsConflict()
        {
            var product = CreateProduct();

            Assert.Throws<ConflictException>(() => new EfDeleteCategoryCommand(_context).Execute(product.CategoryId));
        }

        [Fact]
        public void DeleteCategory_WithOnlySoftDeletedProducts_ThrowsConflict()
        {
            var product = CreateProduct();
            new EfDeleteProductCommand(_context).Execute(product.Id);

            Assert.Throws<ConflictException>(() => new EfDeleteCategoryCommand(_context).Execute(product.CategoryId));
        }

        [Fact]
        public void DeleteCategory_WhenEmpty_RemovesIt()
        {
            var category = new Category { Name = "Empty " + Guid.NewGuid().ToString("N"), Description = "d" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            new EfDeleteCategoryCommand(_context).Execute(category.Id);

            Assert.False(_context.Categories.Any(c => c.Id == category.Id));
        }

        // ---------- Cart / wishlist ownership ----------

        [Fact]
        public void DeleteCartItem_OfAnotherUser_ThrowsNotFound()
        {
            var owner = CreateUser();
            var other = CreateUser();
            var product = CreateProduct();
            var cart = new Cart { UserId = owner.Id, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            var item = new CartItem { CartId = cart.Id, ProductId = product.Id, Quantity = 1, AddedAt = DateTime.Now };
            _context.CartItems.Add(item);
            _context.SaveChanges();

            Assert.Throws<EntityNotFoundException>(() =>
                new EfDeleteCartItemCommand(_context, new Actor { Id = other.Id }).Execute(item.Id));

            new EfDeleteCartItemCommand(_context, new Actor { Id = owner.Id }).Execute(item.Id);
            Assert.False(_context.CartItems.Any(ci => ci.Id == item.Id));
        }

        [Fact]
        public void DeleteWishlistItem_OfAnotherUser_ThrowsNotFound()
        {
            var owner = CreateUser();
            var other = CreateUser();
            var product = CreateProduct();
            var item = new WishlistItem { UserId = owner.Id, ProductId = product.Id, AddedAt = DateTime.Now };
            _context.WishlistItems.Add(item);
            _context.SaveChanges();

            Assert.Throws<EntityNotFoundException>(() =>
                new EfDeleteWishlistItemCommand(_context, new Actor { Id = other.Id }).Execute(item.Id));

            new EfDeleteWishlistItemCommand(_context, new Actor { Id = owner.Id }).Execute(item.Id);
            Assert.False(_context.WishlistItems.Any(w => w.Id == item.Id));
        }

        // ---------- Address (soft delete + ownership) ----------

        [Fact]
        public void DeleteAddress_SoftDeletesOnlyForOwner()
        {
            var owner = CreateUser();
            var other = CreateUser();
            var address = CreateAddress(owner);

            Assert.Throws<EntityNotFoundException>(() =>
                new EfDeleteAddressCommand(_context, new Actor { Id = other.Id }).Execute(address.Id));

            new EfDeleteAddressCommand(_context, new Actor { Id = owner.Id }).Execute(address.Id);

            Assert.False(_context.Addresses.Any(a => a.Id == address.Id));
            Assert.True(_context.Addresses.IgnoreQueryFilters().Single(a => a.Id == address.Id).IsDeleted);
        }

        [Fact]
        public void DeleteAddress_UsedByOrder_KeepsOrderIntact()
        {
            var user = CreateUser();
            var address = CreateAddress(user);
            var order = CreateOrder(user, address, CreateShippingMethod());

            new EfDeleteAddressCommand(_context, new Actor { Id = user.Id }).Execute(address.Id);

            Assert.True(_context.Orders.Any(o => o.Id == order.Id));
        }

        // ---------- Shipping method / coupon / store conflicts ----------

        [Fact]
        public void DeleteShippingMethod_UsedByOrder_ThrowsConflict()
        {
            var user = CreateUser();
            var method = CreateShippingMethod();
            CreateOrder(user, CreateAddress(user), method);

            Assert.Throws<ConflictException>(() => new EfDeleteShippingMethodCommand(_context).Execute(method.Id));
        }

        [Fact]
        public void DeleteShippingMethod_Unused_RemovesIt()
        {
            var method = CreateShippingMethod();

            new EfDeleteShippingMethodCommand(_context).Execute(method.Id);

            Assert.False(_context.ShippingMethods.Any(m => m.Id == method.Id));
        }

        [Fact]
        public void DeleteCoupon_UsedByOrder_ThrowsConflict()
        {
            var user = CreateUser();
            var coupon = new Coupon { Code = "T" + Guid.NewGuid().ToString("N")[..8], DiscountAmount = 5, ExpiryDate = DateTime.Now.AddDays(1) };
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
            CreateOrder(user, CreateAddress(user), CreateShippingMethod(), coupon);

            Assert.Throws<ConflictException>(() => new EfDeleteCouponCommand(_context).Execute(coupon.Id));
        }

        [Fact]
        public void DeleteStore_WithStock_ThrowsConflict_ButEmptyStoreIsRemoved()
        {
            var product = CreateProduct();
            var store = new Store { Name = "s", City = "c", Address = "a", Phone = "1", IsActive = true };
            _context.Stores.Add(store);
            _context.SaveChanges();
            var inventory = new Inventory { StoreId = store.Id, ProductId = product.Id, Quantity = 5, LastUpdated = DateTime.Now };
            _context.Inventories.Add(inventory);
            _context.SaveChanges();

            var command = new EfDeleteStoreCommand(_context);
            Assert.Throws<ConflictException>(() => command.Execute(store.Id));

            inventory.Quantity = 0;
            _context.SaveChanges();
            command.Execute(store.Id);

            Assert.False(_context.Stores.Any(s => s.Id == store.Id));
        }

        // ---------- Simple hard deletes ----------

        [Fact]
        public void DeleteInventory_ImageAndSpecification_RemoveTheRow()
        {
            var product = CreateProduct();
            var store = new Store { Name = "s", City = "c", Address = "a", Phone = "1", IsActive = true };
            _context.Stores.Add(store);
            _context.SaveChanges();
            var inventory = new Inventory { StoreId = store.Id, ProductId = product.Id, Quantity = 1, LastUpdated = DateTime.Now };
            var image = new ProductImage { ProductId = product.Id, ImageUrl = "u", AltText = "a", IsPrimary = true };
            var spec = new ProductSpecification { ProductId = product.Id, SpecificationName = "n", SpecificationValue = "v" };
            _context.Inventories.Add(inventory);
            _context.ProductImages.Add(image);
            _context.ProductSpecifications.Add(spec);
            _context.SaveChanges();

            new EfDeleteInventoryCommand(_context).Execute(inventory.Id);
            new EfDeleteProductImageCommand(_context).Execute(image.Id);
            new EfDeleteProductSpecificationCommand(_context).Execute(spec.Id);

            Assert.False(_context.Inventories.Any(i => i.Id == inventory.Id));
            Assert.False(_context.ProductImages.Any(i => i.Id == image.Id));
            Assert.False(_context.ProductSpecifications.Any(s => s.Id == spec.Id));
        }

        [Fact]
        public void SimpleDeletes_WhenMissing_ThrowNotFound()
        {
            Assert.Throws<EntityNotFoundException>(() => new EfDeleteInventoryCommand(_context).Execute(int.MaxValue));
            Assert.Throws<EntityNotFoundException>(() => new EfDeleteProductImageCommand(_context).Execute(int.MaxValue));
            Assert.Throws<EntityNotFoundException>(() => new EfDeleteProductSpecificationCommand(_context).Execute(int.MaxValue));
            Assert.Throws<EntityNotFoundException>(() => new EfDeleteStoreCommand(_context).Execute(int.MaxValue));
            Assert.Throws<EntityNotFoundException>(() => new EfDeleteCouponCommand(_context).Execute(int.MaxValue));
        }
    }
}
