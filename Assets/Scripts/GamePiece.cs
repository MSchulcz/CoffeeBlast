using UnityEngine;

namespace Match3
{
    public class GamePiece : MonoBehaviour
    {
        public int score;

        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set { if (IsMovable()) { _x = value; } }
        }

        public int Y
        {
            get => _y;
            set { if (IsMovable()) { _y = value; } }
        }

        private PieceType _type;
        public PieceType Type => _type;

        private GameGrid _gameGrid;
        public GameGrid GameGridRef => _gameGrid;

        private MovablePiece _movableComponent;
        public MovablePiece MovableComponent => _movableComponent;

        private ColorPiece _colorComponent;
        public ColorPiece ColorComponent => _colorComponent;

        private ClearablePiece _clearableComponent;
        public ClearablePiece ClearableComponent => _clearableComponent;

        private ActivatablePiece _activatableComponent;
        public ActivatablePiece ActivatableComponent => _activatableComponent;

        private Vector3 _dragStartPos;
        private const float DRAG_SPEED = 20f; // Speed for smooth dragging
        private const float CLICK_THRESHOLD = 0.1f; // Считаем малое движение кликом

        private void Awake()
        {
            _movableComponent = GetComponent<MovablePiece>();
            _colorComponent = GetComponent<ColorPiece>();
            _clearableComponent = GetComponent<ClearablePiece>();
            _activatableComponent = GetComponent<ActivatablePiece>();
        }

        public void Init(int x, int y, GameGrid gameGrid, PieceType type)
        {
            _x = x;
            _y = y;
            _gameGrid = gameGrid;
            _type = type;
        }

        private void OnMouseEnter() => _gameGrid.EnterPiece(this);

        private void OnMouseDown()
        {
            _dragStartPos = transform.position;
            _gameGrid.PressPiece(this);
        }

        private void OnMouseUp()
        {
            Vector3 dragDelta = transform.position - _dragStartPos;
            float distance = dragDelta.magnitude;
            bool isClick = distance < CLICK_THRESHOLD;

            // Если фишка активируемая и это клик — активируем её
            if (IsActivatable() && isClick)
            {
                _activatableComponent.Activate();

                // Возврат на сетку для Movable
                if (IsMovable())
                    _movableComponent.ReturnToPosition(_gameGrid.fillTime);

                return;
            }

            // Если фишка Movable и движение превышает порог — обрабатываем перетаскивание
            if (IsMovable() && distance >= CLICK_THRESHOLD)
            {
                _gameGrid.ReleasePiece();
                _movableComponent.ReturnToPosition(_gameGrid.fillTime);
            }
            else if (IsMovable()) // Если клик без движения — просто вернуть на место
            {
                _movableComponent.ReturnToPosition(_gameGrid.fillTime);
            }
        }

        private void OnMouseDrag()
        {
            if (!IsMovable() || _gameGrid.IsFilling || IsActivatable()) return;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;

            Vector3 dragDelta = worldPos - _dragStartPos;
            bool isHorizontal = Mathf.Abs(dragDelta.x) > Mathf.Abs(dragDelta.y);

            if (isHorizontal)
                worldPos.y = _dragStartPos.y;
            else
                worldPos.x = _dragStartPos.x;

            Vector2 gridMin = _gameGrid.GetWorldPosition(0, _gameGrid.yDim - 1);
            Vector2 gridMax = _gameGrid.GetWorldPosition(_gameGrid.xDim - 1, 0);

            float cellSizeClamp = 1.0f;
            worldPos.x = Mathf.Clamp(worldPos.x, gridMin.x - cellSizeClamp, gridMax.x + cellSizeClamp);
            worldPos.y = Mathf.Clamp(worldPos.y, gridMin.y - cellSizeClamp, gridMax.y + cellSizeClamp);

            transform.position = Vector3.Lerp(transform.position, worldPos, Time.deltaTime * DRAG_SPEED);
        }

        public bool IsMovable() => _movableComponent != null;
        public bool IsColored() => _colorComponent != null;
        public bool IsClearable() => _clearableComponent != null;
        public bool IsActivatable() => _activatableComponent != null;
    }
}
