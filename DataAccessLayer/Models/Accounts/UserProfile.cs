using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Accounts
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ProfilePicture { get; set; }
        public int AccountId { get; set; } // Foreign Key
        public Account Account { get; set; } // Navigation Property
    }

}

