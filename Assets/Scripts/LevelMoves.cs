﻿namespace Match3
{
    public class LevelMoves : Level
    {

        public int numMoves;
        public int targetScore;

        private int _movesUsed = 0;

        private void Start()
        {
            type = LevelType.Moves;

            hud.SetLevelType(type);
            hud.SetScore(targetScore - currentScore); // Отображаем оставшиеся очки
            hud.SetTarget(targetScore);
            hud.SetRemaining(numMoves);
        }

        public override void OnMove()
        {
            _movesUsed++;

            hud.SetRemaining(numMoves - _movesUsed);
            hud.SetScore(targetScore - currentScore); // Обновляем оставшиеся очки

            if (numMoves - _movesUsed != 0) return;

            if (currentScore >= targetScore)
            {
                GameWin();
            }
            else
            {
                GameLose();
            }
        }
    }
}
