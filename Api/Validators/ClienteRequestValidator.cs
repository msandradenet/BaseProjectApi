using Domain.Enums;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Shared.Extensions;
using System;

namespace API.Validators
{
    public class ClienteRequestValidator : AbstractValidator<ClienteRequest>
    {
        public ClienteRequestValidator(IHttpContextAccessor httpContextAccessor)
        {
            var method = httpContextAccessor.GetMethod();

            if (method == Method.PUT)
                RuleFor(m => m.IdCliente).NotEmpty();

            RuleFor(m => m.Nome)
                .NotEmpty().WithMessage("Informe o nome do Cliente; ")
                .Length(3, 30).WithMessage("O nome deve ter entre 3 e 30 caracteres; ");

            RuleFor(m => m.Sobrenome)
                .NotEmpty().WithMessage("Informe o sobrenome do Cliente; ")
                .Length(2, 100).WithMessage("O sobrenome deve ter entre 2 e 100 caracteres; ");

            RuleFor(m => m.DtNasc)
                .NotEmpty().WithMessage("Informe a data de nascimento do Cliente; ");
                //.Must(ValidarIdade).WithMessage("Informe idade acima de 10 anos; ");

            RuleFor(m => m.Cpf)
                .IsValidCPF()
                .WithMessage("Informe um CPF válido; ");
        }

        private static bool ValidarIdade(DateTime dtNasc)
        {
            return dtNasc <= DateTime.Now.AddYears(-10);
        }
    }
}
