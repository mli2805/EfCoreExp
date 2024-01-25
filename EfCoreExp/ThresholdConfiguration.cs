using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreExp;

public class ThresholdConfiguration : IEntityTypeConfiguration<ThresholdEf>
{
    public void Configure(EntityTypeBuilder<ThresholdEf> builder)
    {
        builder.ToTable("Threshold");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Parameter)
            .HasConversion(
                v =>  v.ToString(), 
                v => (RftsParameter)Enum.Parse(typeof(RftsParameter), v))
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.IsEnabled).IsRequired();
        builder.Property(x => x.IsSimple).IsRequired();

        builder.Property(x => x.SimpleValue).IsRequired(false);
        builder.Property(x => x.Minor).IsRequired(false);
        builder.Property(x => x.Major).IsRequired(false);
        builder.Property(x => x.Critical).IsRequired(false);

        builder.HasOne<AlarmProfileEf>()
            .WithMany(x=>x.Thresholds)
            .HasForeignKey(x=>x.AlarmProfileId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}