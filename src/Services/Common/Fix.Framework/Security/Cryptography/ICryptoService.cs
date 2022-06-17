namespace Fix.Security.Cryptography
{
    public interface ICryptoService : IScoped
    {
        HashString Encrypt(string text, HashTypes hashType);
    }
}
