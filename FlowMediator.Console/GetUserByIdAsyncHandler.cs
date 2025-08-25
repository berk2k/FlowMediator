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
        private readonly IUserRepository _repository;

        public GetUserByIdAsyncHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
