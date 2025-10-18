using DataAccessLayer.Models.Accounts;
using DataAccessLayer.Models.Admins;
using DataAccessLayer.Models.Students;
using DataAccessLayer.Models.Teachers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Accounts
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }

        [Required, MaxLength(256)]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public DateTime? LastLogin { get; set; }

        public int FailedLoginAttempts { get; set; } = 0;

        public List<Role> Roles { get; set; } = new List<Role>(); // Many-to-Many

        public Admin Admin { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public UserProfile UserProfile { get; set; } // One-to-One
    }
}


