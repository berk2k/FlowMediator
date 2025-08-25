namespace FlowMediator.Console
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
    }

    public class UserRepository : IUserRepository
    {
        public Task<User> GetByIdAsync(int id) =>
            Task.FromResult(new User { Id = id, Name = $"Async User {id} from Repo" });
    }
}
