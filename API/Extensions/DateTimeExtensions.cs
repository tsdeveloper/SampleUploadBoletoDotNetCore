﻿using System.Globalization;

namespace API.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToDateTime(this string s,
        string format = "yyyyMMdd", string cultureString = "en-US")
    {
        try
        {
            var r = DateTime.ParseExact(
                s: s,
                format: format,
                provider: CultureInfo.GetCultureInfo(cultureString));
            return r;
        }
        catch (FormatException)
        {
            throw;
        }
        catch (CultureNotFoundException)
        {
            throw; // Given Culture is not supported culture
        }
    }
}