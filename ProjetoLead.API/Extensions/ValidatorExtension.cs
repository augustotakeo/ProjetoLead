using FluentValidation.Results;
using ProjetoLead.API.Dtos;

namespace ProjetoLead.API.Extensions;

public static class ValidatorExtension
{
    public static ErrorDetails GetValidationError(this ValidationResult result)
    {
        var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        return new(errors);
    }
}
