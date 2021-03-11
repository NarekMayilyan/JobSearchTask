using FluentValidation;

namespace JobSearch.Models.Account
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username Empty");
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password Empty");
        }
    }
}
