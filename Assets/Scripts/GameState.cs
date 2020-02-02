public static class GameState
{
    public static int levelCompleted = 0;
    public static int remainingLifes = 5;
    public static int socksPairsForLevel
    {
        get
        {
            return 5 + levelCompleted;
        }   
    }
    public static int remainingSocksPairs = socksPairsForLevel;
}
