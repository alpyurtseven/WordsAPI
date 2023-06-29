using System;
using FluentValidation;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Service.Validatons
{
	public class RegisterDtoValidator: AbstractValidator<UserRegisterDTO>
	{
		public RegisterDtoValidator()
		{
            RuleFor(user => user.Name)
               .NotNull().WithMessage("İsim alanı gerekli.")
               .NotEmpty().WithMessage("İsim alanı boş olmamalı.");

            RuleFor(user => user.Surname)
                .NotNull().WithMessage("Soyisim alanı gerekli.")
                .NotEmpty().WithMessage("Soyisim alanı boş olmamalı.");

            RuleFor(user => user.Username)
                .NotNull().WithMessage("Kullanıcı adı alanı gerekli.")
                .NotEmpty().WithMessage("Kullanıcı adı alanı boş olmamalı.");

            RuleFor(user => user.Email)
                .NotNull().WithMessage("E posta alanı gerekli.")
                .NotEmpty().WithMessage("E posta alanı boş olmamalı.")
                .EmailAddress().WithMessage("Geçerli bir e posta adresi girin.");

            RuleFor(user => user.Password)
                .NotNull().WithMessage("Şifre alanı gerkeli")
                .NotEmpty().WithMessage("Şifre alanı boş olmamalı.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");
        }
	}
}

