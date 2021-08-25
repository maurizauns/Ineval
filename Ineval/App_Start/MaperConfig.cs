using AutoMapper;
using Ineval.DAL;
using Ineval.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ineval.App_Start
{
    public class MaperConfig : Profile
    {
        public MaperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Configuracion, SettingViewModel>().ReverseMap();
            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<Province, ProvinceViewModel>().ReverseMap();
            CreateMap<Canton, CantonViewModel>().ReverseMap();
            CreateMap<Test, TestViewModel>().ReverseMap();
        }
    }
}