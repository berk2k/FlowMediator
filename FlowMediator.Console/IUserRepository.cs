namespace FlowMediator.Console
{
    public interface IUserRepository
    {
        User GetById(int id);
        Task<User> GetByIdAsync(int id);
    }

    public class UserRepository : IUserRepository
    {
        public User GetById(int id) => new User { Id = id, Name = $"User {id} from Repo" };

        public Task<User> GetByIdAsync(int id) =>
            Task.FromResult(new User { Id = id, Name = $"Async User {id} from Repo" });
    }
}
