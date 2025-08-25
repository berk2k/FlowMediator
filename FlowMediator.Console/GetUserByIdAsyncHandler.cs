using FlowMediator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMediator.Console
{
    public class GetUserByIdAsyncHandler : IRequestHandlerAsync<GetUserByIdQuery, User>
    {
        public async Task<User> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken); // simulate I/O
            return new User
            {
                Id = request.Id,
                Name = $"Async User {request.Id}"
            };
        }
    }
}
