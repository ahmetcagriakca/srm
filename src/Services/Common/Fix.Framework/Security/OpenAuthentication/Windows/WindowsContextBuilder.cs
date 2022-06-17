using System;

namespace Fix.Security.OpenAuthentication.Windows
{
    public interface IWindowsContextBuilder : IDependency
    {
        T Create<T>(string key) where T : IIdentityContext;
    }
    public class WindowsContextBuilder : IWindowsContextBuilder
    {
        private readonly WindowsConfig configuration;

        public WindowsContextBuilder(WindowsConfig configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public T Create<T>(string key) where T : IIdentityContext
        {

            var expiredOn = GetExpiryDate();
            //var tokenGuid = Guid.NewGuid().ToString();
            //var token = Create(JwtSecurityKey.Create(configuration.SecretKey), tokenGuid, key, expiredOn);
            string userName = key.Substring(key.IndexOf("\\") + 1);

            //var user = repository.Users.Find(Builders<User>.Filter
            //	.Where(en => en.UserName == userName.ToLower())).FirstOrDefault();
            //var userName = key.Substring(key.IndexOf("\\"));
            IIdentityContext context = new WindowsContext
            {
                ExpiredOn = expiredOn,
                //Id = tokenGuid,
                Key = key,
                UserName = userName
            };

            return (T)context;
            throw new NotImplementedException();
        }

        private DateTime GetExpiryDate()
        {
            return DateTime.Now.AddMinutes(configuration.ExpiryInMinute);
        }
    }
}
