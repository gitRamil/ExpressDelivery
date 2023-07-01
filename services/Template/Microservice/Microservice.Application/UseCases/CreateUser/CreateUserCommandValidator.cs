using FluentValidation;

namespace Microservice.Application.UseCases.CreateUser;

/// <summary>
/// Представляет валидатор для команды <see cref="CreateUserCommand" />.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateUserCommandValidator" />.
    /// </summary>
    public CreateUserCommandValidator()
    {
        const int userNameMaxLength = 100;

        RuleFor(p => p.UserName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Имя пользователя не может быть null")
            .NotEmpty()
            .WithMessage("Имя пользователя не может быть null")
            .Must(p => p is not null && p.Length <= userNameMaxLength)
            .WithMessage($"Имя не может быть больше {userNameMaxLength}");
    }
}
