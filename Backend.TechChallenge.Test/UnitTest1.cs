using System;
using System.Dynamic;
using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace Backend.TechChallenge.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userController = new UsersController();

            var inputUser = new DtoInputUser(null, "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(inputUser).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();

            var inputUser = new DtoInputUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124");

            var result = userController.CreateUser(inputUser).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
