using AutoMapper;
using Microservice.Application.Dtos;
using Microservice.Domain.Entities;

namespace Microservice.Application.Profiles;

/// <summary>
/// Представляет конфигурацию преобразования для типа <see cref="User" />.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserProfile" />.
    /// </summary>
    public UserProfile() =>
        CreateMap<User, UserDto>()
            .ForMember(dto => dto.Name, expression => expression.MapFrom(user => user.Name.Value))
            .ForMember(dto => dto.Phone, expression => expression.MapFrom(user => user.Phone == null ? null : (string?)user.Phone));
}
