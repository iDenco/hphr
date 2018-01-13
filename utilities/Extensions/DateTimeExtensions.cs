using System;

namespace HPHR.Utilities
{
  public static class DateTimeExtensions
  {
    public static string GetFormattedDateString(DateTime? dateTime)
    {
      if (dateTime != null)
      {
        return ((DateTime)dateTime).ToString("yyyy-MM-dd");
      }
      return string.Empty;
    }
  }
}
