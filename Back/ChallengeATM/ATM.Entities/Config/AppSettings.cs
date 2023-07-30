namespace ATM.Entities.Config
{
    public class AppSettings
    {
        public string[] Origin { get; set; }
        public int ExpiryDuration { get; set; }
        public string Secret { get; set; }
    }
}