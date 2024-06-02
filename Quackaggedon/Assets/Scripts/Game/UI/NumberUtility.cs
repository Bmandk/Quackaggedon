public static class NumberUtility
{
    public static string FormatNumber(double number)
    {
        if (number < 1000)
            return number.ToString("F0");
        if (number < 1000000)
            return (number / 1000).ToString("F2") + "K";
        if (number < 1000000000)
            return (number / 1000000).ToString("F2") + "M";
        if (number < 1000000000000)
            return (number / 1000000000).ToString("F2") + "B";
        // Scientific notation
        return number.ToString("0.##e0");
    }
}