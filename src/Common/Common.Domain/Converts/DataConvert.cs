namespace Common.Domain.Converts;

using System;
using System.Globalization;

public static class DateConvertor
{
    public static string ToShamsi(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime):00}/{pc.GetDayOfMonth(dateTime):00}";
    }
    public static DateTime ToMiladi(this DateTime dateTime)
        => new(dateTime.Year, dateTime.Month, dateTime.Day, new PersianCalendar());

    public static int GetYearShamsi(this DateTimeOffset dateTime)
    {
        var pc = new PersianCalendar();

        var dateTimeUtc = dateTime.UtcDateTime;

        return pc.GetYear(dateTimeUtc);
    }
}
