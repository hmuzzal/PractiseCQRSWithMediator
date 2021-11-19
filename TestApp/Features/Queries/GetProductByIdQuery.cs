using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.Model;

namespace TestApp.Features.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IAppDbContext _context;
            public GetProductByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken00)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == query.Id);
                return Task.FromResult(product ?? null);
            }
        }
    }
}
