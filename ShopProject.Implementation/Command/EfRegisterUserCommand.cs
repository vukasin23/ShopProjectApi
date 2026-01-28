using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShopProject.Implementation.Command
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        private readonly ShopProjectContext _context;

        public EfRegisterUserCommand(ShopProjectContext context)
        {
            _context = context;
        }

        public int Id => 2;

        public string Name => "Register user";

        public void Execute(RegisterUserDto request)
        {
            //validator

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
