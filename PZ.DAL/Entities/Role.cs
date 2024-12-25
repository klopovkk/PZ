using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZ.DAL.Entities;
[Table("administrator")]
public class Role : BaseEntity
{
    [Column("name)")]
    public string Name { get; set; }
    [Column("permissions)")]
    public string Permissions { get; set; }

    [NotMapped]
    public ICollection<Employee> Employees { get; set; }
}