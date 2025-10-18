using DataAccessLayer.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configration_classes
{
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            // Primary Key
            builder.HasKey(up => up.Id);
            builder.Property(up => up.Id).UseIdentityColumn(1, 1);

            // Properties
            builder.Property(up => up.PhoneNumber)
                   .HasColumnType("nvarchar(15)")
                   .IsRequired();
            builder.Property(up => up.Address)
                   .HasColumnType("nvarchar(50)")
                   .IsRequired();
            builder.Property(up => up.City)
                   .HasColumnType("nvarchar(20)")
                   .IsRequired();
            builder.Property(up => up.ProfilePicture)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired(false);
            builder.Property(up => up.AccountId)
                   .HasColumnType("int")
                   .IsRequired();
        }
    }
}