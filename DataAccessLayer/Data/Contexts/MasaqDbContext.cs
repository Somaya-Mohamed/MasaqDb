//using DataAccessLayer.Models;
//using DataAccessLayer.Models.Accounts;
//using DataAccessLayer.Models.Admins;
//using DataAccessLayer.Models.Announcements;
//using DataAccessLayer.Models.Contents.Answers;
//using DataAccessLayer.Models.Contents.Comments;
//using DataAccessLayer.Models.Contents.Courses;
//using DataAccessLayer.Models.Contents.Exams;
//using DataAccessLayer.Models.Contents.Lessons;
//using DataAccessLayer.Models.Contents.Questions;
//using DataAccessLayer.Models.Levels;
//using DataAccessLayer.Models.Notifications;
//using DataAccessLayer.Models.Students;
//using DataAccessLayer.Models.Teachers;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection;

//namespace DataAccessLayer.Data.Contexts
//{
//    public class MasaqDbContext : DbContext
//    {
//        public MasaqDbContext(DbContextOptions<MasaqDbContext> options) : base(options)
//        {
//        }


//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            modelBuilder.Entity<HumanBaseEntity>().UseTpcMappingStrategy();

//            // Configure derived types
//            modelBuilder.Entity<Student>().ToTable("Students");
//            modelBuilder.Entity<Teacher>().ToTable("Teachers");

//            // علاقة One-to-One بين Account و Admin
//            modelBuilder.Entity<Account>()
//                .HasOne(a => a.Admin)
//                .WithOne(a => a.Account)
//                .HasForeignKey<Admin>(a => a.AccountId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // علاقة One-to-One بين Account و Teacher
//            modelBuilder.Entity<Account>()
//                .HasOne(a => a.Teacher)
//                .WithOne(t => t.Account)
//                .HasForeignKey<Teacher>(t => t.AccountId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // علاقة One-to-One بين Account و Student
//            modelBuilder.Entity<Account>()
//                .HasOne(a => a.Student)
//                .WithOne(s => s.Account)
//                .HasForeignKey<Student>(s => s.AccountId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // علاقة One-to-One بين Account و UserProfile
//            modelBuilder.Entity<Account>()
//                .HasOne(a => a.UserProfile)
//                .WithOne(u => u.Account)
//                .HasForeignKey<UserProfile>(u => u.AccountId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // علاقة Many-to-Many بين Account و Role
//            modelBuilder.Entity<Account>()
//                .HasMany(a => a.Roles)
//                .WithMany(r => r.Accounts)
//                .UsingEntity(j => j.ToTable("AccountRoles"));


//            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//        }
//        public DbSet<Account> Accounts { get; set; }
//        public DbSet<Role> Roles { get; set; }
//        public DbSet<UserProfile> UserProfiles { get; set; }
//        public DbSet<Teacher> Teachers { get; set; }
//        public DbSet<Student> Students { get; set; }
//        public DbSet<Admin> Admins { get; set; }
//        public DbSet<Level> Levels { get; set; }
//        public DbSet<Course> Courses { get; set; }
//        public DbSet<Announcement> Announcements { get; set; }
//        public DbSet<Answer> Answer { get; set; }
//        public DbSet<Exam> Exams { get; set; }
//        public DbSet<Lesson> Lessons { get; set; }
//        public DbSet<Question> Questions { get; set; }
//        public DbSet<Notification> Notifications { get; set; }

//    }
//}

using DataAccessLayer.Models;
using DataAccessLayer.Models.Accounts;
using DataAccessLayer.Models.Admins;
using DataAccessLayer.Models.Announcements;
using DataAccessLayer.Models.Contents.Answers;
using DataAccessLayer.Models.Contents.Comments;
using DataAccessLayer.Models.Contents.Courses;
using DataAccessLayer.Models.Contents.Exams;
using DataAccessLayer.Models.Contents.Lessons;
using DataAccessLayer.Models.Contents.Questions;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Notifications;
using DataAccessLayer.Models.Students;
using DataAccessLayer.Models.Teachers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer.Data.Contexts
{
    public class MasaqDbContext : DbContext
    {
        public MasaqDbContext(DbContextOptions<MasaqDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // إعداد TPH لـ HumanBaseEntity
            modelBuilder.Entity<HumanBaseEntity>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Student>("Student")
                .HasValue<Teacher>("Teacher");

            // علاقة One-to-One بين Account و Admin
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Admin)
                .WithOne(a => a.Account)
                .HasForeignKey<Admin>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة One-to-One بين Account و Teacher
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Teacher)
                .WithOne(t => t.Account)
                .HasForeignKey<Teacher>(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة One-to-One بين Account و Student
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Student)
                .WithOne(s => s.Account)
                .HasForeignKey<Student>(s => s.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة One-to-One بين Account و UserProfile
            modelBuilder.Entity<Account>()
                .HasOne(a => a.UserProfile)
                .WithOne(u => u.Account)
                .HasForeignKey<UserProfile>(u => u.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة Many-to-Many بين Account و Role
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Roles)
                .WithMany(r => r.Accounts)
                .UsingEntity(j => j.ToTable("AccountRoles"));

            // إعدادات جدول UserNotification
            modelBuilder.Entity<UserNotification>()
                .HasKey(un => new { un.HumanBaseEntityId, un.NotificationId });

            modelBuilder.Entity<UserNotification>()
                .HasOne(un => un.User)
                .WithMany(u => u.UserNotifications)
                .HasForeignKey(un => un.HumanBaseEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserNotification>()
                .HasOne(un => un.Notification)
                .WithMany(n => n.UserNotifications)
                .HasForeignKey(un => un.NotificationId)
                .OnDelete(DeleteBehavior.Cascade);

            // إعدادات إضافية للعلاقات
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.student)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StudentIdFK)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentLesson>()
                .HasOne(sl => sl.Student)
                .WithMany(s => s.StudentLessons)
                .HasForeignKey(sl => sl.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentAnswer>()
                .HasOne(sa => sa.StudentExam)
                .WithMany(se => se.Answers)
                .HasForeignKey(sa => sa.StudentExamId)
                .OnDelete(DeleteBehavior.NoAction); 


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<QuestionOptions> QuestionOptions { get; set; }

        public DbSet<StudentExam> StudentExams { get; set; } 
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
    }
}
