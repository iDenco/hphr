using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.Models
{
  public class EmployeeType
  {
    [Key]
    public int Id { get; set; }

    [StringLength(50), Required]
    public string Name { get; set; }

    public string Description { get; set; }
  }
}
