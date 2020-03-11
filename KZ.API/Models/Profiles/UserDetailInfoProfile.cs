using AutoMapper;
using KZ.API.Models.DtoModels;
using KZ.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.Profiles
{
    public class UserDetailInfoProfile:Profile
    {
        public UserDetailInfoProfile()
        {
            CreateMap<KzUser, UserDetailInfoDto>();
            CreateMap<KzUserProfile, UserDetailInfoDto>();
        }
    }
}
