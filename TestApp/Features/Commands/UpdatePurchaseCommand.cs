using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.Features.Queries;
using TestApp.Model;

namespace TestApp.Features.Commands
{
    public class UpdatePurchaseCommand: Purchase, IRequest<int>
    {
        public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, int>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;
        
            public UpdatePurchaseCommandHandler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpdatePurchaseCommand command, CancellationToken cancellationToken)
            {
                var purchase = new Purchase();
                purchase.Id = command.Id;
                purchase.PurchaseCode = command.PurchaseCode;
                purchase.SupplierName = command.SupplierName;
                purchase.TotalAmount = command.TotalAmount;
                purchase.PurchaseDate = DateTime.Today.Date;

                _context.Purchases.Update(purchase);

                var existingPurchase = await _mediator.Send(new GetPurchaseByIdQuery { Id = command.Id});
                //var deletedPurchaseDetails = existingPurchase.PurchaseDetails.Where(ep => !purchase.PurchaseDetails.Any(p => p.Id == ep.Id));
                var deletedPurchaseDetails = existingPurchase.PurchaseDetails.Where(ep => purchase.PurchaseDetails.All(p => p.Id != ep.Id));
                foreach (var deletedPurchaseDetail in deletedPurchaseDetails)
                {
                    deletedPurchaseDetail.Purchase = null;
                    _context.PurchaseDetails.Remove(deletedPurchaseDetail);
                }

                foreach (var purchaseDetail in command.PurchaseDetails)
                {
                    foreach (var existingPurchaseDetail in existingPurchase.PurchaseDetails)
                    {

                        if (purchaseDetail.Id == existingPurchaseDetail.Id)
                            _context.PurchaseDetails.Update(purchaseDetail);
                        else
                            _context.PurchaseDetails.Add(purchaseDetail);
                    }
                }

                await _context.SaveChanges();
                return purchase.Id;
            }
        }
    }
}
