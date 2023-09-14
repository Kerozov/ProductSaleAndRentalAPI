using Application.Models.OrderModels.Dtos;
using FluentValidation;

namespace Application.Helpers.Validators;

public class CreateOrderValidator :AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity can't be null");

    }
}