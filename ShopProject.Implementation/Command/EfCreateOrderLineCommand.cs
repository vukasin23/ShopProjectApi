using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfCreateOrderLineCommand:ICreateOrderLineCommand
{
    private readonly ShopProjectContext _context;
    private readonly CreateOrderLineValidator _validator;

    public EfCreateOrderLineCommand(ShopProjectContext context, CreateOrderLineValidator validator)
    {
        _context = context;
        _validator = validator;
    }
    public int Id => 16;
    public string Name => "Create order line";

    public void Execute(OrderLineDto request)
    {
        _validator.ValidateAndThrow(request);
        var product = _context.Products.Find(request.ProductId);
        var orderLine = new Domain.OrderLine
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            //Its not going to be null because validator checks if product exists
            UnitPrice = product.Price,
            OrderId = request.OrderId
            
        };
        
        _context.OrderLines.Add(orderLine);
        _context.SaveChanges();
    }
    
}