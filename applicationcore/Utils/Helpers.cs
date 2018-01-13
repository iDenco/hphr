using HPHR.ApplicationCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace HPHR.ApplicationCore.Utils.Helpers
{
  public static class Helpers
  {
    public static IEnumerable<string> AddressParts(PhysicalAddress address)
    {
      var addressParts = new[] {
              address.Street.Replace(",", "<br>").Replace("\n", "<br>"),
              address.State,
              address.City,
              address.Country,
              address.ZipCode
          };
      return addressParts.Where(p => !string.IsNullOrEmpty(p));
    }

    public static string AssignStatusColorClassName(string status)
    {
      string color = string.Empty;

      switch(status.ToLower())
      {
        case "active":
          color = "badge-success";
          break;
        case "pending":
          color = "badge-info";
          break;
        case "inactive":
          color = "badge-warning";
          break;
        case "blacklisted":
          color = "badge-danger";
          break;
        case "barred":
          color = "badge-danger";
          break;
        default:
          break;
      }

      return color;
    }

    public static string AssignEmployeeTypeColorClassName(string type)
    {
      string color = string.Empty;

      switch(type.ToLower())
      {
        case "full time":
          color = "badge-primary";
          break;
        case "part time":
          color = "badge-info";
          break;
        default: break;
      }

      return color;
    }
  }
}
