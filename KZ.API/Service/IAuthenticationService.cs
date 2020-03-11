using KZ.API.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Service
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated(LoginDataDto loginData);
        string GetToken(LoginDataDto loginData);
    }
}
