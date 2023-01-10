using Data.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Validation
{
    public class WordValidator:AbstractValidator<T>
    {
        public WordValidator()
        {
            Expression<Func<T, string>> value = (c => c.Word);
            this.RuleFor<string>((Expression<Func<English, string>>)value).NotNull().NotEmpty().WithMessage("Word should not empty or null").MaximumLength(30);
        }
    }
}
