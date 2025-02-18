using FluentValidation;

namespace FwksLabs.Orbita.Core.Entities.Validators;

public sealed class SkillEntityValidator : AbstractValidator<SkillEntity>
{
    public SkillEntityValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Level).GreaterThanOrEqualTo(1);
    }
}