using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SaaV.SaaS.WebApi.Endpoints
{
    internal record struct GetBearerTokenResponse(string Token);

    internal static class AuthenticationEndpoints
    {
        public const string SecretKey = "12345678901234567890";
        private const string _userName = "cesar.liebana";
        private const int _tenantId = 1;
        private const int _tokenLifetimeInSeconds = 3600;

        public static IResult GetBearerToken()
        {                                    
            return Results.Ok(new GetBearerTokenResponse(GenerateBearerToken(
                new Guid().ToString(),
                _userName,
                _tokenLifetimeInSeconds
                )));
        }

        private static string GenerateBearerToken(string userId, string userName, int tokenLifetime)
        {
            List<Claim> claims = new();
            claims.Add(new Claim(ClaimTypes.Sid, userId));
            claims.Add(new Claim(ClaimTypes.Name, userName));
            claims.Add(new Claim(ClaimTypes.GroupSid, _tenantId.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            JwtSecurityToken tokenOptions = new(
                expires: DateTime.UtcNow.AddSeconds(_tokenLifetimeInSeconds),
                signingCredentials: new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)), SecurityAlgorithms.HmacSha256),
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
