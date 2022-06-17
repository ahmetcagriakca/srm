using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Fix.Security.OpenAuthentication.Jwt
{
    public class JwtTokenBuilder : IJwtTokenBuilder
    {
        private readonly JwtConfig configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JwtTokenBuilder(JwtConfig configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public T Create<T>(string key, out string jwtToken) where T : IIdentityContext
        {
            var expiredOn = GetExpiryDate();
            var finalExpiredOn = GetFinalExpiryDate();
            var tokenGuid = Guid.NewGuid().ToString();
            var token = Create(JwtSecurityKey.Create(configuration.SecretKey), tokenGuid, key, expiredOn);
            var refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty);
            SetClaimsPrincipal(token.Claims);
            jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            IIdentityContext context = new JwtContext
            {
                Token = jwtToken,
                ExpiredOn = expiredOn,
                TokenType = "Bearer ",
                Id = tokenGuid,
                Key = key,
                RefreshToken = refreshToken,
                FinalExpiredOn = finalExpiredOn
            };

            return (T)context;
        }

        private JwtSecurityToken Create(SecurityKey securityKey, string guid, string uniqueName, DateTime expiredOn)
        {
            if (securityKey == null)
            {
                throw new ArgumentNullException(nameof(securityKey));
            }

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, configuration.Subject),
              new Claim(JwtRegisteredClaimNames.Jti, guid),
              new Claim(JwtRegisteredClaimNames.UniqueName,uniqueName )
            };

            return new JwtSecurityToken(
                              issuer: configuration.Issuer,
                              audience: configuration.Audience,
                              claims: claims,
                              expires: expiredOn,
                              signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
        }

        private DateTime GetExpiryDate()
        {
            return DateTime.UtcNow.AddMinutes(configuration.ExpiryInMinute);
        }
        private DateTime GetFinalExpiryDate()
        {
            return DateTime.UtcNow.AddMinutes(configuration.FinalExpiration);
        }


        private void SetClaimsPrincipal(IEnumerable<Claim> claims)
        {
            httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
        }
    }
}
