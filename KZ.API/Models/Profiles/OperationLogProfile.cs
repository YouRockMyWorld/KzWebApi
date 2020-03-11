using AutoMapper;
using KZ.API.Helpers;
using KZ.API.Models.DtoModels;
using KZ.API.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.Profiles
{
    public class OperationLogProfile : Profile
    {
        public OperationLogProfile()
        {
            CreateMap<OperationLogResult, OperationLogDto>()
                .ForMember(dest => dest.Module,
                opt => opt.MapFrom((log, dto) => _moduleDict[log.Module]))
                .ForMember(dest => dest.LogType,
                opt => opt.MapFrom((log, dto) => _logTypeDict[log.LogType]))
                .ForMember(dest => dest.IPAddress,
                opt => opt.MapFrom((log, dto) => IPAddressHelper.NumberToIpString(log.IPAddress)));
        }

        private Dictionary<int, string> _moduleDict = new Dictionary<int, string>
        {
            //
        };
        private Dictionary<int, string> _logTypeDict = new Dictionary<int, string>
        {
            //
        };
    }
}
