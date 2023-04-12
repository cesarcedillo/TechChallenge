using Backend.TechChallenge.Domain;

namespace Backend.TechChallenge.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
