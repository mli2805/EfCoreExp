using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreExp;

public class MonitoringProfileConfiguration : IEntityTypeConfiguration<MonitoringPortEf>
{
    public void Configure(EntityTypeBuilder<MonitoringPortEf> builder)
    {
        builder.ToTable("MonitoringPort");
        builder.HasKey(x => x.Id);

        builder.HasOne<AlarmProfileEf>()
            .WithMany(x => x.MonitoringPorts)
            .HasForeignKey(x => x.AlarmProfileId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

    }
}