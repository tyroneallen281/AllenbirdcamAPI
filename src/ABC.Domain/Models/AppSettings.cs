namespace ABC.Domain.Models
{
    public class AppSettings
    {
        public static string AuthIssuer { get; set; }
        public static string AuthAudience { get; set; }
        public static string AuthSecret { get; set; }
        public static string CurrentAppVersion { get; set; }
        public static string CMAPiKey { get; set; }
        public static string StorageConnectionString { get; set; }
        public static string BlobUrl { get; set; }
    }
}