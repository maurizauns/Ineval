using AutoMapper;
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
            CreateMap<DatosExcelCabecera, DatosExcelCabeceraViewModel>().ReverseMap();
            CreateMap<DatosSustentantes, DatosSustentantesViewModel>().ReverseMap();
            CreateMap<NombreProceso, NombreProcesoViewModel>().ReverseMap();
            CreateMap<DatosTemporales, DatosTemporalesViewModel>().ReverseMap();
            CreateMap<Asignacion, AsignacionViewModel>().ReverseMap();
        }
    }
}