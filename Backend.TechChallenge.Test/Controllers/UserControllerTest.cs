using AutoMapper;
using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Application.Contracts.Persistence;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Mappings;
using Backend.TechChallenge.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Backend.TechChallenge.Test.Controllers
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UserRepository> _userRepository;

        public UserControllerTest()
        {
            _userRepository = MockUserRepository.GetUserRepository();
            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            MockUserRepository.AddDataUserRepository(_userRepository.Object.UserDbContext);
        }

        [Fact]
        public void UserControllerTest_Create_Success()
        {
            var userController = new UsersController(_mapper, _userRepository.Object);

            var inputUser = new DtoInputUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(inputUser).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void UserControllerTest_Create_Fail_Duplicated()
        {
            var userController = new UsersController(_mapper, _userRepository.Object);

            var inputUser = new DtoInputUser("Cesar", "Cesar@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(inputUser).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
