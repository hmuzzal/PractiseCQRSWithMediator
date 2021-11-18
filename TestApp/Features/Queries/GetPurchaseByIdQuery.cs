using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.Model;

namespace TestApp.Features.Queries
{
    public class GetPurchaseByIdQuery : IRequest<Purchase>
    {
        public int Id { get; set; }

        public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, Purchase>
        {
            private readonly IAppDbContext _context;
            public GetPurchaseByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public Task<Purchase> Handle(GetPurchaseByIdQuery query, CancellationToken cancellationToken00)
            {
                var purchase = _context.Purchases
                    .Include(p => p.PurchaseDetails)
                    .ThenInclude(p => p.Product).FirstOrDefault(p => p.Id == query.Id);
                return Task.FromResult(purchase);
            }
        }
    }
}

