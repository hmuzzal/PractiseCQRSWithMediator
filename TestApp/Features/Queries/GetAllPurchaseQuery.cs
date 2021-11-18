using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.Model;

namespace TestApp.Features.Queries
{
    public class GetAllPurchaseQuery : IRequest<IEnumerable<Purchase>>
    {
        public class GetAllPurchaseQueryHandler : IRequestHandler<GetAllPurchaseQuery, IEnumerable<Purchase>>
        {
            private readonly IAppDbContext _context;

            public GetAllPurchaseQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Purchase>> Handle(GetAllPurchaseQuery query, CancellationToken cancellationToken)
            {
                var purchases = await _context.Purchases
                    .Include(p=>p.PurchaseDetails)
                    .ThenInclude(pd=>pd.Product)
                    .ToListAsync();

                return purchases?.AsReadOnly();
                //return JsonSerializer.Serialize<IEnumerable<Purchase>> (purchase);
            }
        }
    }
}
    