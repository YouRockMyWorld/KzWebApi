using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KZ.API.Helpers;
using KZ.API.Models.DtoModels;
using KZ.API.Models.DtoParameters;
using KZ.API.Models.Entities;
using KZ.API.Models.Others;
using KZ.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace KZ.API.Controllers
{
    [ApiController]
    [Route("api/users")]//[Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name =nameof(GetUsers))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery]UserDtoParameters parameters)
        {
            var users = await _userRepository.GetAllUsersAsync(parameters);
            var previousPageLink = users.HasPrevious ? CreateUsersResourceUri(parameters, ResourceUriType.PreviousPage) : null;
            var nextPageLink = users.HasNext ? CreateUsersResourceUri(parameters, ResourceUriType.NextPage) : null;
            var paginationMetaData = new
            {
                CurrentPage = users.CurrentPage,
                PageSize = users.PageSize,
                TotalCount = users.TotalCount,
                TotalPages = users.TotalPages,
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };
            if (users.Count != 0)
            {
                Response.Headers.Add("X-Paginaion", JsonSerializer.Serialize(paginationMetaData, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = null
                }));
            }
            var result = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var user = await _userRepository.QueryByIdAsync(userId);
            var result = _mapper.Map<UserDto>(user);
            return Ok(result);
        }

        [HttpGet("{userId}/detailInfo")]
        [Authorize]
        public async Task<ActionResult<UserDetailInfoDto>> GetUserDetailInfo(int userId)
        {
            var user = await _userRepository.QueryByIdAsync(userId);
            if (user == null)
            {
                return NoContent();
            }
            var detailInfo = await _userRepository.GetUserDetailInfoAsync(userId);
            UserDetailInfoDto output = new UserDetailInfoDto();
            _mapper.Map<KzUser, UserDetailInfoDto>(user, output);
            _mapper.Map<KzUserProfile, UserDetailInfoDto>(detailInfo, output);
            return Ok(output);
        }

        [HttpGet("{userId}/operationLog", Name =nameof(GetUserOperationLogs))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OperationLogDto>>> GetUserOperationLogs(int userId, [FromQuery] OperationLogDtoParameters parameters)
        {
            var logs = await _userRepository.GetUserOperationLogAsync(userId, parameters);
            var previousPageLink = logs.HasPrevious ? CreateOperationLogsResourceUri(parameters, ResourceUriType.PreviousPage) : null;
            var nextPageLink = logs.HasNext ? CreateOperationLogsResourceUri(parameters, ResourceUriType.NextPage) : null;
            var paginationMetaData = new
            {
                CurrentPage = logs.CurrentPage,
                PageSize = logs.PageSize,
                TotalCount = logs.TotalCount,
                TotalPages = logs.TotalPages,
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };
            if (logs.Count != 0)
            {
                Response.Headers.Add("X-Paginaion", JsonSerializer.Serialize(paginationMetaData, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    PropertyNamingPolicy = null
                }));
            }
            var result = _mapper.Map<IEnumerable<OperationLogDto>>(logs);
            return Ok(result);
        }

        private string CreateOperationLogsResourceUri(OperationLogDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetUserOperationLogs), new
                    {
                        StartDateTime = parameters.StartDateTime,
                        EndDateTime = parameters.EndDateTime,
                        PageNumber = parameters.PageNumber - 1,
                        PageSize = parameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetUserOperationLogs), new
                    {
                        StartDateTime = parameters.StartDateTime,
                        EndDateTime = parameters.EndDateTime,
                        PageNumber = parameters.PageNumber + 1,
                        PageSize = parameters.PageSize
                    });
                default:
                    return Url.Link(nameof(GetUserOperationLogs), new
                    {
                        StartDateTime = parameters.StartDateTime,
                        EndDateTime = parameters.EndDateTime,
                        PageNumber = parameters.PageNumber,
                        PageSize = parameters.PageSize
                    });
            }
        }

        private string CreateUsersResourceUri(UserDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetUsers), new
                    {
                        PageNumber = parameters.PageNumber - 1,
                        PageSize = parameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetUsers), new
                    {
                        PageNumber = parameters.PageNumber + 1,
                        PageSize = parameters.PageSize
                    });
                default:
                    return Url.Link(nameof(GetUsers), new
                    {
                        PageNumber = parameters.PageNumber,
                        PageSize = parameters.PageSize
                    });
            }
        }
    }
}
