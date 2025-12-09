using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevelButtonHandler : MonoBehaviour
{
    [Header("Сюда перетащите кнопку StartLevelButton")]
    public Button startButton;

    void OnEnable()
    {
        if (startButton != null)
        {
            // На всякий случай удаляем старые обработчики
            startButton.onClick.RemoveAllListeners();
            // Добавляем свой обработчик
            startButton.onClick.AddListener(OnStartButtonClick);
        }
        else
        {
            Debug.LogWarning("StartButton не назначен!");
        }
    }

    void OnStartButtonClick()
    {
        Debug.Log("StartLevelButton CLICKED!");
        
        string levelName = ProgressSystem.GetCurrentLevelName(); // ваш метод получения уровня
        Debug.Log("Trying to load: " + levelName);
        
        SceneManager.LoadScene(levelName);
    }
}
