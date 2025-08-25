using FlowMediator.Contracts;

namespace FlowMediator.Console
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; }
        public GetUserByIdQuery(int id) => Id = id;
    }
}
