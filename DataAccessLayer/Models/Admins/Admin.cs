using DataAccessLayer.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Admins
{

    public class Admin
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public Gender Gender { get; set; }
        public int AccountId { get; set; } // Foreign Key
        public Account Account { get; set; } // Navigation Property
        public DateTime LastActive { get; set; }
        public bool IsDeleted { get; set; }
    }

}
