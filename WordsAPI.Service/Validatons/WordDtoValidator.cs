using System;
using FluentValidation;
using WordsAPI.Core.DTOs;

namespace WordsAPI.Service.Validatons
{
	public class WordDtoValidator : AbstractValidator<WordDTO>
	{
		public WordDtoValidator()
		{
			RuleFor(wordDto => wordDto.Word)
				.NotNull().WithMessage("The Word field is required")
				.NotEmpty().WithMessage("The Word field must not be empty.");

			RuleFor(wordDto => wordDto.Translations)
				.NotNull().WithMessage("Translations field is required");

			RuleFor(wordDto => wordDto.Categories)
				.NotNull().WithMessage("Categories field is required");

			RuleFor(wordDto => wordDto.Status)
				.Must(status => status == 0 || status == 1).WithMessage("Status must be 0 or 1");
        }
	}
}

