//using DataAccessLayer.Models.Contents.Answers;
//using DataAccessLayer.Models.Students;
//using DataAccessLayer.Models.Teachers;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLayer.Models.Notifications
//{
//    public class Notification: BaseOfAllContentEntities

//    {

//        public string Body { get; set; } = null!;
//        public string Header { get; set; }= null!;
       
//        //[InverseProperty(nameof(UserNotification.notification))]
//        //public ICollection<UserNotification> UserNotifications { get; set; } = new HashSet<UserNotification>();

//        public int TeacherFK { get; set; }
//        [ForeignKey(nameof(TeacherFK))]
//        [InverseProperty(nameof(Teacher.notifications))]
//        public Teacher teacher { get; set; } = null!;

//        public int StudentFK { get; set; }
//        [ForeignKey(nameof(StudentFK))]
//        [InverseProperty(nameof(Student.notifications))]
//        public Student student { get; set; } = null!;
//        public DateTime SentAt { get; set; } = DateTime.Now;
//        public bool IsRead { get; set; } = false;






//    }
//}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace DataAccessLayer.Models.Notifications
//{
//    public class Notification
//    {
//        public int Id { get; set; }

//        [Required]
//        [StringLength(600)]
//        public string Body { get; set; } = null!;

//        [Required]
//        [StringLength(50)]
//        public string Header { get; set; } = null!;

//        public DateTime SentAt { get; set; }
//        public bool IsRead { get; set; }
//        public int CreatedBy { get; set; }
//        public DateTime? CreatedOn { get; set; }
//        public int LastModifiedBy { get; set; }
//        public DateTime LastModifiedOn { get; set; }
//        public bool IsDeleted { get; set; }

//        public List<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
//    }
//}

using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Notifications
{
    public class Notification : BaseOfAllContentEntities
    {
        [Required]
        [StringLength(600)]
        public string Body { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Header { get; set; } = null!;

        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public List<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    }
}