using HPHR.ApplicationCore.BaseTypes;using System;using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.Models{
  public class Employee : BaseModel  {
    [Key]
    public int Id { get; set; }

    public int ContactId { get; set; }
    public Contact Contact { get; set; }

    public int EmployeeTypeId { get; set; }
    public EmployeeType EmployeeType { get; set; }

    public int EmployeeStatusId { get; set; }
    public EmployeeStatus EmployeeStatus { get; set; }

    public int DesignationId { get; set; }
    public Designation Designation { get; set; }

    [DefaultValue(false)]
    public bool IsUnderProbation { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndedProbationAt { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime HiredAt { get; set; }

    [DataType(DataType.Date)]
    public DateTime? LeaveAt { get; set; }

    [MaxLength(150)]
    public string ABAAccountName { get; set; }

    [MaxLength(20)]
    public string ABAAccountNumber { get; set; }

    public string Remarks { get; set; }
  }}