using UW.AdminPagoExpress.Domain.Entities;
using UW.AdminPagoExpress.Shared.Dtos;

namespace UW.AdminPagoExpress.Application.Mapping;

public class MensajeProfile : IRegister
{
    //public MensajeProfile()
    //{
    //    CreateMap<MensajeEntity, MensajeDto>()
    //        .ForMember(dest => dest.Mensaje, opt => opt.MapFrom(src => src.mensaje))
    //        .ReverseMap();
    //}

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MensajeEntity, MensajeDto>()
             .Map(dest => dest.Mensaje, src => src.mensaje)
             .TwoWays();
    }
}