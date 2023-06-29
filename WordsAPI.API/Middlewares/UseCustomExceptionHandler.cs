using Microsoft.AspNetCore.Diagnostics;
using SharedLibrary.Dtos;
using WordsAPI.SharedLibrary.Exceptions;

namespace WordsAPI.API.Middlewares
{
	public static class UseCustomExceptionHandler
	{
		public static void UseCustomException(this IApplicationBuilder app)
		{
            app.UseExceptionHandler(config =>
			{
				config.Run(async (context) =>
				{
					context.Response.ContentType = "application/json";

					var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
					var statusCode = exceptionFeature.Error switch
					{
						ClientSideException => 400,
						DirectoryNotFoundException => 404,
						_ => 500
					} ;

					context.Response.StatusCode = statusCode;

					var response = new NoContentCustomResponseDto(new List<string>() { exceptionFeature.Error.Message}, statusCode);

					await context.Response.WriteAsJsonAsync(response);
				});
			});
		}
	}
}

