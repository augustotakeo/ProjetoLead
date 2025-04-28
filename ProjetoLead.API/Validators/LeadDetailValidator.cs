using FluentValidation;
using ProjetoLead.API.Dtos;

namespace ProjetoLead.API.Validators;

public class LeadDetailValidator : AbstractValidator<LeadDetailDto>
{
    private static readonly IReadOnlyList<int> _validationNumbers = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

    public LeadDetailValidator()
    {
        RuleFor(x => x.Cnpj)
            .NotNull().WithMessage("Preencher o CNPJ")
            .Length(14).WithMessage("CNPJ deve ter 14 caracteres")
            .Matches(@"^\d").WithMessage("CNPJ deve ter apenas números")
            .Must(CheckDigits).WithMessage("CNPJ Inválido");

        RuleFor(x => x.RazaoSocial)
            .NotNull().WithMessage("Preencher Razão Social")
            .MinimumLength(2).WithMessage("Razão Social deve ter pelo menos 2 caracteres")
            .MaximumLength(60).WithMessage("Razão Social deve ter no máximo 60 caracteres");

        RuleFor(x => x.CEP)
            .NotNull().WithMessage("Preencher CEP")
            .Matches(@"^\d").WithMessage("CEP deve ter apenas números")
            .Length(8).WithMessage("CEP deve ter 8 caracteres");

        RuleFor(x => x.Endereco)
            .NotNull().WithMessage("Preencher Endereço")
            .MinimumLength(3).WithMessage("Endereço deve ter pelo menos 3 caracteres")
            .MaximumLength(500).WithMessage("Endereço deve ter no máximo 500 caracteres");

        RuleFor(x => x.Numero)
            .NotNull().WithMessage("Preencher Número")
            .MinimumLength(1)
            .MaximumLength(50);

        RuleFor(x => x.Complemento)
            .MaximumLength(500).WithMessage("Complemento deve ter no máximo 500 caracteres");

        RuleFor(x => x.Bairro)
            .NotNull().WithMessage("Preencher Bairro")
            .MinimumLength(2).WithMessage("Bairro deve ter no mínimo 2 caracteres")
            .MaximumLength(50).WithMessage("Bairro deve ter no máximo 20 caracteres");

        RuleFor(x => x.Cidade)
            .NotNull().WithMessage("Preencher Cidade")
            .MinimumLength(2).WithMessage("Cidade deve ter no mínimo 2 caracteres")
            .MaximumLength(50).WithMessage("Cidade deve ter no máximo 50 caracters");

        RuleFor(x => x.Estado)
            .NotNull().WithMessage("Preencher Estado")
            .MinimumLength(2).WithMessage("Estado deve ter no mínimo 2 caracteres")
            .MaximumLength(50).WithMessage("Estado deve ter no máximo 50 caracteres");
    }

    private bool CheckDigits(string cnpj)
    {
        if (cnpj?.Length != 14)
            return true;

        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            var cnpjDigit = cnpj[i] - '0';
            sum += cnpjDigit * _validationNumbers[i + 1];
        }

        var digit = sum % 11;
        if (digit < 2)
            digit = 0;
        else
            digit = 11 - digit;

        if (digit != cnpj[12] - '0')
            return false;

        sum = 0;
        for (int i = 0; i < 13; i++)
        {
            var cnpjDigit = cnpj[i] - '0';
            sum += cnpjDigit * _validationNumbers[i];
        }

        digit = sum % 11;
        if (digit < 2)
            digit = 0;
        else
            digit = 11 - digit;

        if (digit != cnpj[13] - '0')
            return false;
        
        return true;
    }
}