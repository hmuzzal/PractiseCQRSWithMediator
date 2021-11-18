using TestApp.Context;
using TestApp.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken00)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == query.Id);
                return product ?? null;
            }
        }
    }
}
