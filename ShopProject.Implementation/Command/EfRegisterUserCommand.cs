using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Command
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        public int Id => 2;

        public string Name => "Register user";

        public void Execute(RegisterUserDto request)
        {
            //execute registration logic
        }
    }
}
