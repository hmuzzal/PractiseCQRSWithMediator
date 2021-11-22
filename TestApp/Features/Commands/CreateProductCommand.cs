using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.Model;

namespace TestApp.Features.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Rate { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IAppDbContext _context;
            private ProductFactory _productFactory;

            public CreateProductCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                _productFactory = new ConcreteProductFactory();
                var product = _productFactory.Create();

                product.Barcode = command.Barcode;
                product.Name = command.Name;
                product.BuyingPrice = command.BuyingPrice;
                product.Rate = command.Rate;
                product.Description = command.Description;
                //var product = new Product
                //{
                //    Barcode = command.Barcode,
                //    Name = command.Name,
                //    BuyingPrice = command.BuyingPrice,
                //    Rate = command.Rate,
                //    Description = command.Description
                //};
                _context.Products.Add(product);
                await _context.SaveChanges();
                return product.Id;
            }
        }
    }
}
