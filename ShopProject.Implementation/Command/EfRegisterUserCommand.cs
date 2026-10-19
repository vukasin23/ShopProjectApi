using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        public int Id => 2;

        public string Name => "Register user using EF";
        private readonly ShopProjectContext _context;
        private readonly RegisterUserValidator _validator;

        public EfRegisterUserCommand(ShopProjectContext context, RegisterUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }



        public void Execute(RegisterUserDto request)
        {
            //validator
            _validator.ValidateAndThrow(request);
            //execute registration logic

            var user = new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            

        }
    }
}
