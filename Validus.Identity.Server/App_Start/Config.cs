using System.Configuration;

namespace Validus.Identity.Server
{
    public static class Config
    {
        public static string Host => ConfigurationManager.AppSettings["ServerHostUrl"];

        public static string HostDb => ConfigurationManager.ConnectionStrings["ServerHostDb"].ConnectionString;

        public static string AdminDb => ConfigurationManager.ConnectionStrings["ServerAdminDb"].ConnectionString;

        public static string MembershipDb => ConfigurationManager.ConnectionStrings["ServerMembershipDb"].ConnectionString;
    }
}
