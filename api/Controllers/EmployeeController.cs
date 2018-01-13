using HPHR.ApplicationCore.DTOs;
using HPHR.ApplicationCore.Models;
using HPHR.ApplicationCore.Utils.Helpers;
using HPHR.Infrastructure.Data;
using HPHR.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPHR.API.Controllers
{
  [Route("api/[controller]")]
  public class EmployeesController : Controller
  {
    private readonly DefaultDbContext _context;
    private readonly ILogger<EmployeesController> _logger;
    public EmployeesController(DefaultDbContext context, ILogger<EmployeesController> logger)
    {
      _context = context;
      _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        var q = (from emp in _context.Employees
                 join contact in _context.Contacts on emp.ContactId equals contact.Id
                 join empType in _context.EmployeeTypes on emp.EmployeeTypeId equals empType.Id
                 join designation in _context.Designations on emp.DesignationId equals designation.Id
                 join status in _context.EmployeeStatuses on emp.EmployeeStatusId equals status.Id
                 where status.Code != Constants.DELETED
                 orderby contact.LastName
                 select new EmployeeDto()
                 {
                   Id = emp.Id,
                   FirstName = contact.FirstName,
                   LastName = contact.LastName,
                   Email = contact.Email,
                   OfficialEmail = contact.OfficialEmail,
                   Phone = contact.Phone,
                   MobilePhone = contact.MobilePhone,
                   IsUnderProbation = emp.IsUnderProbation,
                   EmployeeType = empType.Name,
                   Designation = designation.Name,
                   EmployeeStatus = status.Code.ToUpper(),
                   StatusColorClassName = Helpers.AssignStatusColorClassName(status.Code),
                   EmployeeTypeColorClassName = Helpers.AssignEmployeeTypeColorClassName(empType.Name)
                 }).ToList();

        if (q.Count == 0)
        {
          return NotFound();
        }

        return Ok(q);
      }
      catch (Exception e)
      {
        // TODO: Log exception
        _logger.LogError("API_ERR | GET_EMPLOYEES | ERR: {0}", e.Message.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("{id}", Name = "GetEmployee")]
    public IActionResult Get(int id)
    {
      try
      {
        var q = (from emp in _context.Employees
                 join contact in _context.Contacts on emp.ContactId equals contact.Id
                 join empType in _context.EmployeeTypes on emp.EmployeeTypeId equals empType.Id
                 join designation in _context.Designations on emp.DesignationId equals designation.Id
                 join status in _context.EmployeeStatuses on emp.EmployeeStatusId equals status.Id
                 where emp.Id == id && status.Code != Constants.DELETED
                 select new EmployeeDetailDto
                 {
                   Id = emp.Id,
                   FirstName = contact.FirstName,
                   LastName = contact.LastName,
                   Email = contact.Email,
                   OfficialEmail = contact.OfficialEmail,
                   Phone = contact.Phone,
                   MobilePhone = contact.MobilePhone,
                   IsUnderProbation = emp.IsUnderProbation,
                   EndedProbationAt = DateTimeExtensions.GetFormattedDateString(emp.EndedProbationAt),
                   HiredAt = DateTimeExtensions.GetFormattedDateString(emp.HiredAt),
                   LeaveAt = DateTimeExtensions.GetFormattedDateString(emp.LeaveAt),
                   EmployeeType = empType.Name,
                   Designation = designation.Name,
                   EmployeeStatus = status.Code.ToUpper(),
                   DesignationId = designation.Id,
                   EmployeeStatusId = status.Id,
                   EmployeeTypeId = empType.Id,
                   ABAAccountName = emp.ABAAccountName,
                   ABAAccountNumber = emp.ABAAccountNumber,
                   Remarks = emp.Remarks,
                   Street = contact.PhysicalStreet,
                   City = contact.PhysicalCity,
                   State = contact.PhysicalState,
                   ZipCode = contact.PhysicalZipCode,
                   Country = contact.PhysicalCountry,
                   Address = StringExtensions.ConvertStringArrayToStringJoin(Helpers.AddressParts(
                     new PhysicalAddress()
                     {
                       Street = contact.PhysicalStreet,
                       City = contact.PhysicalCity,
                       State = contact.PhysicalState,
                       ZipCode = contact.PhysicalZipCode,
                       Country = contact.PhysicalCountry
                     }), ", "),
                   StatusColorClassName = Helpers.AssignStatusColorClassName(status.Code),
                   EmployeeTypeColorClassName = Helpers.AssignEmployeeTypeColorClassName(empType.Name)
                 }).FirstOrDefault();

        if(q == null)
        {
          return NotFound();
        }

        return Ok(q);
      }
      catch (Exception e)
      {
        // TODO: Log exception
        _logger.LogError("API_ERR | GET_EMPLOYEE | ID: {0} | ERR: {1}", id.ToString(), e.Message.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    // GET api/employees/search/?=
    [HttpGet("search")]
    public IActionResult Search(string str)
    {
      try
      {
        var q = (from emp in _context.Employees
                 join contact in _context.Contacts on emp.ContactId equals contact.Id
                 join empType in _context.EmployeeTypes on emp.EmployeeTypeId equals empType.Id
                 join designation in _context.Designations on emp.DesignationId equals designation.Id
                 join status in _context.EmployeeStatuses on emp.EmployeeStatusId equals status.Id
                 where contact.LastName.ToLower().Contains(str.ToLower())
                   || contact.FirstName.ToLower().Contains(str.ToLower()) && status.Code != Constants.DELETED
                 select new EmployeeDto()
                 {
                   Id = emp.Id,
                   FirstName = contact.FirstName,
                   LastName = contact.LastName,
                   Email = contact.Email,
                   OfficialEmail = contact.OfficialEmail,
                   Phone = contact.Phone,
                   MobilePhone = contact.MobilePhone,
                   IsUnderProbation = emp.IsUnderProbation,
                   EmployeeType = empType.Name,
                   Designation = designation.Name,
                   EmployeeStatus = status.Code.ToUpper(),
                   StatusColorClassName = Helpers.AssignStatusColorClassName(status.Code),
                   EmployeeTypeColorClassName = Helpers.AssignEmployeeTypeColorClassName(empType.Name)
                 }).ToList();

        if (q == null)
        {
          return NotFound();
        }

        return Ok(q);
      }
      catch (Exception e)
      {
        _logger.LogError("API_ERR | SEARCH_EMPLOYEE | Q_STR: {0} | ERR: {1}", str, e.StackTrace.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    // GET api/employees/init
    [HttpGet("init")]
    public IActionResult Init()
    {
      try
      {
        var designations = _context.Designations.Select(d => new ListDesignationDto() {
          Id = d.Id,
          Name = d.Name
        }).ToList();

        var types = _context.EmployeeTypes.Select(t => new ListEmployeeTypeDto()
        {
          Id = t.Id,
          Name = t.Name
        }).ToList();

        var statuses = _context.EmployeeStatuses.Select(s => new ListEmployeeStatusDto()
        {
          Id = s.Id,
          Code = s.Code
        }).ToList();

        var model = new InitEmployeeDto()
        {
          Designations = designations,
          Types = types,
          Statuses = statuses
        };

        return Ok(model);
      } catch(Exception e)
      {
        _logger.LogError("API_ERR | INIT_EMPLOYEE | ERR: {0}", e.StackTrace.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddEmployeeDto item)
    {
      try
      {
        if(item == null)
        {
          return BadRequest();
        }

        var contact = new Contact()
        {
          FirstName = item.FirstName,
          LastName = item.LastName,
          Phone = item.Phone,
          MobilePhone = item.MobilePhone,
          Email = item.Email,
          OfficialEmail = item.OfficialEmail,
          PhysicalStreet = item.Street,
          PhysicalState = item.State,
          PhysicalCity = item.City,
          PhysicalZipCode = item.ZipCode,
          PhysicalCountry = item.Country
        };

        var emp = new Employee()
        {
          ContactId = contact.Id,
          EmployeeTypeId = item.EmployeeTypeId,
          DesignationId = item.DesignationId,
          IsUnderProbation = item.IsUnderProbation,
          EmployeeStatusId = item.EmployeeStatusId,
          HiredAt = DateTime.Now,
          ABAAccountName = item.ABAAccountName,
          ABAAccountNumber = item.ABAAccountName,
          Remarks = item.Remarks
        };

        _context.Contacts.Add(contact);
        emp.Contact = contact;
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();

        return Ok(emp.Id);
      }
      catch (Exception e)
      {
        _logger.LogError("API_ERR | POST_EMPLOYEE | item: {0} | ERR: {1}", JsonConvert.SerializeObject(item), e.StackTrace.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]UpdateEmployeeDto item)
    {
      if(item == null || item.Id != id)
      {
        return BadRequest();
      }

      try
      {
        var target = _context.Employees.FirstOrDefault(e => e.Id == id);

        if (target == null)
        {
          return NotFound();
        }

        var targetContact = _context.Contacts.FirstOrDefault(c => c.Id == target.ContactId);

        targetContact.FirstName = item.FirstName;
        targetContact.LastName = item.LastName;
        targetContact.Phone = item.Phone;
        targetContact.MobilePhone = item.MobilePhone;
        targetContact.Email = item.Email;
        targetContact.OfficialEmail = item.OfficialEmail;
        targetContact.PhysicalStreet = item.Street;
        targetContact.PhysicalState = item.State;
        targetContact.PhysicalCity = item.City;
        targetContact.PhysicalZipCode = item.ZipCode;
        targetContact.PhysicalCountry = item.Country;

        target.EmployeeTypeId = item.EmployeeTypeId;
        target.EmployeeStatusId = item.EmployeeStatusId;
        target.DesignationId = item.DesignationId;
        target.IsUnderProbation = item.IsUnderProbation;
        target.EndedProbationAt = item.EndedProbationAt;
        target.HiredAt = item.HiredAt;
        target.LeaveAt = item.LeaveAt;
        target.ABAAccountName = item.ABAAccountName;
        target.ABAAccountNumber = item.ABAAccountNumber;
        target.Remarks = item.Remarks;

        _context.Update(targetContact);
        _context.Update(target);

        await _context.SaveChangesAsync();

        return Ok();
      } catch(Exception e)
      {
        _logger.LogError("API_ERR | PUT_EMPLOYEE | item: {0} | ERR: {1}", JsonConvert.SerializeObject(item), e.StackTrace.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        var emp = _context.Employees.FirstOrDefault(e => e.Id == id);

        if (emp == null)
        {
          return NotFound();
        }

        emp.EmployeeStatus = _context.EmployeeStatuses.FirstOrDefault(s => s.Code == Constants.DELETED);
        emp.IsDeleted = true;

        _context.Update(emp);
        await _context.SaveChangesAsync();

        return Ok();
      } catch(Exception e)
      {
        _logger.LogError("API_ERR | DELETE_EMPLOYEE | id: {0} | ERR: {1}", id.ToString(), e.StackTrace.ToString());
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
