using System;
using System.Dynamic;
using AutoMapper;
using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Mappings;
using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace Backend.TechChallenge.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private readonly IMapper _mapper;

        public UnitTest1()
        {
            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void Test1()
        {
            var userController = new UsersController(_mapper);

            var inputUser = new DtoInputUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(inputUser).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController(_mapper);

            var inputUser = new DtoInputUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(inputUser).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
