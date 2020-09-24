using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using DTO;
using MobilePortalAPI.ViewModel;

namespace MobilePortalAPI.Common
{
    public class MobilePortalProfile : Profile
    {
        public MobilePortalProfile()
        {
            CreateMap<DealerApplicationConfiguration, DealerApplicationConfigurationDTO>()
                .ForMember(dest => dest.IsAllowAccess, o => o.MapFrom(x => x.AllowAccess == "1" ? true : false))
                .ForMember(dest => dest.OriginalApplication, o => o.MapFrom(x => x.Application))
                .ForMember(dest => dest.Application, o => o.MapFrom(x => x.Application.ToLower()))
                .ForMember(dest => dest.DealerId, o => o.MapFrom(x => x.DealerId.ToUpper()));
            CreateMap<DealerApplicationConfigurationViewModel, DealerApplicationConfigurationDTO>()
                .ForMember(dest => dest.AllowAccess, o => o.MapFrom(x => x.IsAllowAccess ? "1" : "0"));
            CreateMap<DealerApplicationConfigurationDTO, DealerApplicationConfiguration>()
                .ForMember(dest => dest.CreationEmpId, o => o.Ignore())
                .ForMember(dest => dest.CreationTimestamp, o => o.Ignore())
                .ForMember(dest => dest.DealerApplicationConfigurationKey, o => o.Ignore());
            CreateMap<SearchEngineViewModel, SearchEngineDTO>();
        }
    }
}
