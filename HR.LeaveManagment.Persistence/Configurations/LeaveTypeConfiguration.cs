using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    Name = "Sick"
                },
                new LeaveType
                {
                    Id = 3,
                    Name = "Maternity"
                },
                new LeaveType
                {
                    Id = 4,
                    Name = "Paternity"
                },
                new LeaveType
                {
                    Id = 5,
                    Name = "Unpaid"
                }
            );
        }
    }
}
