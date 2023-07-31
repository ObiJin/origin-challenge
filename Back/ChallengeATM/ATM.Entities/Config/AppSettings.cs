namespace ATM.Entities.Config
{
    public class AppSettings
    {
        public string[] Origin { get; set; }
        public int ExpiryDuration { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}