using UW.AdminPagoExpress.Domain.Entities.Database.Login;
using UW.AdminPagoExpress.Shared.Dtos.User;

namespace UW.AdminPagoExpress.Application.Mapping;

public class UserProfile : IRegister
{
    //public UserProfile()
    //{
    //    CreateMap<UserEntity, UserDto>()
    //        .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.ESTADO))
    //        .ForMember(dest => dest.Mensaje, opt => opt.MapFrom(src => src.MENSAJE))
    //        .ForMember(dest => dest.Alumno, opt => opt.MapFrom(src => src.ALUMNO))
    //        .ForMember(dest => dest.Institucion, opt => opt.MapFrom(src => src.INSTITUCION))
    //        .ForMember(dest => dest.InstitucionDes, opt => opt.MapFrom(src => src.INSTITUCION_DES))
    //        .ForMember(dest => dest.Grado, opt => opt.MapFrom(src => src.GRADO))
    //        .ForMember(dest => dest.GradoDes, opt => opt.MapFrom(src => src.GRADO_DES))
    //        .ForMember(dest => dest.Carrera, opt => opt.MapFrom(src => src.CARRERA))
    //        .ForMember(dest => dest.CarreraDescripcion, opt => opt.MapFrom(src => src.CARRERA_DES))
    //        .ForMember(dest => dest.Facultad, opt => opt.MapFrom(src => src.FACULTAD))
    //        .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.Foto))
    //        .ForMember(dest => dest.PKCODIGO, opt => opt.MapFrom(src => src.PKCODIGO))
    //        .ReverseMap();
    //}

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserEntity, UserDto>()
            .Map(dest => dest.Estado, src => src.ESTADO)
            .Map(dest=>dest.Nombres,src=>src.NOMBRES)
            .Map(dest => dest.Mensaje, src => src.MENSAJE)
            .Map(dest => dest.Alumno, src => src.ALUMNO)
            .Map(dest => dest.Institucion, src => src.INSTITUCION)
            .Map(dest => dest.InstitucionDes, src => src.INSTITUCION_DES)
            .Map(dest => dest.Grado, src => src.GRADO)
            .Map(dest => dest.GradoDes, src => src.GRADO_DES)
            .Map(dest => dest.Carrera, src => src.CARRERA)
            .Map(dest => dest.CarreraDescripcion, src => src.CARRERA_DES)
            .Map(dest => dest.Facultad, src => src.FACULTAD)
            .Map(dest => dest.Foto, src => src.Foto)
            .Map(dest => dest.PKCODIGO, src => src.PKCODIGO)
            .TwoWays();
    }
}