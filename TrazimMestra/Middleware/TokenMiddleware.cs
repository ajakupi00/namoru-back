using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Options;

namespace TrazimMestra.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationContext dataContext, ITokenService tokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = tokenService.ValidateJwtToken(token);
            if(userId != null)
                context.Items["User"] = await dataContext.Users.FindAsync(userId.Value);

            await _next(context);
        }
    }

    public static class RequestTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestToken(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenMiddleware>();
        }
    }
}
