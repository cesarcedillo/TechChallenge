using AutoMapper;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Mappings;
using Backend.TechChallenge.Domain;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Xunit;

namespace Backend.TechChallenge.Test.Mapper
{
    public class MapDtoInputUserToUserTest
    {
        private readonly IMapper _mapper;
        private readonly DtoInputUser _inputUser;

        public MapDtoInputUserToUserTest()
        {
            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            string name = "Mike";
            string email = "mike@gmail.com";
            string address = "Av. Juan G";
            string phone = "+349 1122354215";
            string userType = "Normal";
            string money = "124";

            _inputUser = new DtoInputUser(name, email, address, phone, userType, money);
        }

        [Fact]
        public void MapDtoInputUser_To_User_SimpleFields_Success()
        {
            var user = _mapper.Map<User>(_inputUser);

            Assert.Equal(_inputUser.Name, user.Name);
            Assert.Equal(_inputUser.Address, user.Address);
            Assert.Equal(_inputUser.Phone, user.Phone);
        }

        [Fact]
        public void MapDtoInputUser_To_User_Email_Success()
        {
            string email = "mikewilliamns@gmail.com";
            _inputUser.Email = "mike.williamns@gmail.com";

            var user = _mapper.Map<User>(_inputUser);

            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void MapDtoInputUser_To_User_Money_Normal_Success()
        {
            var money = 138.88m;

            var user = _mapper.Map<User>(_inputUser);

            Assert.Equal(money, user.Money);
        }

        [Fact]
        public void MapDtoInputUser_To_User_Money_SuperUser_Success()
        {
            var money = 148.8m;
            _inputUser.UserType = "SuperUser";

            var user = _mapper.Map<User>(_inputUser);

            Assert.Equal(money, user.Money);
        }



        [Fact]
        public void MapDtoInputUser_To_User_Money_Premium_Success()
        {
            var money = 372m;
            _inputUser.UserType = "Premium";

            var user = _mapper.Map<User>(_inputUser);

            Assert.Equal(money, user.Money);
        }
    }
}
