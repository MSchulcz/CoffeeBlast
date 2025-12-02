using UnityEngine;

namespace Match3
{
    public class RandomClearPiece : ActivatablePiece
    {
        // Используем _piece из базового класса
        
        // Переопределяем Awake для вызова базового метода
        protected override void Awake()
        {
            base.Awake();
        }

        public override void Activate()
        {
            Debug.Log("RandomClearPiece активирован!");
            // Clear a random piece on the grid
            var allPieces = _piece.GameGridRef.GetPiecesOfType(PieceType.Normal);
            Debug.Log($"Найдено {allPieces.Count} обычных кусочков");
            if (allPieces.Count > 0)
            {
                int randomIndex = Random.Range(0, allPieces.Count);
                GamePiece randomPiece = allPieces[randomIndex];
                Debug.Log($"Очищаю случайный кусочек в ({randomPiece.X},{randomPiece.Y})");
                _piece.GameGridRef.ClearPiece(randomPiece.X, randomPiece.Y);
            }

            // Clear self
            Debug.Log("Очищаю себя");
            _piece.ClearableComponent.Clear();
        }
    }
}
