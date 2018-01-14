using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HPHR.ApplicationCore.DTOs
{
  public class InitEmployeeDto
  {
    public List<ListEmployeeStatusDto> Statuses { get; set; }
    public List<ListDesignationDto> Designations { get; set; }
    public List<ListEmployeeTypeDto> Types { get; set; }
  }

  public class BaseEmployeeDto
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string MobilePhone { get; set; }
    public string EmployeeType { get; set; }
    public string Designation { get; set; }
    public bool IsUnderProbation { get; set; } = false;
    public string EmployeeStatus { get; set; }
    public string Email { get; set; }
    public string OfficialEmail { get; set; }
  }

  public class EmployeeDto : BaseEmployeeDto
  {
    public int Id { get; set; }
    public string StatusColorClassName { get; set; }
    public string EmployeeTypeColorClassName { get; set; }
  }

  public class EmployeeDetailDto : BaseEmployeeDto
  {
    public int Id { get; set; }
    public string EndedProbationAt { get; set; }
    public string HiredAt { get; set; }
    public string LeaveAt { get; set; }
    public int EmployeeTypeId { get; set; }
    public int DesignationId { get; set; }
    public int EmployeeStatusId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string ABAAccountName { get; set; }
    public string ABAAccountNumber { get; set; }
    public string Remarks { get; set; }
    public string Address { get; set; }
    public string StatusColorClassName { get; set; }
    public string EmployeeTypeColorClassName { get; set; }
  }

  public class AddEmployeeDto : BaseEmployeeDto
  {
    public int EmployeeTypeId { get; set; }
    public int DesignationId { get; set; }
    public int EmployeeStatusId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string ABAAccountName { get; set; }
    public string ABAAccountNumber { get; set; }
    public string Remarks { get; set; }
  }

  public class UpdateEmployeeDto : AddEmployeeDto
  {
    public int Id { get; set; }
    public DateTime? EndedProbationAt { get; set; }
    public DateTime HiredAt { get; set; }
    public DateTime? LeaveAt { get; set; }
  }
}
