namespace Fix.Security.Cryptography
{
    public class HashString
    {

        public HashString(string hash)
        {
            this.Value = hash;
        }
        public string Value { get; }
    }
}
