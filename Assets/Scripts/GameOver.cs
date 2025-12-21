using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class GameOver : MonoBehaviour
    {
        public GameObject screenParent;
        public GameObject scoreParent;
        public Text loseText;
        public Text scoreText;
        public Image[] stars;   // 3 звезды

        private void Start()
        {
            screenParent.SetActive(false);

            // Выключаем все звезды
            foreach (var s in stars)
                s.enabled = false;
        }

        public void ShowLose()
        {
            screenParent.SetActive(true);
            scoreParent.SetActive(false);
            loseText.enabled = true;

            Animator animator = GetComponent<Animator>();
            if (animator)
                animator.Play("GameOverShow");
        }

        public void ShowWin(int score, int starCount)
        {
            screenParent.SetActive(true);
            loseText.enabled = false;

            scoreText.text = score.ToString();
            scoreText.enabled = false;

            Animator animator = GetComponent<Animator>();
            if (animator)
                animator.Play("GameOverShow");

            StartCoroutine(ShowWinCoroutine(starCount));
        }

        private IEnumerator ShowWinCoroutine(int starCount)
        {
            yield return new WaitForSeconds(0.5f);

            starCount = Mathf.Clamp(starCount, 0, stars.Length);

            for (int i = 0; i < starCount; i++)
            {
                stars[i].enabled = true;
                yield return new WaitForSeconds(0.4f);
            }

            scoreText.enabled = true;
        }

        /// <summary>
        /// Кнопка "Replay" — перезапускает текущий уровень
        /// </summary>
        public void OnReplayClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }

        /// <summary>
        /// Кнопка "Done" — завершение уровня
        /// </summary>
        public void OnDoneClicked()
        {
            // Ищем LevelEndController на сцене
            LevelEndController end = FindObjectOfType<LevelEndController>();
            if (end != null)
            {
                // Вызываем победу, которая начисляет монеты и открывает следующий уровень
                end.OnLevelWin();
            }
            else
            {
                // fallback на MainScene
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainSceneNew");
            }
        }
    }
}
