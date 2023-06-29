using System;
using FluentValidation;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Service.Validatons
{
	public class RefreshTokenDtoValidator:AbstractValidator<RefreshTokentDTO>
	{
		public RefreshTokenDtoValidator()
		{
			RuleFor(refreshTokenDto => refreshTokenDto.Token)
				.NotNull().WithMessage("RefreshToken alanı gerekli")
				.NotEmpty().WithMessage("RefreshToken alanı boş olmamalı");
		}
	}
}

