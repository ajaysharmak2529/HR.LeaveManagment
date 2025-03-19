using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Helpers;

public static class TokenHelper
{
    public static Task<string?> GetTokenClaims(this HttpRequest _context, string key)
    {
        var bearerToken = _context.HttpContext.Request.Headers["Authorization"].ToString();

        var token = bearerToken.Substring("Bearer ".Length).Trim();

        var handler = new JwtSecurityTokenHandler();
        var value = handler.ReadJwtToken(token).Claims.FirstOrDefault(x => x.Type == key)?.Value;

        return Task.FromResult(value);
    }
}
