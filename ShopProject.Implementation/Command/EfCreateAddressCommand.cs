using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command
{
    public class EfCreateAddressCommand:ICreateAddressCommand
    {
        private readonly ShopProjectContext _context;
        private readonly CreateAddressValidator _validator;

        public EfCreateAddressCommand(ShopProjectContext context, CreateAddressValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 5;
        public string Name => "Create Address Command";

        public void Execute(AddressDto request)
        {
            _validator.ValidateAndThrow(request);

            var address = new Domain.Address
            {
                UserId = request.UserId,
                Street = request.Street,
                City = request.City,
                ZipCode = request.ZipCode,
                State = request.State,
                Country = request.Country,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            };  
            _context.Addresses.Add(address);
            _context.SaveChanges(); 
        }
    }
}
