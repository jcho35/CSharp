using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;

namespace ContosoUniversity.DAL
{
    /// <summary>
    /// Entity framework automatically runs the code of a class that inherits from DbConfiguration.
    /// Since you declared interceptors independently, you can choose which add and use.
    /// !ATTENTION! If you add the same interceptor twice, you see the logs twice.
    /// Interceptors are executed in the order of registration.
    /// Without SetExecutionStrategy, application stops at first exception (annoying for user).
    /// </summary>
    public class SchoolConfiguration : DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy()); 
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());
        }
    }
}