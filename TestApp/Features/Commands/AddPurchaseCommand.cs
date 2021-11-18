using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.Model;

namespace TestApp.Features.Commands
{
    public class AddPurchaseCommand :Purchase, IRequest<int>
    {
       public class AddPurchaseCommandHandler : IRequestHandler<AddPurchaseCommand, int>
        {
            private readonly IAppDbContext _context;

            public AddPurchaseCommandHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(AddPurchaseCommand command, CancellationToken cancellationToken)
            {
                var purchase = new Purchase
                {
                    Id = command.Id,
                    PurchaseCode = command.PurchaseCode,
                    SupplierName = command.SupplierName,
                    TotalAmount = command.TotalAmount,
                    PurchaseDate = DateTime.Today.Date,
                    PurchaseDetails = command.PurchaseDetails
                };
                _context.Purchases.Add(purchase);
                await _context.SaveChanges();
                return purchase.Id;
            }
        }
    }
}