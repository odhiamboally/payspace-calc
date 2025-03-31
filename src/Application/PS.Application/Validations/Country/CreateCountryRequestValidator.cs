namespace PS.Application.Validations.Country;
using FluentValidation;
using PS.Application.Dtos.Country;

public class CreateCountryRequestValidator : AbstractValidator<CreateCountryRequest>
{
    public CreateCountryRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Country Code is required.");

        RuleFor(x => x.TaxRegime)
            .NotEmpty()
            .WithMessage("Tax Regime is required.");


    }
}
