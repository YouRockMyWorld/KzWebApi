using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KZ.API.Helpers;
using KZ.API.Models.DtoModels;
using KZ.API.Models.DtoParameters;
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
    [Route("api/logs")]//[Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public LogController(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository));
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetOperationLogs))]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OperationLogDto>>> GetOperationLogs([FromQuery] OperationLogDtoParameters parameters)
        {
            var logs = await _logRepository.GetOperationLogsAsync(parameters);
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
            if(logs.Count != 0)
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
                    return Url.Link(nameof(GetOperationLogs), new
                    {
                        StartDateTime = parameters.StartDateTime,
                        EndDateTime = parameters.EndDateTime,
                        PageNumber = parameters.PageNumber - 1,
                        PageSize = parameters.PageSize
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetOperationLogs), new
                    {
                        StartDateTime = parameters.StartDateTime,
                        EndDateTime = parameters.EndDateTime,
                        PageNumber = parameters.PageNumber + 1,
                        PageSize = parameters.PageSize
                    });
                default:
                    return Url.Link(nameof(GetOperationLogs), new
                    {
                        StartDateTime = parameters.StartDateTime,
                        EndDateTime = parameters.EndDateTime,
                        PageNumber = parameters.PageNumber,
                        PageSize = parameters.PageSize
                    });
            }
        }
    }
}
