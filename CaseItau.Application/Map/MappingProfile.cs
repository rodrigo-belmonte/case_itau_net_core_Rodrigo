using AutoMapper;
using CaseItau.Domain.Models;
using CaseItau.Domain.Reponses.Fundo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Application.Map
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<GetFundoTipoFundoModel, FundoTipoFundoResponseModel>()
                .ForMember(dest => dest.Patrimonio, opt => opt.MapFrom(src => src.Patrimonio.Replace('.',',')));
            CreateMap<UpdateFundoModel, UpdateFundoRequestModel>().ReverseMap();
        }
    }
}
