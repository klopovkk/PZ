using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ.DAL.Entities
{
    [Table("employees")]
    public class Employee : BaseEntity
    {
        [Required]
        [Column("full_name")]
        public string FullName { get; set; }

        [Required]
        [Column("password)")]
        public string Password { get; set; }

        [Required]
        [Column("email)")]
        public string Email { get; set; }

        [Column("role_id)")]
        public Guid? RoleId;
        [NotMapped]
        public Role? Role { get; set; }
    }
}
