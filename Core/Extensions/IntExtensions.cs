using System.Globalization;

namespace Core.Extensions;

public static class IntExtensions
{
    public static int ParseToInt(this string value)
    {
        try
        {
            NumberStyles style;
            CultureInfo culture;
            int number;

            if (Int32.TryParse(value, out number))
            {
                return number;
            }

            return 0;
        }
        catch (Exception e)
        {
            return 0;
            
        }
    }
}