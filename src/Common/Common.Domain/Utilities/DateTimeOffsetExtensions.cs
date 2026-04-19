namespace Common.Domain.Utilities;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset TruncateToHours(this DateTimeOffset date) =>
         new(
             date.Year,
             date.Month,
             date.Day,
             0,
             0,
             0
             , date.Offset
            );

    public static DateTimeOffset TruncateToSecond(this DateTimeOffset date) =>
         new(
             date.Year,
             date.Month,
             date.Day,
             date.Hour,
             date.Minute,
             0
             , date.Offset
            );
}
