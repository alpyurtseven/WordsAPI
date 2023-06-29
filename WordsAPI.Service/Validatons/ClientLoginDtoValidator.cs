using System;
using FluentValidation;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Service.Validatons
{
	public class ClientLoginDtoValidator:AbstractValidator<ClientLoginDTO>
	{
		public ClientLoginDtoValidator()
		{
            RuleFor(dto => dto.Id)
              .NotEmpty().WithMessage("Id alanı boş olmamalı.")
              .NotNull().WithMessage("Id alanı null olmamalı.");

            RuleFor(dto => dto.Secret)
                .NotEmpty().WithMessage("Secret alanı boş olmamalı.")
                .NotNull().WithMessage("Secret alanı null olmamalı.")
                .MinimumLength(32).WithMessage("Secret en az 32 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Secret en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Secret en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Secret en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Secret en az bir şekil içermelidir.");
        }
	}
}

