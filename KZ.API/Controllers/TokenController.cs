using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KZ.API.Models.DtoModels;
using KZ.API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController:ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetToken([FromBody]LoginDataDto loginData)
        {
            if (!_authenticationService.IsAuthenticated(loginData))
            {
                return BadRequest();
            }
            return Ok(_authenticationService.GetToken(loginData));
        }
    }
}
