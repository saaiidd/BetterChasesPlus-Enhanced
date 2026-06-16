using System;
using GTA.Chrono;
internal static class GameClockCompat
{
    public static DateTime Now
    {
        get
        {
            GameClockDateTime now = GameClock.Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        }
    }
}
