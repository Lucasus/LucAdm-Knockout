using FluentValidation;

namespace KnockAdm
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).SetValidator(new IdValidator());
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("User name is required");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required");
        }
    }
}