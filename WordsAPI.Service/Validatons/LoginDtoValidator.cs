using System;
using FluentValidation;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Service.Validatons
{
	public class LoginDtoValidator:AbstractValidator<UserLoginDTO>
	{
		public LoginDtoValidator()
		{
            RuleFor(loginDto => loginDto.Email)
                .NotNull().WithMessage("E posta adresi alanı gerekli.")
                .NotEmpty().WithMessage("E posta adresi alanı boş olmamalı.")
                .EmailAddress().WithMessage("Geçerli bir e posta adresi girin.");

            RuleFor(loginDto => loginDto.Password)
                .NotNull().WithMessage("Şifre alanı gerekli")
                .NotEmpty().WithMessage("Şifre boş olmamalı.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir."); ;
        }
	}
}