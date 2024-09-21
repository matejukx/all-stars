namespace AllStars.Application.Helpers;

public static class DutchHelpers
{
    public const int MinDutchPointsValue = 0;
    public const int MaxDutchPointsValue = 1000;

    public static bool IsScoreValid(int value) => value >= MinDutchPointsValue && value <= MaxDutchPointsValue;
}
