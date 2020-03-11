using AutoMapper;
using KZ.API.Models.DtoModels;
using KZ.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<KzUser, UserDto>()
                .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom((user, str) =>
                {
                    string[] s = user.Email.Split('@');
                    if (s[0].Length > 3)
                    {
                        return $"{s[0].Substring(0, 3)}***@{s[1]}";
                    }
                    else
                    {
                        return $"{s[0]}***@{s[1]}";
                    }
                }));
        }
    }
}
