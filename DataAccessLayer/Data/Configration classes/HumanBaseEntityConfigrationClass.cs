
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configration_classes
{
    internal class HumanBaseEntityConfigrationClass<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : HumanBaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(t => t.Id).UseIdentityColumn(1, 1);
            builder.Property(t => t.Age).IsRequired();
            builder.Property(t => t.FName).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(t => t.LName).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(t => t.Gender)
                   .HasConversion(gender => gender.ToString(),
                                  togender => (Gender)Enum.Parse(typeof(Gender), togender))
                   .HasMaxLength(10)
                   .IsRequired();
            builder.Property(t => t.LastActive).HasComputedColumnSql("GETDATE()");
            builder.Property(t => t.IsDeleted).HasColumnType("bit").IsRequired().HasDefaultValue(false);
            builder.Property(t => t.CreatedBy).IsRequired();
            builder.Property(t => t.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(t => t.LastModifiedBy).IsRequired();
            builder.Property(t => t.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(t => t.AccountId).IsRequired(false);
            builder.Property(t => t.levelFK).IsRequired(false);
        }
    }
}