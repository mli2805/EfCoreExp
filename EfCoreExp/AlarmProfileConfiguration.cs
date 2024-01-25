using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreExp
{
    public class AlarmProfileConfiguration : IEntityTypeConfiguration<AlarmProfileEf>
    {
        public void Configure(EntityTypeBuilder<AlarmProfileEf> builder)
        {
            builder.ToTable("AlarmProfile");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Kind)
                .HasConversion(
                v => v.ToString(),
                v => (AlarmProfileKind)Enum.Parse(typeof(AlarmProfileKind), v))
                .IsUnicode(false)
                .IsRequired();

            builder.Property(x => x.IsProvisioningMode).IsRequired();
        }
    }
}
