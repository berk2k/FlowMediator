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

        public User Handle(GetUserByIdQuery request)
        {
            return _repository.GetById(request.Id);
        }
    }
}
