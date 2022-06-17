using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fix.Security.OpenAuthentication.Jwt
{
    public interface IJwtTokenValidator : IScoped
    {
        ClaimsPrincipal Validate(string token);
    }


    public class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly JwtConfig configuration;

        public JwtTokenValidator(JwtConfig configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public ClaimsPrincipal Validate(string token)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length, token.Length - "Bearer ".Length);
            }
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.IssuerSigningKey = JwtSecurityKey.Create(configuration.SecretKey);
            validationParameters.ValidAudience = configuration.Audience;
            validationParameters.ValidIssuer = configuration.Issuer;

            return new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}
