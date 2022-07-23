using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Persistance.EntityConfiguration;

internal class OtpEntityConfiguration : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.Property<long>("Id");
        builder.Property<DateTime>("CreationDateTime").HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
    }
}
