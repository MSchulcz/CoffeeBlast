using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Указать номер этого уровня")]
    public int levelNumber;

    // вызывается в момент победы
    public void CompleteLevel()
    {
        // если игрок завершил именно текущий актуальный уровень
        if (levelNumber == ProgressSystem.CurrentLevel)
        {
            ProgressSystem.AdvanceLevel();
        }

        SceneManager.LoadScene("MainScene");
    }
}
