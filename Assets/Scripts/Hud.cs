﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Match3
{
    public class Hud : MonoBehaviour
    {
        public Level level;
        public GameOver gameOver;

        public Text remainingText;
        public Text remainingSubText;
        public Text targetText;
        public Text targetSubtext;
        public Text scoreText;
        public Image[] stars;

        private int _starIndex = 0;

        private void Start()
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].enabled = false; // Изначально все звезды выключены
            }
        }

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();

            int visibleStar = 0;

            if (score >= level.score3Star)
                visibleStar = 3;
            else if (score >= level.score2Star)
                visibleStar = 2;
            else if (score >= level.score1Star)
                visibleStar = 1;

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].enabled = (i < visibleStar);
            }

            _starIndex = visibleStar;
        }

        public void SetTarget(int target) => targetText.text = target.ToString();

        public void SetRemaining(int remaining) => remainingText.text = remaining.ToString();

        public void SetRemaining(string remaining) => remainingText.text = remaining;

        public void SetLevelType(LevelType type)
        {
            switch (type)
            {
                case LevelType.Moves:
                    remainingSubText.text = "ходы";
                    targetSubtext.text = "цель";
                    break;
                case LevelType.Obstacle:
                    remainingSubText.text = "ходы";
                    targetSubtext.text = "осталось пузырьков";
                    break;
                case LevelType.Timer:
                    remainingSubText.text = "осталось времени";
                    targetSubtext.text = "цель";
                    break;
            }
        }

        /// <summary>
        /// Вызывается после победы на уровне
        /// </summary>
        public void OnGameWin(int score, int starCount)
        {
            // Показываем окно победы
            gameOver.ShowWin(score, starCount);

            // Обновляем PlayerPrefs со звездами
            string levelName = SceneManager.GetActiveScene().name;
            int savedStars = PlayerPrefs.GetInt(levelName, 0);
            if (starCount > savedStars)
            {
                PlayerPrefs.SetInt(levelName, starCount);
            }
        }

        /// <summary>
        /// Вызывается после поражения
        /// </summary>
        public void OnGameLose()
        {
            gameOver.ShowLose();
        }
    }
}
