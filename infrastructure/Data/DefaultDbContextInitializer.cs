using HPHR.ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPHR.Infrastructure.Data
{
  public class DefaultDbContextInitializer : IDefaultDbContextInitializer
  {
    private readonly DefaultDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private string[] empStatuses = new[] { "Active", "Inactive", "Pending", "Blacklisted", "Archived", "Deleted" };
    private string[] empEmails = new[] { "adam@somewhere.com", "sbiles@somewhere.com" };
    private string[] empTypes = new[] { "Full Time", "Part Time" };
    private string[] designations = new[] { "Pharmacist", "Cashier", "Salesperson" };

    public DefaultDbContextInitializer(DefaultDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _context = context;
      _roleManager = roleManager;
    }

    public bool EnsureCreated()
    {
      return _context.Database.EnsureCreated();
    }

    public void Migrate()
    {
      _context.Database.Migrate();
    }

    public async Task SeedUsers()
    {
      var email = "userz@test.com";
      if (await _userManager.FindByEmailAsync(email) == null)
      {
        var user = new ApplicationUser
        {
          UserName = email,
          Email = email,
          EmailConfirmed = true,
          GivenName = "John Doez"
        };

        await _userManager.CreateAsync(user, "P2ssw0rd!");
      }
    }

    public async Task SeedContacts()
    {
      if (_context.Contacts.Any())
      {
        foreach (var u in _context.Contacts)
        {
          _context.Remove(u);
        }
      }

      _context.Contacts.Add(new Contact() { LastName = "Finkleyz", FirstName = "Adam", Phone = "555-555-5555", Email = empEmails[0],
        PhysicalStreet = "256, Taman Desa Aman", PhysicalCity ="Kuala Pilah", PhysicalState = "Negeri Sembilan", PhysicalZipCode = "72000", PhysicalCountry = "MY" });
      _context.Contacts.Add(new Contact() { LastName = "Bilesz", FirstName = "Steven", Phone = "555-555-5555", Email = empEmails[1],
       PhysicalStreet = string.Empty, PhysicalCity ="Kuala Pilah", PhysicalState = string.Empty, PhysicalZipCode = "72000", PhysicalCountry = "MY"});

      await _context.SaveChangesAsync();
    }

    public async Task SeedEmployees()
    {
      if (_context.Employees.Any())
      {
        foreach (var u in _context.Employees)
        {
          _context.Remove(u);
        }
      }

      if (_context.EmployeeStatuses.Any())
      {
        foreach (var u in _context.EmployeeStatuses)
        {
          _context.Remove(u);
        }
      }

      if (_context.EmployeeTypes.Any())
      {
        foreach (var u in _context.EmployeeTypes)
        {
          _context.Remove(u);
        }
      }

      if (_context.Designations.Any())
      {
        foreach (var u in _context.Designations)
        {
          _context.Remove(u);
        }
      }

      // Seed EmployeeStatuses
      _context.EmployeeStatuses.Add(new EmployeeStatus() { Code = "Active", Description = "Active", IsEnabled = true });
      _context.EmployeeStatuses.Add(new EmployeeStatus() { Code = "Inactive", Description = "Inactive", IsEnabled = true });
      _context.EmployeeStatuses.Add(new EmployeeStatus() { Code = "Pending", Description = "Pending", IsEnabled = true });
      _context.EmployeeStatuses.Add(new EmployeeStatus() { Code = "Blacklisted", Description = "Blacklisted", IsEnabled = true });
      _context.EmployeeStatuses.Add(new EmployeeStatus() { Code = "Archived", Description = "Archived", IsEnabled = true });
      _context.EmployeeStatuses.Add(new EmployeeStatus() { Code = "Deleted", Description = "Deleted", IsEnabled = true });

      // Seed Employee Types
      _context.EmployeeTypes.Add(new EmployeeType() { Name = empTypes[0], Description = "Part Time Employee" });
      _context.EmployeeTypes.Add(new EmployeeType() { Name = empTypes[1], Description = "Full Time Employee" });

      // Seed Designation
      _context.Designations.Add(new Designation() { Name = designations[0], Description = "Help Plus Pharmacists" });
      _context.Designations.Add(new Designation() { Name = designations[1], Description = "Help Plus Cashiers" });
      _context.Designations.Add(new Designation() { Name = designations[2], Description = "Help Plus Salesperson" });

      await _context.SaveChangesAsync();

      List<Contact> allContacts = _context.Contacts.ToList();
      List<EmployeeStatus> allEmployeeStatuses = _context.EmployeeStatuses.ToList();
      List<EmployeeType> allEmployeeTypes = _context.EmployeeTypes.ToList();
      List<Designation> allDesignations = _context.Designations.ToList();

      _context.Employees.Add(new Employee()
      {
        ContactId = allContacts[0].Id,
        EmployeeTypeId = allEmployeeTypes[0].Id,
        DesignationId = allDesignations[0].Id,
        IsUnderProbation = false,
        HiredAt = DateTime.Now.AddYears(-1),
        ABAAccountName = "Lorem Ipsum",
        ABAAccountNumber = "11233425",
        EmployeeStatusId = allEmployeeStatuses[0].Id,
        Remarks = "Lorem ipsum"
      });

      _context.Employees.Add(new Employee()
      {
        ContactId = allContacts[1].Id,
        EmployeeTypeId = allEmployeeTypes[1].Id,
        DesignationId = allDesignations[1].Id,
        IsUnderProbation = false,
        HiredAt = DateTime.Now.AddYears(-1),
        ABAAccountName = "Lorem Ipsum",
        ABAAccountNumber = "14123123",
        EmployeeStatusId = allEmployeeStatuses[1].Id,
        Remarks = "Lorem ipsum"
      });

      await _context.SaveChangesAsync();
    }

    public async Task Seed()
    {
      await SeedUsers();
      await SeedContacts();
      await SeedEmployees();
    }
  }

  public interface IDefaultDbContextInitializer
  {
    bool EnsureCreated();
    void Migrate();
    Task Seed();
  }
}
