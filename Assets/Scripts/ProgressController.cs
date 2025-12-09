using UnityEngine;

/// <summary>
/// Статический контроллер прогресса — хранит текущий доступный уровень и монеты (PlayerPrefs).
/// </summary>
public static class ProgressController
{
    private const string LevelKey = "CurrentLevel";
    private const string CoinsKey = "Coins";

    // Текущий доступный уровень (начинается с 1)
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt(LevelKey, 1);
        set
        {
            PlayerPrefs.SetInt(LevelKey, Mathf.Max(1, value));
            PlayerPrefs.Save();
        }
    }

    // Всего монет
    public static int Coins
    {
        get => PlayerPrefs.GetInt(CoinsKey, 0);
        set
        {
            PlayerPrefs.SetInt(CoinsKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }
    }

    public static void AddCoins(int amount)
    {
        if (amount <= 0) return;
        Coins = Coins + amount;
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(LevelKey);
        PlayerPrefs.DeleteKey(CoinsKey);
        PlayerPrefs.Save();
    }
}
