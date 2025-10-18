

//using DataAccessLayer.Models.Notifications;
//using DataAccessLayer.Models.Teachers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DataAccessLayer.Data.Configration_classes
//{
//    public class NotificationConfigrationClass: BaseOfAllEntityConfigrationClass<Notification>, IEntityTypeConfiguration<Notification>

//    {
//        public void Configure(EntityTypeBuilder<Notification> builder)
//        {
//            builder.Property(a => a.Header).HasColumnType("nvarchar(50)").IsRequired();
//            builder.Property(a => a.Body).HasColumnType("nvarchar(600)").IsRequired();

//            base.Configure(builder);

//        }
//    }
//}

//using DataAccessLayer.Models.Notifications;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DataAccessLayer.Data.Configration_classes
//{
//    public class NotificationConfigrationClass : BaseOfAllEntityConfigrationClass<Notification>, IEntityTypeConfiguration<Notification>
//    {
//        public void Configure(EntityTypeBuilder<Notification> builder)
//        {
//            // إعدادات الحقول الأساسية
//            builder.Property(a => a.Header).HasColumnType("nvarchar(50)").IsRequired();
//            builder.Property(a => a.Body).HasColumnType("nvarchar(600)").IsRequired();
//            builder.Property(a => a.SentAt).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
//            builder.Property(a => a.IsRead).HasColumnType("bit").HasDefaultValue(false);

//            // إعداد العلاقة مع UserNotification
//            builder.HasMany(n => n.UserNotifications)
//                   .WithOne(un => un.Notification)
//                   .HasForeignKey(un => un.NotificationId)
//                   .OnDelete(DeleteBehavior.Cascade);

//            // استدعاء إعدادات الكلاس الأساسي
//            base.Configure(builder);
//        }
//    }
//}

using DataAccessLayer.Models.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configration_classes
{
    public class NotificationConfigrationClass : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            // Primary Key
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).UseIdentityColumn(1, 1);

            // Properties
            builder.Property(n => n.Header)
                   .HasColumnType("nvarchar(50)")
                   .IsRequired();
            builder.Property(n => n.Body)
                   .HasColumnType("nvarchar(600)")
                   .IsRequired();
            builder.Property(n => n.SentAt)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");
            builder.Property(n => n.IsRead)
                   .HasColumnType("bit")
                   .HasDefaultValue(false);
            builder.Property(n => n.CreatedBy)
                   .HasColumnType("int")
                   .IsRequired();
            builder.Property(n => n.CreatedOn)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");
            builder.Property(n => n.LastModifiedBy)
                   .HasColumnType("int")
                   .IsRequired();
            builder.Property(n => n.LastModifiedOn)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()");
            builder.Property(n => n.IsDeleted)
                   .HasColumnType("bit")
                   .HasDefaultValue(false);

            // Relationship with UserNotification
            builder.HasMany(n => n.UserNotifications)
                   .WithOne(un => un.Notification)
                   .HasForeignKey(un => un.NotificationId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Table Name
            builder.ToTable("Notifications");
        }
    }
}