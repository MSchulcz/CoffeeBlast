using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class DebugReset : MonoBehaviour
    {
        [ContextMenu("Reset Progress & Coins")]
        public void ResetProgress()
        {
            // Удаляем все PlayerPrefs
            PlayerPrefs.DeleteAll();

            Debug.Log("Все монеты и прогресс сброшены!");

            // Для проверки можно перезагрузить MainScene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
