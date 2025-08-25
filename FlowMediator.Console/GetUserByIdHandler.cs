using FlowMediator.Contracts;

namespace FlowMediator.Console
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Simulate async database call
            await Task.Delay(200, cancellationToken);

            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
