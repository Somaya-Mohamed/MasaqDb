
using BCrypt.Net;
using DataAccessLayer.Contracts;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Admin;
using DataAccessLayer.Models.Contents.Courses;
using DataAccessLayer.Models.Contents.Lessons;
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
                    // --------------------- Add New Admin ---------------------
                    if (!_context.Admins.Any(a => a.email == "somaya.mohamed@gmail.com"))
                    {
                        var admin = new Admin
                        {
                            FName = "Somaya",
                            LName = "Mohamed",
                            Gender = Gender.Female,
                            email = "somaya.mohamed@gmail.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("PasswordSOMAYA!")
                        };
                        _context.Admins.Add(admin);
                        _context.SaveChanges();
                    }

                    // --------------------- Add New Teacher ---------------------
                    if (!_context.Teachers.Any(t => t.email == "mohamedSalah@gmail.com"))
                    {
                        var teacher = new Teacher
                        {
                            FName = "محمد",
                            LName = "صلاح",
                            Gender = Gender.Male,
                            email = "mohamedSalah@gmail.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("PasswordMOHAMED!"),
                            Age = 35,
                            Address = "حي الهرم، الجيزة",
                            City = "الجيزة",
                            LastActive = DateTime.Now,
                            IsDeleted = false
                        };
                        _context.Teachers.Add(teacher);
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
                        if (_context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027") == null)
                        {
                            throw new InvalidOperationException("Level 1 for 2026-2027 not found.");
                        }
                        var newCourse = new Course
                        {
                            Title = "الترم الأول - أولى ثانوي - الشهر الأول",
                            LevelFK = _context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027").Id,
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
                        if (_context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027") == null)
                        {
                            throw new InvalidOperationException("Level 1 for 2026-2027 not found.");
                        }
                        var newCourse = new Course
                        {
                            Title = "الترم الأول - أولى ثانوي - الشهر الثاني",
                            LevelFK = _context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027").Id,
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
                    if (course2 != null && !_context.Lessons.Any(l => l.Title == "المصادر"))
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

                    // --------------------- Seeding Students ---------------------
                    var level = _context.Levels.FirstOrDefault(l => l.LevelNumber == 1 && l.AcademicYear == "2026-2027");
                    if (level == null)
                    {
                        throw new InvalidOperationException("Level 1 for 2026-2027 not found.");
                    }
                    if (!_context.Students.Any(s => s.email == "ahmed.mohamed@example.com"))
                    {
                        var students = new List<Student>
                        {
                            new Student { Age = 15, FName = "أحمد", LName = "محمد", Gender = Gender.Male, email = "ahmed.mohamed@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع المعاهد", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 9, PhoneNumber = "01234567890", ParentPhoneNumber = "01234567891", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 16, FName = "سارة", LName = "علي", Gender = Gender.Female, email = "sarah.ali@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي الزهراء", City = "الإسكندرية", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567892", ParentPhoneNumber = "01234567893", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 17, FName = "محمود", LName = "خالد", Gender = Gender.Male, email = "mahmoud.khaled@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع الهرم", City = "الجيزة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567894", ParentPhoneNumber = "01234567895", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 15, FName = "ليلى", LName = "عبدالله", Gender = Gender.Female, email = "laila.abdullah@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي النصر", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 9, PhoneNumber = "01234567896", ParentPhoneNumber = "01234567897", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 16, FName = "يوسف", LName = "إبراهيم", Gender = Gender.Male, email = "youssef.ibrahim@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع الجمهورية", City = "الإسكندرية", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567898", ParentPhoneNumber = "01234567899", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 17, FName = "نورا", LName = "حسن", Gender = Gender.Female, email = "nora.hassan@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي السلام", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567900", ParentPhoneNumber = "01234567901", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 15, FName = "عمر", LName = "سعيد", Gender = Gender.Male, email = "omar.saeed@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع الهرم", City = "الجيزة", LastActive = DateTime.Now, IsDeleted = false, Grade = 9, PhoneNumber = "01234567902", ParentPhoneNumber = "01234567903", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 16, FName = "هدى", LName = "عادل", Gender = Gender.Female, email = "huda.adel@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي الزيتون", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567904", ParentPhoneNumber = "01234567905", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 17, FName = "كريم", LName = "أحمد", Gender = Gender.Male, email = "karim.ahmed@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع السادات", City = "الإسكندرية", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567906", ParentPhoneNumber = "01234567907", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id },
                            new Student { Age = 15, FName = "ريم", LName = "فاروق", Gender = Gender.Female, email = "reem.farouk@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي المعادي", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 9, PhoneNumber = "01234567908", ParentPhoneNumber = "01234567909", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level.Id }
                        };

                        level.NumberOfStudents = _context.Students.Count(s => s.levelFK == level.Id) + students.Count;
                        _context.Students.AddRange(students);
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
                    // --------------------- Add New Admin ---------------------
                    if (!_context.Admins.Any(a => a.email == "alaa.ali@gmail.com"))
                    {
                        var admin = new Admin
                        {
                            FName = "Alaa",
                            LName = "Ali",
                            Gender = Gender.Female,
                            email = "alaa.ali@gmail.com",
                            Password = BCrypt.Net.BCrypt.HashPassword("PasswordALAA!")
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
                    if (course2 != null && !_context.Lessons.Any(l => l.Title == "النعت"))
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

                    // --------------------- Seeding Students ---------------------
                    var level2 = _context.Levels.FirstOrDefault(l => l.LevelNumber == 2 && l.AcademicYear == "2026-2027");
                    if (level2 == null)
                    {
                        throw new InvalidOperationException("Level 2 for 2026-2027 not found.");
                    }
                    if (!_context.Students.Any(s => s.email == "mohamed.adel@example.com"))
                    {
                        var students = new List<Student>
                        {
                            new Student { Age = 16, FName = "محمد", LName = "عادل", Gender = Gender.Male, email = "mohamed.adel@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع التحرير", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567910", ParentPhoneNumber = "01234567911", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 17, FName = "منى", LName = "إبراهيم", Gender = Gender.Female, email = "mona.ibrahim@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي المنيل", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567912", ParentPhoneNumber = "01234567913", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 16, FName = "خالد", LName = "سمير", Gender = Gender.Male, email = "khaled.samir@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع الجمهورية", City = "الإسكندرية", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567914", ParentPhoneNumber = "01234567915", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 17, FName = "زينب", LName = "محمد", Gender = Gender.Female, email = "zeinab.mohamed@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي الدقي", City = "الجيزة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567916", ParentPhoneNumber = "01234567917", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 16, FName = "علي", LName = "حسين", Gender = Gender.Male, email = "ali.hussein@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع فيصل", City = "الجيزة", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567918", ParentPhoneNumber = "01234567919", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 17, FName = "نادية", LName = "عبدالرحمن", Gender = Gender.Female, email = "nadia.abdelrahman@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي شبرا", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567920", ParentPhoneNumber = "01234567921", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 16, FName = "أمير", LName = "سعيد", Gender = Gender.Male, email = "amir.saeed@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع الهرم", City = "الجيزة", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567922", ParentPhoneNumber = "01234567923", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 17, FName = "سمية", LName = "جمال", Gender = Gender.Female, email = "somaya.gamal@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي العجوزة", City = "الجيزة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567924", ParentPhoneNumber = "01234567925", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 16, FName = "إسلام", LName = "محمود", Gender = Gender.Male, email = "islam.mahmoud@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "شارع المنصورة", City = "المنصورة", LastActive = DateTime.Now, IsDeleted = false, Grade = 10, PhoneNumber = "01234567926", ParentPhoneNumber = "01234567927", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id },
                            new Student { Age = 17, FName = "رنا", LName = "صالح", Gender = Gender.Female, email = "rana.saleh@example.com", Password = BCrypt.Net.BCrypt.HashPassword("Password123!"), Address = "حي المعادي", City = "القاهرة", LastActive = DateTime.Now, IsDeleted = false, Grade = 11, PhoneNumber = "01234567928", ParentPhoneNumber = "01234567929", CreationBy = 2, CreatedOn = DateTime.Now, LastModified = DateTime.Now, levelFK = level2.Id }
                        };

                        level2.NumberOfStudents = _context.Students.Count(s => s.levelFK == level2.Id) + students.Count;
                        _context.Students.AddRange(students);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error at seeding SecondYearData: {ex.Message}\n{ex.InnerException?.Message}");
                    throw;
                }
            }
        }
    
    
    }
}