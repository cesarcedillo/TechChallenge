using AutoMapper;
using Backend.TechChallenge.Application.Contracts.Persistence;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Models;
using Backend.TechChallenge.Application.Validators;
using Backend.TechChallenge.Domain;
using Backend.TechChallenge.Infrastructure.Repositories;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public partial class UsersController : ControllerBase
    {

        private IMapper _mapper;
        private IUserRepository _userRepository;

        public UsersController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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

            try
            {
                await _userRepository.AddAsync(newUser);
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                if (ex.InnerException != null)
                {
                    message = ex.InnerException.Message;
                }
                return new Result()
                {
                    IsSuccess = false,
                    Errors = message
                };
            }

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }
    }
}
