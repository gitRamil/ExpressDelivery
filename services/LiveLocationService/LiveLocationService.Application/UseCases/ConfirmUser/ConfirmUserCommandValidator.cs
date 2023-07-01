using FluentValidation;

namespace LiveLocationService.Application.UseCases.ConfirmUser;
/// <summary>
/// Представляет валидатор для команды <see cref="ConfirmUserCommand"/>.
/// </summary>
public class ConfirmUserCommandValidator : AbstractValidator<ConfirmUserCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ConfirmUserCommandValidator" />.
    /// </summary>
    public ConfirmUserCommandValidator()
    {
        const string pattern = "^\\+[7]{1}-[0-9]{3}-[0-9]{3}-[0-9]{4}$";
        RuleFor(p => p.UserPhone)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("Укажите телефон пользователя.")
            .NotEmpty()
            .WithMessage("Укажите телефон пользователя.")
            .Matches(pattern)
            .WithMessage("Телефон должен быть в формате: +7-000-000-0000.");
    }
}
