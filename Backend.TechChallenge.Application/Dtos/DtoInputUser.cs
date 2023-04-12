using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.TechChallenge.Application.Dtos
{
    public class DtoInputUser
    {
        public DtoInputUser(string name, string email, string address, string phone, string userType, string money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }
    }
}
