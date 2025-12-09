using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI на MainScene: показывает текущий доступный уровень и количество монет.
/// Кнопка Play вызывает LevelLoader.Instance.LoadCurrentLevel()
/// </summary>
public class MainSceneUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text coinsText;

    private void Start()
    {
        RefreshUI();
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    /// <summary>
    /// Вызывается кнопкой Play
    /// </summary>
    public void OnPlayButton()
    {
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("MainSceneUI: LevelLoader.Instance == null");
            return;
        }

        LevelLoader.Instance.LoadCurrentLevel();
    }

    public void RefreshUI()
    {
        if (levelText != null)
            levelText.text = $"Level: {ProgressController.CurrentLevel}";

        if (coinsText != null)
            coinsText.text = $"Coins: {ProgressController.Coins}";
    }
}
