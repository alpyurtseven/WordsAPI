using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedLibrary.Dtos;

namespace WordsAPI.API.Filters
{
	public class ValidateFilterAttribute:ActionFilterAttribute
	{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(z => z.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(new NoContentCustomResponseDto(errors,400));
            }
        }
    }
}

