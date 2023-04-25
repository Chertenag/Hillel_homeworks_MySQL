namespace Hillel_hw_23.Core
{
    public static class DateMapper
    {
        public static string? Convert (string value)
        {
            if (value == string.Empty)
            {
                return null;
            }
            else
            {
                if (DateOnly.TryParse(value, out var dateOnly))
                {
                    return dateOnly.ToString("yyyy-MM-dd");
                }
                else
                {
                    throw new ArgumentException("Wrong date format.");
                }
            }
        }
    }
}