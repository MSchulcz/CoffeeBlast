using UnityEngine;

namespace Match3
{
    public class ActivatablePiece : MonoBehaviour
    {
        protected GamePiece _piece;
        private ClearablePiece _clearablePiece;
        private ClearColorPiece _clearColorPiece;
        private ClearLinePiece _clearLinePiece;

        protected virtual void Awake()
        {
            _piece = GetComponent<GamePiece>();
            _clearablePiece = GetComponent<ClearablePiece>();
            _clearColorPiece = GetComponent<ClearColorPiece>();
            _clearLinePiece = GetComponent<ClearLinePiece>();
        }

        public virtual void Activate()
        {
            if (_clearColorPiece != null)
            {
                _clearColorPiece.Activate();
            }
            else if (_clearLinePiece != null)
            {
                _clearLinePiece.Activate();
            }
            else if (_clearablePiece != null)
            {
                _clearablePiece.Clear();
            }
        }
    }
}
