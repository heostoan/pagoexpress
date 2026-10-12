using UW.AdminPagoExpress.Domain.Entities.Database.Login;
using UW.AdminPagoExpress.Shared.Dtos.User;

namespace UW.AdminPagoExpress.Application.Mapping;

public class ValidateAccessProfile : IRegister
{
    //public ValidateAccessProfile()
    //{
        
    //    CreateMap<ValidateAccessEntity, ValidateAccessDto>()
    //        .ReverseMap();
    //}

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ValidateAccessEntity, ValidateAccessDto>()
            .TwoWays();
    }
}
