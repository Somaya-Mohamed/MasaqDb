using DataAccessLayer.Models.Accounts;
using DataAccessLayer.Models.Admins;
using DataAccessLayer.Models.Students;
using DataAccessLayer.Models.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configration_classes
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Primary Key
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).UseIdentityColumn(1, 1);

            // Properties
            builder.Property(a => a.Email)
                   .HasColumnType("nvarchar(50)")
                   .IsRequired();
            builder.Property(a => a.PasswordHash)
                   .HasColumnType("nvarchar(256)")
                   .IsRequired();
            builder.Property(a => a.IsActive)
                   .HasColumnType("bit")
                   .IsRequired()
                   .HasDefaultValue(true);
            builder.Property(a => a.CreatedOn)
                   .HasColumnType("datetime")
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.LastLogin)
                   .HasColumnType("datetime")
                   .IsRequired(false);
            builder.Property(a => a.FailedLoginAttempts)
                   .HasColumnType("int")
                   .IsRequired()
                   .HasDefaultValue(0);

        }
    }
}