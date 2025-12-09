﻿using System.Collections;
using UnityEngine;

namespace Match3
{
    public class Level : MonoBehaviour
    {
        public GameGrid gameGrid;
        public Hud hud;

        public int score1Star;
        public int score2Star;
        public int score3Star;

        protected LevelType type;

        protected int currentScore;

        private bool _didWin;

        private void Start()
        {
            hud.SetScore(currentScore);
        }

        public LevelType Type => type;

        protected virtual void GameWin()
        {
            gameGrid.GameOver();
            _didWin = true;
            StartCoroutine(WaitForGridFill());
        }

        protected virtual void GameLose()
        {
            gameGrid.GameOver();
            _didWin = false;
            StartCoroutine(WaitForGridFill());
        }

        public virtual void OnMove()
        {
        }

        public virtual void OnPieceCleared(GamePiece piece)
        {
            currentScore += piece.score;
            hud.SetScore(currentScore);
        }

        protected virtual IEnumerator WaitForGridFill()
        {
            while (gameGrid.IsFilling)
            {
                yield return null;
            }

            if (_didWin)
            {
                int starCount = CalculateStars(currentScore);
                hud.OnGameWin(currentScore, starCount);
            }
            else
            {
                hud.OnGameLose();
            }
        }

        /// <summary>
        /// Возвращает количество звёзд от 0 до 3 в зависимости от текущего счета и порогов.
        /// Если порог равен или меньше нуля, он игнорируется.
        /// </summary>
        protected int CalculateStars(int score)
        {
            // Если установлены пороги — вычисляем количество звёзд по убывающему приоритету.
            if (score3Star > 0 && score >= score3Star) return 3;
            if (score2Star > 0 && score >= score2Star) return 2;
            if (score1Star > 0 && score >= score1Star) return 1;
            return 0;
        }
    }
}
