using KZ.API.Authentication;
using KZ.API.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenHelper _tokenHelper;

        public AuthenticationService(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }
        public string GetToken(LoginDataDto loginData)
        {
            return _tokenHelper.CreateToken(loginData);
        }

        public bool IsAuthenticated(LoginDataDto loginData)
        {
            //测试，之后修改
            return true;
        }
    }
}
