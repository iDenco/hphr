using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.Models
{
  public class EmployeeStatus
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(15)]
    public string Code { get; set; }

    [MaxLength(15)]
    public string Description { get; set; }

    [DefaultValue(true)]
    public bool IsEnabled { get; set; }
  }
}
