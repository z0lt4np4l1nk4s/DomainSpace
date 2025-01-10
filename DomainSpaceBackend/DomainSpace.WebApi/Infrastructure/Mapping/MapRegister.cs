namespace DomainSpace.WebApi.Infrastructure.Mapping;

/// <summary>
/// Mapster register
/// </summary>
public class MapRegister : IRegister
{
    /// <inheritdoc cref="IRegister.Register(TypeAdapterConfig)" />
    public void Register(TypeAdapterConfig config)
    {
        //User
        //config.NewConfig<UserEntity, UserDto>()
        //    .Map(destination => destination.PhoneNumber, source => source.FullPhoneNumber);
    }
}