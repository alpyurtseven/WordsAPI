using System;
using FluentValidation;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Service.Validatons
{
	public class UserUpdateDtoValidator:AbstractValidator<UserUpdateDTO>
	{
		public UserUpdateDtoValidator()
		{
            RuleFor(user => user.Name)
             .NotNull().WithMessage("İsim alanı gerekli.")
             .NotEmpty().WithMessage("İsim alanı boş olmamalı.");

            RuleFor(user => user.Surname)
                .NotNull().WithMessage("Soyisim alanı gerekli.")
                .NotEmpty().WithMessage("Soyisim alanı boş olmamalı.");

            RuleFor(user => user.Email)
                .NotNull().WithMessage("E posta alanı gerekli.")
                .NotEmpty().WithMessage("E posta alanı boş olmamalı.")
                .EmailAddress().WithMessage("Geçerli bir e posta adresi girin.");

            RuleFor(user => user.Password)
                .Cascade(CascadeMode.Continue)
                .Must((user, password) => password == null || ValidatePassword(password))
                .WithMessage("Şifre en az 8 karakter uzunluğunda, en az 1 büyük harf, en az 1 küçük harf ve en az 1 rakam içermelidir");
        }

        private bool ValidatePassword(string password)
        {
            return password == null || (password.Length > 7 && password.Any(char.IsDigit) && password.Any(char.IsLower) && password.Any(char.IsUpper));
        }
    }
}

