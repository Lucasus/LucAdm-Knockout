using FluentValidation;

namespace KnockAdm
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.UserName).Matches(@"^[a-zA-Z][a-zA-Z0-9._\-]*$").WithMessage("User name is not correct");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not correct");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .Length(0, 100).WithMessage("Password is too long")
                .Length(6, 100).WithMessage("Password is too short");
            RuleFor(x => x.RepeatedPassword).Equal(x => x.Password).WithMessage("Passwords are not the same");
        }
    }
}