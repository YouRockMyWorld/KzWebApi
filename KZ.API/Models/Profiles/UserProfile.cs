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
                dest => dest.Phone,
                opt => opt.MapFrom((user, str) =>
                {
                    string p = user.Phone.Trim();
                    if (p.Length == 11)
                    {
                        return p.Substring(0, 3) + "****" + p.Substring(7);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }));
        }
    }
}
