﻿using AutoMapper;
using Ineval.DAL;
using Ineval.Dto;
using Ineval.Dto.Dto.Procesos;

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
            CreateMap<Parroquia, ParroquiaViewModel>().ReverseMap();

            /*CABECERA EXCEL*/

            CreateMap<DatosExcelCabecera, DatosExcelCabeceraViewModel>().ReverseMap();
            CreateMap<DatosExcelPersonal, DatosExcelPersonalViewModel>().ReverseMap();

            /*FIN CABECERA EXCEL*/

            CreateMap<DatosSustentantes, DatosSustentantesViewModel>().ReverseMap();
            CreateMap<NombreProceso, NombreProcesoViewModel>().ReverseMap();
            CreateMap<DatosTemporales, DatosTemporalesViewModel>().ReverseMap();
            CreateMap<Asignacion, AsignacionViewModel>().ReverseMap();
            CreateMap<DatosInstituciones, DatosInstitucionesViewModel>().ReverseMap();
            CreateMap<ParametrosIniciales, ParametrosInicialesViewModel>().ReverseMap();
            CreateMap<DatosPersonalTerritorio, DatosPersonalTerritorioViewModel>().ReverseMap();

        }
    }
}