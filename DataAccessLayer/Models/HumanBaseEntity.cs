//using DataAccessLayer.Models.Accounts;
//using DataAccessLayer.Models.Levels;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Security.Principal;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLayer.Models
//{

//    public abstract class HumanBaseEntity
//    {
//        public int Id { get; set; }
//        public int Age { get; set; }
//        public string FName { get; set; }
//        public string LName { get; set; }
//        public Gender Gender { get; set; }
//        public DateTime LastActive { get; set; }
//        public bool IsDeleted { get; set; } //Soft Delete

//        [InverseProperty(nameof(UserNotification.User))]
//        public ICollection<UserNotification> UserNotifications { get; set; } = new HashSet<UserNotification>();
//    }


//}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models.Accounts;
using DataAccessLayer.Models.Levels;

namespace DataAccessLayer.Models
{
    //public enum Gender
    //{
    //    Male,
    //    Female
    //}

    public abstract class HumanBaseEntity : BaseOfAllContentEntities
    {
        [Required]
        public int Age { get; set; }

        [Required, MaxLength(20)]
        public string FName { get; set; }

        [Required, MaxLength(20)]
        public string LName { get; set; }

        [Required, MaxLength(10)]
        public Gender Gender { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastActive { get; set; }


        public int? AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        public int? levelFK { get; set; }
        [ForeignKey(nameof(levelFK))]
        public Level Level { get; set; }

        [InverseProperty(nameof(UserNotification.User))]
        public ICollection<UserNotification> UserNotifications { get; set; } = new HashSet<UserNotification>();
    }
}