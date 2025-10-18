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
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary Key
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn(1, 1);

            // Properties
            builder.Property(r => r.Name)
                   .HasColumnType("nvarchar(20)")
                   .IsRequired();

        }
    }
}