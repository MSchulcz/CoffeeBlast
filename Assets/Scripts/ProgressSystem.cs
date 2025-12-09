using UnityEngine;

public static class ProgressSystem
{
    private const string CURRENT_LEVEL_KEY = "CurrentLevel";

    // текущий уровень (номер)
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);
        set
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, value);
            PlayerPrefs.Save();
        }
    }

    // вернуть имя сцены уровня
    public static string GetCurrentLevelName()
    {
        return "Level_" + CurrentLevel.ToString("000");
    }

    // переход на следующий уровень
    public static void AdvanceLevel()
    {
        CurrentLevel++;
    }
}
