// using System.ex
// using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CoWorking.Data.Context
{
    public class ConnectionStringBuilder
    {
        public static string GetConnectionString(IConfigurationRoot Configuration, string connectionName = "default")
        {
            var server = Configuration.GetSection("DbConnections:" + connectionName + ":Server").Value;
            var database = Configuration.GetSection("DbConnections:" + connectionName + ":Database").Value;
            var trustedConnection = Configuration.GetSection("DbConnections:" + connectionName + ":Trusted_Connection").Value;

            return "Server=" + server + ";" +
                    "Database=" + database + ";" +
                    "Trusted_Connection=" + trustedConnection;
        }
    }
}