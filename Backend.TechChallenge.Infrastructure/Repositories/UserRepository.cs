using Backend.TechChallenge.Application.Contracts.Persistence;
using Backend.TechChallenge.Domain;
using Backend.TechChallenge.Infrastructure.Persistence;

namespace Backend.TechChallenge.Infrastructure.Repositories
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {

        }
    }
}
