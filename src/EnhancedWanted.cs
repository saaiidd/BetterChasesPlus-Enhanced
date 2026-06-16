using GTA;
internal static class EnhancedWanted
{
    public static int Get() => Game.Player.Wanted.WantedLevel;
    public static void Set(int value)
    {
        int clamped = System.Math.Max(0, System.Math.Min(5, value));
        Game.Player.Wanted.SetWantedLevel(clamped, false);
        Game.Player.Wanted.ApplyWantedLevelChangeNow(false);
    }
}
