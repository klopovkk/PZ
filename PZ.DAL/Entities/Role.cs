using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZ.DAL.Entities;
[Table("administrator")]
public class Role : BaseEntity
{
    public string name { get; set; }

    [NotMapped]
    public ICollection<Employee> Employees { get; set; }
}