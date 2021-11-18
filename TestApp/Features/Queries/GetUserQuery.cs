using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Context;
using TestApp.DTOs;
using TestApp.Model;
using TestApp.Service;

namespace TestApp.Features.Queries
{
    public class GetUserQuery : UserDto, IRequest<bool>
    {
        public class GGetUserQueryHandler : IRequestHandler<GetUserQuery, bool>
        {
            //private readonly IAppDbContext _context;
            private readonly IUserService _service;
                
            public GGetUserQueryHandler(IUserService service)
            {
                _service = service;
            }

            public Task<bool> Handle(GetUserQuery query, CancellationToken cancellationToken)
            {
                //var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == query.Id);
                var result = _service.IsValidUserInformation(query);
                return Task.FromResult(result);
            }
        }
    }
}
