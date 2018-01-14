using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HPHR.Utilities
{
  public static class StringExtensions
  {
    public static string ToSnakeCase(this string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        return input;
      }

      var startUnderscores = Regex.Match(input, @"^_+");
      return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
    public static string ConvertStringArrayToStringJoin(IEnumerable<string> array, string symbol)
    {
      return string.Join(symbol, array);
    }
  }
}
