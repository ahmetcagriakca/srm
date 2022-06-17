using Fix.Data;
using Fix.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Fix.Security
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private const string TOKEN_KEY = "Authorization";
        protected readonly DbContext Context;
        private readonly IDbContextLocator dbContextLocator;
        private readonly IHeaderValueProvider headerValueProvider;

        public AuthenticationRepository(
            IDbContextLocator dbContextLocator,
            IHeaderValueProvider headerValueProvider
            )
        {
            this.dbContextLocator = dbContextLocator ?? throw new ArgumentNullException(nameof(dbContextLocator));
            this.headerValueProvider = headerValueProvider ?? throw new ArgumentNullException(nameof(headerValueProvider));
            Context = dbContextLocator.Current;
        }
        public IQueryable<AuthenticationEntity> Table
        {
            get
            {
                return Context.Set<AuthenticationEntity>();
            }
        }

        public AuthenticationEntity GetAuthenticationEntity(string token, string refreshToken)
        {
            var entity = Table.FirstOrDefault(en =>
            en.Token == token &&
            en.RefreshToken == refreshToken &&
            en.ExpiredOn > DateTime.UtcNow &&
            en.IsActive == true
            );
            return entity;
        }

        public void CreateAuthenticationEntity(AuthenticationEntity entity)
        {
            var range = Table.Where(en =>
               en.RefreshToken == entity.RefreshToken
               );
            Context.Set<AuthenticationEntity>().AttachRange(range);
            Context.Set<AuthenticationEntity>().RemoveRange(range);
            Context.Set<AuthenticationEntity>().Add(entity);
        }
        public void UpdateAuthenticationEntity(AuthenticationEntity entity)
        {
            var authenticationEntity = Table.FirstOrDefault(en =>
               en.UserId == entity.UserId &&
               en.RefreshToken == entity.RefreshToken
               );
            authenticationEntity.Token = entity.Token;
            Context.Set<AuthenticationEntity>().Attach(authenticationEntity);
            Context.Set<AuthenticationEntity>().Update(authenticationEntity);
        }
        public void DeleteAuthenticationIdentity(long userId)
        {
            if (headerValueProvider.TryGet(TOKEN_KEY, out string token))
            {
                var range = Table.Where(en =>
                   en.UserId == userId &&
                   en.Token == token
                   );
                Context.Set<AuthenticationEntity>().AttachRange(range);
                Context.Set<AuthenticationEntity>().RemoveRange(range);
            }
        }
    }
}
