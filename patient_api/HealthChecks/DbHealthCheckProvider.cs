using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using patient_api.Data;
using System.Threading;
using System.Threading.Tasks;

namespace patient_api.HealthChecks
{
    public class DbHealthCheckProvider : IHealthCheck
    {
        private readonly PatientContext context;
        public DbHealthCheckProvider(PatientContext _context)
        {
            context = _context;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext hcContext, CancellationToken cancellationToken = default)
        {
                  return await context.Database.CanConnectAsync(cancellationToken)
              ? HealthCheckResult.Healthy(context.Database.GetDbConnection().ConnectionString)
              : HealthCheckResult.Unhealthy(context.Database.GetDbConnection().ConnectionString);
        }
    }
}
