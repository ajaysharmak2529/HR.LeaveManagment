using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagement.Identity
{
    public class LeaveManagementIdentityDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementIdentityDbContext>
    {
        public LeaveManagementIdentityDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<LeaveManagementIdentityDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("LeaveManagementConnectionString"));

            return new LeaveManagementIdentityDbContext(optionsBuilder.Options);
        }
    }
}
