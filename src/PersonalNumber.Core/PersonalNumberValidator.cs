using System;
using System.Linq;

namespace PersonalNumber.Core;

public static class PersonalNumberValidator
{
    // Accepts formats:
    // YYMMDD-XXXX, YYMMDD+XXXX, YYMMDDXXXX
    // YYYYMMDDXXXX, YYYYMMDD-XXXX
    public static bool IsValid(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // Remove non-digits
        string digits = new string(input.Where(char.IsDigit).ToArray());

        // Must be 10 or 12 digits
        if (digits.Length != 10 && digits.Length != 12)
            return false;

        // Validate date part
        if (!IsValidDatePart(digits))
            return false;

        // Luhn is calculated on last 10 digits
        string last10 = digits.Length == 12
            ? digits.Substring(2, 10)
            : digits;

        return PassesLuhn(last10);
    }

    private static bool IsValidDatePart(string digits)
    {
        if (digits.Length == 12)
        {
            int year = int.Parse(digits.Substring(0, 4));
            int month = int.Parse(digits.Substring(4, 2));
            int day = int.Parse(digits.Substring(6, 2));

            // Coordination number (day + 60)
            if (day >= 61 && day <= 91)
                day -= 60;

            try
            {
                _ = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            int month = int.Parse(digits.Substring(2, 2));
            int day = int.Parse(digits.Substring(4, 2));

            if (month < 1 || month > 12)
                return false;

            if (day >= 61 && day <= 91)
                day -= 60;

            return day >= 1 && day <= 31;
        }
    }

    // Luhn / Mod-10 algorithm
    private static bool PassesLuhn(string tenDigits)
    {
        int sum = 0;

        for (int i = 0; i < tenDigits.Length; i++)
        {
            int digit = tenDigits[i] - '0';

            if (i % 2 == 0)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
        }

        return sum % 10 == 0;
    }
}
