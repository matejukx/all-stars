namespace AllStars.Application.Helpers;

public static class DutchHelpers
{
    static public int MIN_DUTCH_POINTS_VALUE = 0;
    static public int MAX_DUTCH_POINTS_VALUE = 1000;

    static public bool IsScoreValid(int value) => value >= MIN_DUTCH_POINTS_VALUE && value <= MAX_DUTCH_POINTS_VALUE;
}
