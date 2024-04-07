using AutoMapper;
using PasswordManager.Application.Contracts.Requests.Authentication;
using PasswordManager.Application.Contracts.Responses;
using PasswordManager.Domain.Entities;

namespace PasswordManager.Application.Mappers;

public class AuthenticationMapping : Profile
{
    public AuthenticationMapping()
    {
        CreateMap<RegisterRequest, User>()
            .ConstructUsing(request => new User(
                request.Username,
                request.Password,
                request.Email
            ));

        CreateMap<User, UserResponse>()
            .ConstructUsing(user => new UserResponse(
                user.Id,
                user.Username,
                user.Email
            ));
    }
}