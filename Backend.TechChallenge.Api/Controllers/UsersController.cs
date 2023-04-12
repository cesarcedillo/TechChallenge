using AutoMapper;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Models;
using Backend.TechChallenge.Application.Validators;
using Backend.TechChallenge.Domain;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        private IMapper _mapper;

        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(DtoInputUser inputUser)
        {
            DtoInputUserValidator userValidator = new DtoInputUserValidator();
            ValidationResult userValidationResult = userValidator.Validate(inputUser);

            if (!userValidationResult.IsValid)
                return new Result()
                {
                    IsSuccess = false,
                    Errors = string.Join(" ", userValidationResult.Errors)
                };

            var newUser = _mapper.Map<User>(inputUser);

            var reader = ReadUsersFromFile();
            
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = EnumHelper<UserTypes>.Parse(line.Split(',')[4].ToString()),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();


            foreach (var user in _users)
            {
                if (user.Email == newUser.Email || 
                    user.Phone == newUser.Phone ||
                    (user.Name == newUser.Name && user.Address == newUser.Address)
                    )
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }

            WriteUserToFile(newUser);

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }
    }
}
