using FlowMediator.Contracts;

namespace FlowMediator.Console
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        public User Handle(GetUserByIdQuery request)
        {

            return new User
            {
                Id = request.Id,
                Name = $"User {request.Id}"
            };
        }
    }
}
