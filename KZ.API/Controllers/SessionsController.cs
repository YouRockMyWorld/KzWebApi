using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KZ.API.Models.Others;
using KZ.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionsController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SessionResult>>> GetSessions()
        {
            var sessions = await _sessionRepository.GetAllSessionsAsync();
            return Ok(sessions);
        }

        [HttpDelete("{username}")]
        [Authorize]
        public async Task<IActionResult> DeleteSession(string username)
        {
            if(await _sessionRepository.DeleteByUsernameAsync(username))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
