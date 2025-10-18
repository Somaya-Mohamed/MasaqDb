
using DataAccessLayer.Models.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Data.Configration_classes
{
    internal class StudentConfigrationClass : HumanBaseEntityConfigrationClass<Student>, IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            base.Configure(builder); // يطبق إعدادات HumanBaseEntity

            builder.Property(x => x.Grade)
                   .IsRequired();
            builder.Property(x => x.ParentPhoneNumber)
                   .HasColumnType("nvarchar(15)")
                   .IsRequired();
            builder.Property(x => x.AccountId)
                   .IsRequired();
            builder.Property(x => x.levelFK)
                   .IsRequired();
            builder.Property(x => x.Teacher_AccountId)
                   .IsRequired(false); // Nullable عشان ON DELETE NO ACTION

            // علاقة الطالب بالمعلم
            builder.HasOne(s => s.TeacherAccount)
                   .WithMany()
                   .HasForeignKey(s => s.Teacher_AccountId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
