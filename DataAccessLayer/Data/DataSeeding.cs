
using BCrypt.Net;
using DataAccessLayer.Contracts;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Accounts;
using DataAccessLayer.Models.Admins;
using DataAccessLayer.Models.Announcements;
using DataAccessLayer.Models.Contents.Courses;
using DataAccessLayer.Models.Contents.Exams;
using DataAccessLayer.Models.Contents.Lessons;
using DataAccessLayer.Models.Contents.Questions;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Students;
using DataAccessLayer.Models.Teachers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public class DataSeeding : IDataSeeding
    {
        private readonly MasaqDbContext _context;

        public DataSeeding(MasaqDbContext context)
        {
            _context = context;
        }


        public void AddFirstYearData()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // --------------------- Add Roles ---------------------
                    if (!_context.Roles.Any())
                    {
                        var roles = new List<Role>
                        {
                            new Role { Name = "Admin" },
                            new Role { Name = "Teacher" },
                            new Role { Name = "Student" }
                        };
                        _context.Roles.AddRange(roles);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Admin ---------------------
                    if (!_context.Admins.Any(a => a.Account.Email == "somaya.mohamed@gmail.com"))
                    {
                        var adminAccount = new Account
                        {
                            Email = "somaya.mohamed@gmail.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("PasswordSOMAYA!"),
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            FailedLoginAttempts = 0
                        };
                        var adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
                        if (adminRole == null)
                            throw new InvalidOperationException("Admin role not found.");
                        adminAccount.Roles.Add(adminRole);
                        _context.Accounts.Add(adminAccount);
                        _context.SaveChanges();

                        var admin = new Admin
                        {
                            FName = "Somaya",
                            LName = "Mohamed",
                            Gender = Gender.Female,
                            AccountId = adminAccount.Id,
                            LastActive = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Admins.Add(admin);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Teacher ---------------------
                    if (!_context.Teachers.Any(t => t.Account.Email == "mohamedSalah@gmail.com"))
                    {
                        var teacherAccount = new Account
                        {
                            Email = "mohamedSalah@gmail.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("PasswordMOHAMED!"),
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            FailedLoginAttempts = 0
                        };
                        var teacherRole = _context.Roles.FirstOrDefault(r => r.Name == "Teacher");
                        if (teacherRole == null)
                            throw new InvalidOperationException("Teacher role not found.");
                        teacherAccount.Roles.Add(teacherRole);
                        _context.Accounts.Add(teacherAccount);
                        _context.SaveChanges();

                        var teacher = new Teacher
                        {
                            FName = "محمد",
                            LName = "صلاح",
                            Gender = Gender.Male,
                            Age = 35,
                            AccountId = teacherAccount.Id,
                            LastActive = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Teachers.Add(teacher);
                        _context.SaveChanges();

                        var teacherProfile = new UserProfile
                        {
                            AccountId = teacherAccount.Id,
                            PhoneNumber = "01234567888",
                            Address = "حي الهرم، الجيزة",
                            City = "الجيزة",
                            ProfilePicture = "mohamed_salah.jpg"
                        };
                        _context.UserProfiles.Add(teacherProfile);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Level (1) ---------------------
                    if (!_context.Levels.Any(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027"))
                    {
                        var newLevel = new Level
                        {
                            LevelNumber = 1,
                            NumberOfStudents = 0,
                            AcademicYear = "2026-2027",
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Levels.Add(newLevel);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Course 1 ---------------------
                    if (!_context.Courses.Any(c => c.Title == "الترم الأول - أولى ثانوي - الشهر الأول"))
                    {
                        var level1 = _context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027");
                        if (level1 == null)
                        {
                            throw new InvalidOperationException("Level 1 for 2026-2027 not found.");
                        }
                        var newCourse = new Course
                        {
                            Title = "الترم الأول - أولى ثانوي - الشهر الأول",
                            LevelFK = level1.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Courses.Add(newCourse);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Course 2 ---------------------
                    if (!_context.Courses.Any(c => c.Title == "الترم الأول - أولى ثانوي - الشهر الثاني"))
                    {
                        var level1 = _context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027");
                        if (level1 == null)
                        {
                            throw new InvalidOperationException("Level 1 for 2026-2027 not found.");
                        }
                        var newCourse = new Course
                        {
                            Title = "الترم الأول - أولى ثانوي - الشهر الثاني",
                            LevelFK = level1.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Courses.Add(newCourse);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Lessons for Course 1 ---------------------
                    var course1 = _context.Courses.FirstOrDefault(c => c.Title == "الترم الأول - أولى ثانوي - الشهر الأول");
                    if (course1 != null)
                    {
                        if (!_context.Lessons.Any(l => l.Title == "كان التامة _ الفرق بين كان الناقصة والتامة في دقايق"))
                        {
                            var videos1 = new List<string> { "https://youtu.be/video1", "https://youtu.be/video2" };
                            var lesson1 = new Lesson
                            {
                                Title = "كان التامة _ الفرق بين كان الناقصة والتامة في دقايق",
                                Description = "شرح الفرق بين كان الناقصة والتامة",
                                ImageName = "صورة1.jpg",
                                VideoName = videos1,
                                DocName = null,
                                CourseIdFK = course1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson1);
                            _context.SaveChanges();
                        }

                        if (!_context.Lessons.Any(l => l.Title == "إعمال اسم الفاعل _ تعلم الإعراب بسهولة"))
                        {
                            var videos2 = new List<string> { "https://youtu.be/video3", "https://youtu.be/video4" };
                            var lesson2 = new Lesson
                            {
                                Title = "إعمال اسم الفاعل _ تعلم الإعراب بسهولة",
                                Description = "شرح إعمال اسم الفاعل",
                                ImageName = "صورة2.jpg",
                                VideoName = videos2,
                                DocName = null,
                                CourseIdFK = course1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson2);
                            _context.SaveChanges();
                        }

                        if (!_context.Lessons.Any(l => l.Title == "تعلم الإعراب بسهولة - كان وأخواتها"))
                        {
                            var videos3 = new List<string> { "https://youtu.be/video5", "https://youtu.be/video6" };
                            var lesson3 = new Lesson
                            {
                                Title = "تعلم الإعراب بسهولة - كان وأخواتها",
                                Description = "شرح كان وأخواتها",
                                ImageName = "صورة3.jpg",
                                VideoName = videos3,
                                DocName = null,
                                CourseIdFK = course1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson3);
                            _context.SaveChanges();
                        }
                    }

                    // --------------------- Add New Lessons for Course 2 ---------------------
                    var course2 = _context.Courses.FirstOrDefault(c => c.Title == "الترم الأول - أولى ثانوي - الشهر الثاني");
                    if (course2 != null)
                    {
                        if (!_context.Lessons.Any(l => l.Title == "المصادر"))
                        {
                            var videos4 = new List<string> { "https://youtu.be/QwY1iiEUSLg", "https://youtu.be/nNyWzsPNddk" };
                            var lesson4 = new Lesson
                            {
                                Title = "المصادر",
                                Description = "",
                                ImageName = "محمد صلاح.jpg",
                                VideoName = videos4,
                                DocName = null,
                                CourseIdFK = course2.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson4);
                            _context.SaveChanges();
                        }
                    }

                    // --------------------- Seeding Students ---------------------
                    var level = _context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027");
                    if (level == null)
                    {
                        throw new InvalidOperationException("Level 1 for 2026-2027 not found.");
                    }
                    if (!_context.Accounts.Any(a => a.Email == "ahmed.mohamed@example.com"))
                    {
                        var studentRole = _context.Roles.FirstOrDefault(r => r.Name == "Student");
                        if (studentRole == null)
                            throw new InvalidOperationException("Student role not found.");

                        var studentAccounts = new List<Account>
                        {
                            new Account { Email = "ahmed.mohamed@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "sarah.ali@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "mahmoud.khaled@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "laila.abdullah@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "youssef.ibrahim@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "nora.hassan@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "omar.saeed@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "huda.adel@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "karim.ahmed@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "reem.farouk@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } }
                        };

                        _context.Accounts.AddRange(studentAccounts);
                        _context.SaveChanges();

                        var students = new List<Student>
                        {
                            new Student { Age = 15, FName = "أحمد", LName = "محمد", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 9, ParentPhoneNumber = "01234567891", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[0].Id },
                            new Student { Age = 16, FName = "سارة", LName = "علي", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567893", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[1].Id },
                            new Student { Age = 17, FName = "محمود", LName = "خالد", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567895", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[2].Id },
                            new Student { Age = 15, FName = "ليلى", LName = "عبدالله", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 9, ParentPhoneNumber = "01234567897", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[3].Id },
                            new Student { Age = 16, FName = "يوسف", LName = "إبراهيم", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567899", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[4].Id },
                            new Student { Age = 17, FName = "نورا", LName = "حسن", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567901", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[5].Id },
                            new Student { Age = 15, FName = "عمر", LName = "سعيد", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 9, ParentPhoneNumber = "01234567903", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[6].Id },
                            new Student { Age = 16, FName = "هدى", LName = "عادل", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567905", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[7].Id },
                            new Student { Age = 17, FName = "كريم", LName = "أحمد", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567907", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[8].Id },
                            new Student { Age = 15, FName = "ريم", LName = "فاروق", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 9, ParentPhoneNumber = "01234567909", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level.Id, AccountId = studentAccounts[9].Id }
                        };

                        var studentProfiles = new List<UserProfile>
                        {
                            new UserProfile { AccountId = studentAccounts[0].Id, PhoneNumber = "01234567890", Address = "شارع المعاهد", City = "القاهرة", ProfilePicture = "ahmed_mohamed.jpg" },
                            new UserProfile { AccountId = studentAccounts[1].Id, PhoneNumber = "01234567892", Address = "حي الزهراء", City = "الإسكندرية", ProfilePicture = "sarah_ali.jpg" },
                            new UserProfile { AccountId = studentAccounts[2].Id, PhoneNumber = "01234567894", Address = "شارع الهرم", City = "الجيزة", ProfilePicture = "mahmoud_khaled.jpg" },
                            new UserProfile { AccountId = studentAccounts[3].Id, PhoneNumber = "01234567896", Address = "حي النصر", City = "القاهرة", ProfilePicture = "laila_abdullah.jpg" },
                            new UserProfile { AccountId = studentAccounts[4].Id, PhoneNumber = "01234567898", Address = "شارع الجمهورية", City = "الإسكندرية", ProfilePicture = "youssef_ibrahim.jpg" },
                            new UserProfile { AccountId = studentAccounts[5].Id, PhoneNumber = "01234567900", Address = "حي السلام", City = "القاهرة", ProfilePicture = "nora_hassan.jpg" },
                            new UserProfile { AccountId = studentAccounts[6].Id, PhoneNumber = "01234567902", Address = "شارع الهرم", City = "الجيزة", ProfilePicture = "omar_saeed.jpg" },
                            new UserProfile { AccountId = studentAccounts[7].Id, PhoneNumber = "01234567904", Address = "حي الزيتون", City = "القاهرة", ProfilePicture = "huda_adel.jpg" },
                            new UserProfile { AccountId = studentAccounts[8].Id, PhoneNumber = "01234567906", Address = "شارع السادات", City = "الإسكندرية", ProfilePicture = "karim_ahmed.jpg" },
                            new UserProfile { AccountId = studentAccounts[9].Id, PhoneNumber = "01234567908", Address = "حي المعادي", City = "القاهرة", ProfilePicture = "reem_farouk.jpg" }
                        };

                        level.NumberOfStudents = _context.Students.Count(s => s.levelFK == level.Id) + students.Count;
                        _context.Students.AddRange(students);
                        _context.UserProfiles.AddRange(studentProfiles);
                        _context.SaveChanges();
                    }

                    transaction.Commit();

                    // --------------------- Add New Announcements for Lesson 1, 2 & 3 in Course 1 ---------------------
                    var Lesson1 = _context.Lessons.FirstOrDefault(l => l.Title == "كان التامة _ الفرق بين كان الناقصة والتامة في دقايق");
                    if (Lesson1 != null && !Lesson1.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                                Header = "إعلان جديد: بدء الترم",
                                Body = "يرجى حضور الدرس الأول غدًا في الساعة 10 صباحًا.",
                                IsPinned = true,
                                LessonIdFK = Lesson1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "تغيير موعد الامتحان",
                               Body = "تم تغيير موعد الامتحان إلى يوم الإثنين الساعة 2 ظهرًا.",
                               IsPinned = false,
                               LessonIdFK = Lesson1.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    var Lesson2 = _context.Lessons.FirstOrDefault(l => l.Title == "إعمال اسم الفاعل _ تعلم الإعراب بسهولة");
                    if (Lesson2 != null && !Lesson2.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                                   Header = "دورة تدريبية مجانية",
                                   Body = "سوف نقيم دورة تدريبية حول القواعد يوم السبت.",
                                   IsPinned = true,
                                   LessonIdFK = Lesson2.Id,
                                   CreatedBy = 1,
                                   CreatedOn = DateTime.Now,
                                   LastModifiedBy = 1,
                                   LastModifiedOn = DateTime.Now,
                                   IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "إعدادات الواجب",
                               Body = "يرجى تقديم الواجب رقم 3 قبل يوم الخميس.",
                               IsPinned = false,
                               LessonIdFK = Lesson2.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           },
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    var Lesson3 = _context.Lessons.FirstOrDefault(l => l.Title == "تعلم الإعراب بسهولة - كان وأخواتها");
                    if (Lesson3 != null && !Lesson3.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                               Header = "موعد إضافي للدعم",
                               Body = "سيكون هناك موعد دعم إضافي يوم الأحد الساعة 3 عصرًا.",
                               IsPinned = false,
                               LessonIdFK = Lesson3.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "تنبيه هام",
                               Body = "يرجى مراجعة الفيديوهات قبل الامتحان القادم.",
                               IsPinned = true,
                               LessonIdFK = Lesson3.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Announcements for Lesson 4 in Course 2 ---------------------
                    var Lesson4 = _context.Lessons.FirstOrDefault(l => l.Title == "المصادر");
                    if (Lesson4 != null && !Lesson4.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                               Header = "موعد إضافي للدعم",
                               Body = "سيكون هناك موعد دعم إضافي يوم الأحد الساعة 3 عصرًا.",
                               IsPinned = false,
                               LessonIdFK = Lesson4.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "تنبيه هام",
                               Body = "يرجى مراجعة الفيديوهات قبل الامتحان القادم.",
                               IsPinned = true,
                               LessonIdFK = Lesson4.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Exams for Lesson 1, 2 & 3 in Course 1 ---------------------
                    if (Lesson1 != null && !Lesson1.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار كان التامة",
                            Description = "اختبار لدرس كان التامة _ الفرق بين كان الناقصة والتامة.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson1.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.Add(exam);
                        _context.SaveChanges();
                    }
                    if (Lesson2 != null && !Lesson2.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار إعمال اسم الفاعل",
                            Description = "اختبار لدرس إعمال اسم الفاعل.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson2.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.Add(exam);
                        _context.SaveChanges();
                    }
                    if (Lesson3 != null && !Lesson3.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار كان وأخواتها",
                            Description = "اختبار لدرس تعلم الإعراب بسهولة - كان وأخواتها.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson3.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.Add(exam);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Exams for Lesson 4 in Course 2 ---------------------
                    if (Lesson4 != null && !Lesson4.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار المصادر",
                            Description = "اختبار لدرس المصادر.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson4.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.Add(exam);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam lesson 1 ---------------------
                    var exam1 = _context.Exams.FirstOrDefault(e => e.Title == "اختبار كان التامة");
                    if (exam1 != null && !exam1.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هو الفرق بين كان الناقصة والتامة؟",
                               Body = "حدد الفرق الرئيسي.",
                               Mark = 5,
                               Type = QuestionType.MCQ,
                               ExamId = exam1.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "كان التامة لا ترفع الخبر", IsCorrect = true },
                                   new QuestionOptions { OptionText = "كان الناقصة ترفع الخبر", IsCorrect = false },
                                   new QuestionOptions { OptionText = "كلاهما متشابهان", IsCorrect = false },
                                   new QuestionOptions { OptionText = "لا فرق", IsCorrect = false },
                                   new QuestionOptions { OptionText = "كان التامة تنصب الخبر", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "كان التامة تعمل عمل الفعل الماضي الكامل.",
                               Mark = 3,
                               Type = QuestionType.TrueFalse,
                               ExamId = exam1.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam lesson 2 ---------------------
                    var exam2 = _context.Exams.FirstOrDefault(e => e.Title == "اختبار إعمال اسم الفاعل");
                    if (exam2 != null && !exam2.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هي إعمال اسم الفاعل؟",
                               Body = "حدد الإعراب الصحيح.",
                               Mark = 5,
                               Type = QuestionType.MCQ,
                               ExamId = exam2.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "يرفع الفاعل وينصب المفعول", IsCorrect = true },
                                   new QuestionOptions { OptionText = "ينصب الفاعل", IsCorrect = false },
                                   new QuestionOptions { OptionText = "يجر الفاعل", IsCorrect = false },
                                   new QuestionOptions { OptionText = "لا يعمل", IsCorrect = false },
                                   new QuestionOptions { OptionText = "يرفع المفعول", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "اسم الفاعل يعمل عمل الفعل في الإعراب.",
                               Mark = 3,
                               Type = QuestionType.TrueFalse,
                               ExamId = exam2.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };

                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam lesson 3 ---------------------
                    var exam3 = _context.Exams.FirstOrDefault(e => e.Title == "اختبار كان وأخواتها");
                    if (exam3 != null && !exam3.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هي كان وأخواتها؟",
                               Body = "حدد أخوات كان.",
                               Mark = 5,
                               Type = QuestionType.MCQ,
                               ExamId = exam3.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "أصبح، صار، ظل", IsCorrect = true },
                                   new QuestionOptions { OptionText = "أن، لن", IsCorrect = false },
                                   new QuestionOptions { OptionText = "إن، لا", IsCorrect = false },
                                   new QuestionOptions { OptionText = "قد، حتى", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "كان وأخواتها ترفع الاسم وتنصب الخبر.",
                               Mark = 3,
                               Type = QuestionType.TrueFalse,
                               ExamId = exam3.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam lesson 4 ---------------------
                    var exam4 = _context.Exams.FirstOrDefault(e => e.Title == "اختبار المصادر");
                    if (exam4 != null && !exam4.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هي المصادر؟",
                               Body = "حدد جميع الأمثلة على المصادر.",
                               Mark = 5,
                               Type = QuestionType.MCQ,
                               ExamId = exam4.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "الكتابة، القراءة", IsCorrect = true },
                                   new QuestionOptions { OptionText = "الطالب يقرأ", IsCorrect = false },
                                   new QuestionOptions { OptionText = "المعلم الذكي", IsCorrect = false },
                                   new QuestionOptions { OptionText = "القلم الأحمر", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "المصدر هو اسم يدل على الحدث دون زمن.",
                               Mark = 3,
                               Type = QuestionType.TrueFalse,
                               ExamId = exam4.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error at seeding FirstYearData: {ex.Message}\n{ex.InnerException?.Message}");
                    throw;
                }
            }
        }



        public void AddSecondYearData()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // --------------------- Add Roles (إذا لم يتم إضافتها بعد) ---------------------
                    if (!_context.Roles.Any())
                    {
                        var roles = new List<Role>
                        {
                            new Role { Name = "Admin" },
                            new Role { Name = "Teacher" },
                            new Role { Name = "Student" }
                        };
                        _context.Roles.AddRange(roles);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Admin ---------------------
                    if (!_context.Admins.Any(a => a.Account.Email == "alaa.ali@gmail.com"))
                    {
                        var adminAccount = new Account
                        {
                            Email = "alaa.ali@gmail.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("PasswordALAA!"),
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            FailedLoginAttempts = 0
                        };
                        var adminRole = _context.Roles.FirstOrDefault(r => r.Name == "Admin");
                        if (adminRole == null)
                            throw new InvalidOperationException("Admin role not found.");
                        adminAccount.Roles.Add(adminRole);
                        _context.Accounts.Add(adminAccount);
                        _context.SaveChanges();

                        var admin = new Admin
                        {
                            FName = "Alaa",
                            LName = "Ali",
                            Gender = Gender.Female,
                            AccountId = adminAccount.Id,
                            LastActive = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Admins.Add(admin);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Level (2) ---------------------
                    if (!_context.Levels.Any(l => l.LevelNumber == 2 && l.AcademicYear == "2026-2027"))
                    {
                        var newLevel = new Level
                        {
                            LevelNumber = 2,
                            NumberOfStudents = 0,
                            AcademicYear = "2026-2027",
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Levels.Add(newLevel);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Course 1 ---------------------
                    if (!_context.Courses.Any(c => c.Title == "الترم الأول - تانية ثانوي - الشهر الأول"))
                    {
                        var level = _context.Levels.FirstOrDefault(l => l.LevelNumber == 2 && l.AcademicYear == "2026-2027");
                        if (level == null)
                        {
                            throw new InvalidOperationException("Level 2 for 2026-2027 not found.");
                        }
                        var newCourse = new Course
                        {
                            Title = "الترم الأول - تانية ثانوي - الشهر الأول",
                            LevelFK = level.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Courses.Add(newCourse);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Course 2 ---------------------
                    if (!_context.Courses.Any(c => c.Title == "الترم الأول - تانية ثانوي - الشهر الثاني"))
                    {
                        var level = _context.Levels.FirstOrDefault(l => l.LevelNumber == 2 && l.AcademicYear == "2026-2027");
                        if (level == null)
                        {
                            throw new InvalidOperationException("Level 2 for 2026-2027 not found.");
                        }
                        var newCourse = new Course
                        {
                            Title = "الترم الأول - تانية ثانوي - الشهر الثاني",
                            LevelFK = level.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Courses.Add(newCourse);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Lessons for Course 1 ---------------------
                    var course1 = _context.Courses.FirstOrDefault(c => c.Title == "الترم الأول - تانية ثانوي - الشهر الأول");
                    if (course1 != null)
                    {
                        if (!_context.Lessons.Any(l => l.Title == "أدوات نصب الفعل المضارع بطريقة ممتعة"))
                        {
                            var videos1 = new List<string> { "https://youtu.be/EBzmsWWFQ3Q", "https://youtu.be/glx0RZiuvcM" };
                            var lesson1 = new Lesson
                            {
                                Title = "أدوات نصب الفعل المضارع بطريقة ممتعة",
                                Description = "شرح أدوات نصب الفعل المضارع",
                                ImageName = "صورة4.jpg",
                                VideoName = videos1,
                                DocName = null,
                                CourseIdFK = course1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson1);
                            _context.SaveChanges();
                        }

                        if (!_context.Lessons.Any(l => l.Title == "التوكيد"))
                        {
                            var videos2 = new List<string> { "https://youtu.be/yNdhG3EGST4", "https://youtu.be/QwY1iiEUSLg" };
                            var lesson2 = new Lesson
                            {
                                Title = "التوكيد",
                                Description = "شرح التوكيد",
                                ImageName = "صورة5.jpg",
                                VideoName = videos2,
                                DocName = null,
                                CourseIdFK = course1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson2);
                            _context.SaveChanges();
                        }
                    }

                    // --------------------- Add New Lessons for Course 2 ---------------------
                    var course2 = _context.Courses.FirstOrDefault(c => c.Title == "الترم الأول - تانية ثانوي - الشهر الثاني");
                    if (course2 != null)
                    {
                        if (!_context.Lessons.Any(l => l.Title == "النعت"))
                        {
                            var videos3 = new List<string> { "https://youtu.be/nNyWzsPNddk", "https://youtu.be/123456789" };
                            var lesson3 = new Lesson
                            {
                                Title = "النعت",
                                Description = "شرح النعت",
                                ImageName = "صورة6.jpg",
                                VideoName = videos3,
                                DocName = null,
                                CourseIdFK = course2.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.Lessons.Add(lesson3);
                            _context.SaveChanges();
                        }
                    }

                    // --------------------- Seeding Students ---------------------
                    var level2 = _context.Levels.FirstOrDefault(l => l.LevelNumber == 2 && l.AcademicYear == "2026-2027");
                    if (level2 == null)
                    {
                        throw new InvalidOperationException("Level 2 for 2026-2027 not found.");
                    }
                    if (!_context.Accounts.Any(a => a.Email == "mohamed.adel@example.com"))
                    {
                        var studentRole = _context.Roles.FirstOrDefault(r => r.Name == "Student");
                        if (studentRole == null)
                            throw new InvalidOperationException("Student role not found.");

                        var studentAccounts = new List<Account>
                        {
                            new Account { Email = "mohamed.adel@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "mona.ibrahim@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "khaled.samir@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "zeinab.mohamed@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "ali.hussein@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "nadia.abdelrahman@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "amir.saeed@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "somaya.gamal@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "islam.mahmoud@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } },
                            new Account { Email = "rana.saleh@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123!"), IsActive = true, CreatedOn = DateTime.Now, FailedLoginAttempts = 0, Roles = new List<Role> { studentRole } }
                        };

                        _context.Accounts.AddRange(studentAccounts);
                        _context.SaveChanges();

                        var students = new List<Student>
                        {
                            new Student { Age = 16, FName = "محمد", LName = "عادل", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567911", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[0].Id },
                            new Student { Age = 17, FName = "منى", LName = "إبراهيم", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567913", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[1].Id },
                            new Student { Age = 16, FName = "خالد", LName = "سمير", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567915", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[2].Id },
                            new Student { Age = 17, FName = "زينب", LName = "محمد", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567917", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[3].Id },
                            new Student { Age = 16, FName = "علي", LName = "حسين", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567919", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[4].Id },
                            new Student { Age = 17, FName = "نادية", LName = "عبدالرحمن", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567921", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[5].Id },
                            new Student { Age = 16, FName = "أمير", LName = "سعيد", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567923", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[6].Id },
                            new Student { Age = 17, FName = "سمية", LName = "جمال", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567925", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[7].Id },
                            new Student { Age = 16, FName = "إسلام", LName = "محمود", Gender = Gender.Male, LastActive = DateTime.Now, IsDeleted = false, Grade = 10, ParentPhoneNumber = "01234567927", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[8].Id },
                            new Student { Age = 17, FName = "رنا", LName = "صالح", Gender = Gender.Female, LastActive = DateTime.Now, IsDeleted = false, Grade = 11, ParentPhoneNumber = "01234567929", CreatedBy = 2, CreatedOn = DateTime.Now, LastModifiedOn = DateTime.Now, levelFK = level2.Id, AccountId = studentAccounts[9].Id }
                        };

                        var studentProfiles = new List<UserProfile>
                        {
                            new UserProfile { AccountId = studentAccounts[0].Id, PhoneNumber = "01234567890", Address = "شارع المعاهد", City = "القاهرة", ProfilePicture = "ahmed_mohamed.jpg" },
                            new UserProfile { AccountId = studentAccounts[1].Id, PhoneNumber = "01234567892", Address = "حي الزهراء", City = "الإسكندرية", ProfilePicture = "sarah_ali.jpg" },
                            new UserProfile { AccountId = studentAccounts[2].Id, PhoneNumber = "01234567894", Address = "شارع الهرم", City = "الجيزة", ProfilePicture = "mahmoud_khaled.jpg" },
                            new UserProfile { AccountId = studentAccounts[3].Id, PhoneNumber = "01234567896", Address = "حي النصر", City = "القاهرة", ProfilePicture = "laila_abdullah.jpg" },
                            new UserProfile { AccountId = studentAccounts[4].Id, PhoneNumber = "01234567898", Address = "شارع الجمهورية", City = "الإسكندرية", ProfilePicture = "youssef_ibrahim.jpg" },
                            new UserProfile { AccountId = studentAccounts[5].Id, PhoneNumber = "01234567900", Address = "حي السلام", City = "القاهرة", ProfilePicture = "nora_hassan.jpg" },
                            new UserProfile { AccountId = studentAccounts[6].Id, PhoneNumber = "01234567902", Address = "شارع الهرم", City = "الجيزة", ProfilePicture = "omar_saeed.jpg" },
                            new UserProfile { AccountId = studentAccounts[7].Id, PhoneNumber = "01234567904", Address = "حي الزيتون", City = "القاهرة", ProfilePicture = "huda_adel.jpg" },
                            new UserProfile { AccountId = studentAccounts[8].Id, PhoneNumber = "01234567906", Address = "شارع السادات", City = "الإسكندرية", ProfilePicture = "karim_ahmed.jpg" },
                            new UserProfile { AccountId = studentAccounts[9].Id, PhoneNumber = "01234567908", Address = "حي المعادي", City = "القاهرة", ProfilePicture = "reem_farouk.jpg" }
                        };

                        level2.NumberOfStudents = _context.Students.Count(s => s.levelFK == level2.Id) + students.Count;
                        _context.Students.AddRange(students);
                        _context.UserProfiles.AddRange(studentProfiles);
                        _context.SaveChanges();
                    }

                    transaction.Commit();

                    // --------------------- Add New Announcements for Lesson 1 & 2 in Course 1 ---------------------
                    var Lesson1 = _context.Lessons.FirstOrDefault(l => l.Title == "أدوات نصب الفعل المضارع بطريقة ممتعة");
                    if (Lesson1 != null && !Lesson1.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                                Header = "إعلان جديد: بدء الترم",
                                Body = "يرجى حضور الدرس الأول غدًا في الساعة 10 صباحًا.",
                                IsPinned = true,
                                LessonIdFK = Lesson1.Id,
                                CreatedBy = 1,
                                CreatedOn = DateTime.Now,
                                LastModifiedBy = 1,
                                LastModifiedOn = DateTime.Now,
                                IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "تغيير موعد الامتحان",
                               Body = "تم تغيير موعد الامتحان إلى يوم الإثنين الساعة 2 ظهرًا.",
                               IsPinned = false,
                               LessonIdFK = Lesson1.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    var Lesson2 = _context.Lessons.FirstOrDefault(l => l.Title == "التوكيد");
                    if (Lesson2 != null && !Lesson2.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                                   Header = "دورة تدريبية مجانية",
                                   Body = "سوف نقيم دورة تدريبية حول القواعد يوم السبت.",
                                   IsPinned = true,
                                   LessonIdFK = Lesson2.Id,
                                   CreatedBy = 1,
                                   CreatedOn = DateTime.Now,
                                   LastModifiedBy = 1,
                                   LastModifiedOn = DateTime.Now,
                                   IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "إعدادات الواجب",
                               Body = "يرجى تقديم الواجب رقم 3 قبل يوم الخميس.",
                               IsPinned = false,
                               LessonIdFK = Lesson2.Id,
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           },
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Announcements for Lesson 3 in Course 2 ---------------------
                    var Lesson3 = _context.Lessons.FirstOrDefault(l => l.Title == "النعت");
                    if (Lesson3 != null && !Lesson3.announcements.Any())
                    {
                        var announcements = new List<Announcement>()
                        {
                           new Announcement
                           {
                               Header = "موعد إضافي للدعم",
                               Body = "سيكون هناك موعد دعم إضافي يوم الأحد الساعة 3 عصرًا.",
                               IsPinned = false,
                               LessonIdFK = Lesson3.Id, // ربط بالدرس الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           },
                           new Announcement
                           {
                               Header = "تنبيه هام",
                               Body = "يرجى مراجعة الفيديوهات قبل الامتحان القادم.",
                               IsPinned = true,
                               LessonIdFK = Lesson3.Id, // ربط بالدرس الثاني
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Announcements.AddRange(announcements);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Exams for Lesson 1 & 2 in Course 1 ---------------------
                    if (Lesson1 != null && !Lesson1.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار أدوات نصب الفعل",
                            Description = "اختبار لدرس أدوات نصب الفعل المضارع.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson1.Id, // ربط بالدرس الأول
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.AddRange(exam);
                        _context.SaveChanges();
                    }
                    if (Lesson2 != null && !Lesson2.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار التوكيد",
                            Description = "اختبار التوكيد.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson2.Id, // ربط بالدرس الأول
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.AddRange(exam);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Exams for Lesson 3 in Course 2 ---------------------
                    if (Lesson3 != null && !Lesson3.exams.Any())
                    {
                        var exam = new Exam
                        {
                            Duration = 60, // مدة بالدقائق
                            Title = "اختبار النعت",
                            Description = "اختبار لدرس النعت.",
                            StartTime = DateTime.Now.AddDays(1), // غدًا
                            EndTime = DateTime.Now.AddDays(1).AddHours(1), // بعد ساعة من البداية
                            IsCompleted = false,
                            Status = null,
                            IsAvaliable = true,
                            LessonId = Lesson3.Id, // ربط بالدرس الأول
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            LastModifiedBy = 1,
                            LastModifiedOn = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Exams.AddRange(exam);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam  lesson 1 ---------------------
                    var exam1 = _context.Exams.FirstOrDefault(e => e.Title == "اختبار أدوات نصب الفعل");
                    if (exam1 != null && !exam1.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هي أدوات نصب الفعل؟",
                               Body = "حدد جميع أدوات نصب الفعل المضارع.",
                               Mark = 5,
                               Type = QuestionType.MCQ,
                               ExamId = exam1.Id, // ربط بالاختبار الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "أن", IsCorrect = true },
                                   new QuestionOptions { OptionText = "لن", IsCorrect = false },
                                   new QuestionOptions { OptionText = "كي", IsCorrect = false },
                                   new QuestionOptions { OptionText = "لما", IsCorrect = false },
                                   new QuestionOptions { OptionText = "حتى", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "الفعل المضارع ينصب بـ 'أن'.",
                               Mark = 3,
                               Type = QuestionType.TrueFalse, // افتراضي
                               ExamId = exam1.Id, // ربط بالاختبار الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam  lesson 2 ---------------------
                    var exam2 = _context.Exams.FirstOrDefault(e => e.Title == "التوكيد");
                    if (exam2 != null && !exam2.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هي أدوات التوكيد؟",
                               Body = "حدد جميع أدوات التوكيد.",
                               Mark = 5,
                               Type = QuestionType.MCQ, // افتراضي، استبدله حسب enum الخاص بك
                               ExamId = exam2.Id, // ربط بالاختبار الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "قد", IsCorrect = true },
                                   new QuestionOptions { OptionText = "لن", IsCorrect = false },
                                   new QuestionOptions { OptionText = "إن", IsCorrect = false },
                                   new QuestionOptions { OptionText = "لا", IsCorrect = false },
                                   new QuestionOptions { OptionText = "أكد", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "يا اداه توكيد ؟",
                               Mark = 3,
                               Type = QuestionType.TrueFalse, // افتراضي
                               ExamId = exam2.Id, // ربط بالاختبار الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }

                    // --------------------- Add Questions for exam  lesson 3 ---------------------
                    var exam3 = _context.Exams.FirstOrDefault(e => e.Title == "النعت");
                    if (exam3 != null && !exam3.questions.Any())
                    {
                        var questions = new List<Question>()
                        {
                           new Question
                           {
                               Header = "ما هو النعت ؟",
                               Body = "حدد جميع الجمل التى تحتوى ع نعت.",
                               Mark = 5,
                               Type = QuestionType.MCQ, // افتراضي، استبدله حسب enum الخاص بك
                               ExamId = exam3.Id, // ربط بالاختبار الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false,
                               Options = new HashSet<QuestionOptions>
                               {
                                   new QuestionOptions { OptionText = "الكتاب الجديد", IsCorrect = true },
                                   new QuestionOptions { OptionText = "الطالب يقرأ", IsCorrect = false },
                                   new QuestionOptions { OptionText = "المعلم الذكي", IsCorrect = false },
                                   new QuestionOptions { OptionText = "القلم الأحمر", IsCorrect = false }
                               }
                           },
                           new Question
                           {
                               Header = "هل الجملة صحيحة؟",
                               Body = "هل تحتوى الجمله ع نعت القطه جميله ",
                               Mark = 3,
                               Type = QuestionType.TrueFalse, // افتراضي
                               ExamId = exam3.Id, // ربط بالاختبار الأول
                               CreatedBy = 1,
                               CreatedOn = DateTime.Now,
                               LastModifiedBy = 1,
                               LastModifiedOn = DateTime.Now,
                               IsDeleted = false
                           }
                        };
                        _context.Questions.AddRange(questions);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }

            
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error at seeding FirstYearData: {ex.Message}\n{ex.InnerException?.Message}");
                    throw;
                }
            }
        }

    }
}


