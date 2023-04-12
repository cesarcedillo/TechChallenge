using AutoMapper;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Domain;
using System;

namespace Backend.TechChallenge.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DtoInputUser, User>()
                .ForMember(user => user.Money, 
                            expression => expression.MapFrom(dtoInputUser => decimal.Parse(dtoInputUser.Money)))
                .ForMember(user => user.UserType, 
                            expression => expression.MapFrom(dtoInputUser => EnumHelper<UserTypes>.Parse(dtoInputUser.UserType)))
                .ForMember(user => user.Money, 
                            expression => expression.MapFrom(dtoInputUser => MapMoney(decimal.Parse(dtoInputUser.Money), 
                                                                                        EnumHelper<UserTypes>.Parse(dtoInputUser.UserType))))
                .ForMember(user => user.Email,
                            expression => expression.MapFrom(dtoInputUser => NormalizeEmail(dtoInputUser.Email)));
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        private decimal MapMoney(decimal money, UserTypes userType)
        {

            switch (userType)
            {
                case UserTypes.Normal:
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = money * percentage;
                        return money + gif;
                    }
                    if (money < 100)
                    {
                        if (money > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = money * percentage;
                            return money + gif;
                        }
                    }
                    break;
                case UserTypes.SuperUser:
                    if (money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = money * percentage;
                        return money + gif;
                    }
                    break;
                case UserTypes.Premium:
                    if (money > 100)
                    {
                        var gif = money * 2;
                        return money + gif;
                    }
                    break;
            }

            return money;
        }

    }
}
