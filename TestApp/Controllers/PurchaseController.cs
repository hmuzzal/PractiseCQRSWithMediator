using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TestApp.Features.Commands;
using TestApp.Features.Queries;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
            
        [HttpPost("Add")]
        public async Task<IActionResult> Create(AddPurchaseCommand command)
        {
            return Ok(await _mediator.Send(command));
        } 
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdatePurchaseCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetAllPurchase")]
        public async Task<IActionResult> GetAllPurchase()
        {
            return Ok(await _mediator.Send(new GetAllPurchaseQuery()));
        }
    }
}
