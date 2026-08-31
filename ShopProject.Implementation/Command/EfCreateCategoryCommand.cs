using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Domain;
using ShopProject.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Implementation.Command
{
    public class EfCreateCategoryCommand:ICreateCategoryCommand
    {
        public int Id => 3;
        public string Name => "Create category using EF";
        private readonly ShopProjectContext _context;
        private readonly CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(ShopProjectContext context, CreateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CategoryDto category)
        {
            _validator.ValidateAndThrow(category);   
            var categoryEntity = new Category
            {
                Name = category.Name,
                Description = category.Description
            };

            _context.Categories.Add(categoryEntity);
            _context.SaveChanges();
        }
    }
}
