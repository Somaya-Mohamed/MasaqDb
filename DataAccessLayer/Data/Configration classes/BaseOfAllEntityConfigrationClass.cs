
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configration_classes
{
    public class BaseOfAllEntityConfigrationClass<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseOfAllContentEntities
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(b => b.Id).UseIdentityColumn(1, 1);
            builder.Property(b => b.CreatedBy).IsRequired();
            builder.Property(b => b.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.LastModifiedBy).IsRequired();
            builder.Property(b => b.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(b => b.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
