using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPHR.ApplicationCore.Models
{
  [NotMapped]
  public class PhysicalAddress
  {
    [StringLength(200, MinimumLength = 0)]
    public String Street { get; set; }

    [StringLength(70, MinimumLength = 0)]
    public String State { get; set; }

    [StringLength(70, MinimumLength = 0)]
    public String City { get; set; }

    [StringLength(9, MinimumLength = 0)]
    public String ZipCode { get; set; }

    [StringLength(70, MinimumLength = 0)]
    public String Country { get; set; }
  }
}
