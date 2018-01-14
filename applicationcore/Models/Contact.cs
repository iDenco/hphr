using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.Models
{
  public class Contact
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public string LastName { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string Phone { get; set; }

    public string MobilePhone { get; set; }

    [DataType(DataType.EmailAddress)]
    [StringLength(50, MinimumLength = 0)]
    public string Email { get; set; }

    [DataType(DataType.EmailAddress)]
    [StringLength(50, MinimumLength = 0)]
    public string OfficialEmail { get; set; }

    [StringLength(200, MinimumLength = 0)]
    public string PhysicalStreet { get; set; }

    [StringLength(70, MinimumLength = 0)]
    public string PhysicalState { get; set; }

    [StringLength(70, MinimumLength = 0)]
    public string PhysicalCity { get; set; }

    [StringLength(9, MinimumLength = 0)]
    public string PhysicalZipCode { get; set; }

    [StringLength(70, MinimumLength = 0)]
    public string PhysicalCountry { get; set; }
  }
}
