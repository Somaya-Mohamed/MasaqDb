
using DataAccessLayer.Models.Admins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configration_classes
{
    internal class AdminConfigrationClass : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn(1, 1);

            builder.Property(a => a.FName)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired();
            builder.Property(a => a.LName)
                   .HasColumnType("nvarchar(max)")
                   .IsRequired();
            builder.Property(a => a.Gender)
                   .HasColumnType("int")
                   .IsRequired();
            builder.Property(a => a.AccountId)
                   .HasColumnType("int")
                   .IsRequired();
            builder.Property(a => a.LastActive)
                   .HasColumnType("datetime2")
                   .IsRequired();
            builder.Property(a => a.IsDeleted)
                   .HasColumnType("bit")
                   .IsRequired();
        }
    }
}