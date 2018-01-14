using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.BaseTypes
{
  public class BaseModel
  {
    [DefaultValue(false)]
    public bool IsDeleted { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [DataType(DataType.DateTime)]
    public DateTime? UpdatedDate { get; set; }
  }
}
