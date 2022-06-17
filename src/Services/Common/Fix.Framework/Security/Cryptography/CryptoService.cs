using System.Security.Cryptography;
using System.Text;

namespace Fix.Security.Cryptography
{
    public class CryptoService : ICryptoService
    {
        public HashString Encrypt(string text, HashTypes hashType)
        {
            HashAlgorithm algorithm = null;
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            switch (hashType)
            {
                case HashTypes.MD5: algorithm = MD5.Create(); break;
                case HashTypes.SHA512: algorithm = SHA512.Create(); break;
            }
            StringBuilder builder = new StringBuilder();
            var computed = algorithm.ComputeHash(textBytes);
            for (int i = 0; i < computed.Length; i++)
            {
                builder.Append(computed[i].ToString("x2"));
            }

            algorithm.Dispose();
            return new HashString(builder.ToString());
        }
    }
}
