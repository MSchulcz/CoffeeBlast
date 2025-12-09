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
            hud.SetScore(currentScore); 
            hud.SetTarget(targetScore);
            hud.SetRemaining(numMoves);
        }

        public override void OnMove()
        {
            _movesUsed++;
            int movesLeft = numMoves - _movesUsed;
            hud.SetRemaining(movesLeft);
            //hud.SetRemaining(numMoves - _movesUsed); //вот тут аккуратнее(убрал от греха подальше)

            // Проверяем достижение цели сразу
            if (currentScore >= targetScore)
            {
                GameWin();
                return;
            }

            // Если ходы закончились — поражение
            if (movesLeft == 0)
            {
                GameLose();
            }   
        }
    }
}
