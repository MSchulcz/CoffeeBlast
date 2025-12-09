using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton загрузчик сцен и централизованная обработка завершения уровня.
/// Повесить на один объект в MainScene и установить LevelDatabase в инспекторе.
/// </summary>
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [Tooltip("Ссылка на ScriptableObject с перечнем сцен уровней")]
    [SerializeField] private LevelDatabase database;

    [Tooltip("Имя MainScene (точно как в Build Settings)")]
    [SerializeField] private string mainSceneName = "MainScene";

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (database == null)
            {
                Debug.LogError("LevelLoader: LevelDatabase не указана в инспекторе!");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Загружает текущий доступный уровень (по ProgressController.CurrentLevel).
    /// </summary>
    public void LoadCurrentLevel()
    {
        if (database == null)
        {
            Debug.LogError("LevelLoader: LevelDatabase отсутствует!");
            return;
        }

        int index = ProgressController.CurrentLevel - 1;
        string sceneName = database.GetSceneNameByIndex(index);

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning($"LevelLoader: Нет сцены для уровня {ProgressController.CurrentLevel}. Проверь LevelDatabase.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Загружает главную сцену (MainScene).
    /// </summary>
    public void LoadMainScene()
    {
        if (string.IsNullOrEmpty(mainSceneName))
        {
            Debug.LogError("LevelLoader: mainSceneName не задан!");
            return;
        }

        SceneManager.LoadScene(mainSceneName);
    }

    /// <summary>
    /// Вызывается при успешном прохождении уровня:
    /// начисляет монеты, повышает доступный уровень (если есть следующий) и возвращает в MainScene.
    /// </summary>
    public void OnLevelComplete(int rewardCoins)
    {
        // Начисляем монеты
        if (rewardCoins > 0)
        {
            ProgressController.AddCoins(rewardCoins);
        }

        // Увеличиваем доступный уровень, но не выше количества уровней в базе
        if (database != null)
        {
            int next = ProgressController.CurrentLevel + 1;
            if (next <= database.LevelCount)
            {
                ProgressController.CurrentLevel = next;
            }
            else
            {
                // Если это последний уровень — можно оставить уровень как есть
                Debug.Log("LevelLoader: Игрок прошёл последний уровень в базе.");
            }
        }
        else
        {
            // Если базы нет — просто инкрементируем
            ProgressController.CurrentLevel = ProgressController.CurrentLevel + 1;
        }

        // Возвращаем в MainScene
        LoadMainScene();
    }

    /// <summary>
    /// Принудительно загрузить конкретный уровень по индексу (1-based).
    /// </summary>
    public void LoadLevelByNumber(int levelNumber)
    {
        if (database == null) return;

        int index = levelNumber - 1;
        string sceneName = database.GetSceneNameByIndex(index);
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning($"LevelLoader: сцена для уровня {levelNumber} не найдена.");
        }
    }

    /// <summary>
    /// Для тестирования: сразу открыть следующий уровень (если есть).
    /// </summary>
    public void LoadNextLevelIfExists()
    {
        if (database == null) return;

        int nextIndex = ProgressController.CurrentLevel; // current is 1-based, index for next level = current
        if (nextIndex < database.LevelCount)
        {
            SceneManager.LoadScene(database.GetSceneNameByIndex(nextIndex));
        }
        else
        {
            Debug.Log("LevelLoader: Нет следующего уровня.");
        }
    }
}
