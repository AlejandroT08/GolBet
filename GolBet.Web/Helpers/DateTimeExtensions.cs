namespace GolBet.Web.Helpers;

public static class DateTimeExtensions
{
    private static readonly TimeZoneInfo ColombiaTimeZone;

    static DateTimeExtensions()
    {
        try
        {
            ColombiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "SA Pacific Standard Time" : "America/Bogota");
        }
        catch
        {
            ColombiaTimeZone = TimeZoneInfo.CreateCustomTimeZone(
                "Colombia Standard Time",
                TimeSpan.FromHours(-5),
                "Colombia Standard Time",
                "Colombia Standard Time");
        }
    }

    public static DateTime ToColombiaTime(this DateTime dateTime)
    {
        var utc = dateTime.Kind switch
        {
            DateTimeKind.Utc => dateTime,
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            _ => dateTime
        };

        return TimeZoneInfo.ConvertTimeFromUtc(utc, ColombiaTimeZone);
    }
}
