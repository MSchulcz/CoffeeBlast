using UnityEngine;

/// <summary>
/// Контроллер окончания уровня. Вызвать OnLevelWin() при победе, OnLevelLose() при поражении.
/// </summary>
public class LevelEndController : MonoBehaviour
{
    [Tooltip("Количество монет, которое игрок получает за прохождение этого уровня.")]
    [SerializeField] private int rewardCoins = 20;

    /// <summary>
    /// Вызывается при победе игрока на уровне.
    /// </summary>
    public void OnLevelWin()
    {
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.OnLevelComplete(rewardCoins);
        }
        else
        {
            // Если LevelLoader не найден — всё ещё можно начислить и вернуться вручную
            ProgressController.AddCoins(rewardCoins);
            ProgressController.CurrentLevel = ProgressController.CurrentLevel + 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainSceneNew"); //вот тут какой-то прикол произошёл - пришлось вручную менять имя сцены
        }
    }

    /// <summary>
    /// Вызывается при поражении — просто возвращает в MainScene.
    /// </summary>
    public void OnLevelLose()
    {
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.LoadMainScene();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
