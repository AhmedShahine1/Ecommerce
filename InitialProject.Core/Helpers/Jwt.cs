namespace Ecommerce.Core.Helpers
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience;
        public int DurationInDays { get; set; }
    }
}