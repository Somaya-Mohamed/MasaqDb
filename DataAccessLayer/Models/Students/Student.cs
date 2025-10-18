
using DataAccessLayer.Models.Accounts;
using DataAccessLayer.Models.Contents.Comments;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Notifications;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Students
{
    public class Student : HumanBaseEntity
    {
        [Required]
        public int Grade { get; set; }

        [Phone, MaxLength(15)]
        public string ParentPhoneNumber { get; set; } = null!;


        public int? Teacher_AccountId { get; set; }
        [ForeignKey(nameof(Teacher_AccountId))]
        public Account TeacherAccount { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<StudentExam> StudentExams { get; set; } = new HashSet<StudentExam>();
        public ICollection<StudentLesson> StudentLessons { get; set; } = new HashSet<StudentLesson>();
    }
}