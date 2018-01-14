using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.Models
{
  public class Designation
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100), Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
