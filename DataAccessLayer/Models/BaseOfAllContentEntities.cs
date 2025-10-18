//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLayer.Models
//{
//    public class BaseOfAllContentEntities
//    {
//        public int Id { get; set; }//PK
//        public int CreatedBy { get; set; }// UserId
//        public DateTime? CreatedOn { get; set; }// Date
//        public int LastModifiedBy { get; set; }// UserId
//        public DateTime LastModifiedOn { get; set; }// Date
//        public bool IsDeleted { get; set; }// Soft Delete
//    }
//}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public abstract class BaseOfAllContentEntities
    {
        [Key]
        public int Id { get; set; } // PK

        [Required]
        public int CreatedBy { get; set; } // UserId

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedOn { get; set; } // Date, default GETDATE()

        [Required]
        public int LastModifiedBy { get; set; } // UserId

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastModifiedOn { get; set; } // Date, computed GETDATE()

        public bool IsDeleted { get; set; } = false; // Soft Delete
    }
}