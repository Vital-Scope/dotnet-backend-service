namespace VitalScope.Common.Helpers;

public static class DateTimeHelper
{
    public static DateTime? ToDateTime(this long? timeSpan)
    {
        return timeSpan == null
            ? null
            : DateTimeOffset.FromUnixTimeSeconds(timeSpan.Value).UtcDateTime;
    }
    
    public static long? ToTime(this DateTime? date)
    {
        return date == null ? null : new DateTimeOffset(date.Value).ToUnixTimeSeconds();
    }
}