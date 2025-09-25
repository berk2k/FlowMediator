using FlowMediator.Console.Entities;
using FlowMediator.Contracts;

namespace FlowMediator.Console.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; }
        public GetUserByIdQuery(int id) => Id = id;
    }
}
