namespace MSA.Domain.Common
{
    public class AppConfig
    {
        public static ConnectionStrings ConnectionStrings { get; set; } = null!;
        public static Admin Admin { get; set; } = null!;
    }
    public class Admin
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }
}
