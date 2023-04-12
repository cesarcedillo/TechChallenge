using Backend.TechChallenge.Application.Contracts.Persistence;
using Backend.TechChallenge.Domain;
using Backend.TechChallenge.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Infrastructure.Repositories
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserDbContext UserDbContext => _context;
        public UserRepository(UserDbContext context) : base(context)
        {
        }

        public override async Task<User> AddAsync(User entity)
        {
            var users = await GetAsync(user=>(user.Name==entity.Name && user.Address == entity.Address) ||
                                                user.Email == entity.Email ||
                                                user.Phone == entity.Phone);
            if (users.Any())
            {
                throw new Exception("The user is duplicated");
            }

            return await base.AddAsync(entity);
        }
    }
}
