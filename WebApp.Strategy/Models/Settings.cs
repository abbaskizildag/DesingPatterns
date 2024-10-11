namespace WebApp.Strategy.Models
{
    public class Settings
    {
        public static string claimDatabaseType = "databastype";

        public EDatabaseType DatabaseType;

        public EDatabaseType GetDefaultDbType => EDatabaseType.SqlServer;
    }
}
