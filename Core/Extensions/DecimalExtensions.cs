using System.Globalization;

namespace Core.Extensions;

public static class DecimalExtensions
{
    public static decimal ParseToDecimal(this string value)
    {
        try
        {
            NumberStyles style;
            CultureInfo culture;
            decimal number;
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            if (Decimal.TryParse(value, out number))
            {
                return number;
            }

            return 0M;
        }
        catch (Exception e)
        {
            return 0M;
        }
    }
}