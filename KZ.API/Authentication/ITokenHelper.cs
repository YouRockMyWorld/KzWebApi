using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using KZ.API.Models.DtoModels;

namespace KZ.API.Authentication
{
    public interface ITokenHelper
    {
        string CreateToken(LoginDataDto loginData);
    }
}
