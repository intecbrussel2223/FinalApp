using System.ComponentModel.DataAnnotations;

namespace FinalApp.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
