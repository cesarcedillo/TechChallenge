using AutoFixture;
using Backend.TechChallenge.Domain;
using Backend.TechChallenge.Infrastructure.Persistence;
using Backend.TechChallenge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.TechChallenge.Test
{
    public static class MockUserRepository
    {
        public static void AddDataUserRepository(UserDbContext userDbContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var users = fixture.CreateMany<User>().ToList();
            users.Add(fixture.Build<User>()
                .With(user => user.Name, "Cesar")
                .With(user => user.Email, "Cesar@gmail.com")
                .Create());

            userDbContextFake.Users!.AddRange(users);
            userDbContextFake.SaveChanges();
        }

        public static Mock<UserRepository> GetUserRepository()
        {
            var dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{dbContextId}")
                .Options;

            var userDbContextFake = new UserDbContext(options);
            userDbContextFake.Database.EnsureDeleted();
            var mockUserRepository = new Mock<UserRepository>(userDbContextFake);

            return mockUserRepository;
        }
    }
}
