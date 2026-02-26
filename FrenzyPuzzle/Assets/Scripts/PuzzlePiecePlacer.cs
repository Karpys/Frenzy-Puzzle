namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzlePiecePlacer : MonoBehaviour
    {
        [SerializeField] private float m_RangePlace = 1.0f;
        [SerializeField] private PuzzlePieceHolderController m_PuzzlePieceHolderController = null;
        public void TryPlace(PuzzlePiece currentSelectedPiece)
        {
            PuzzlePieceHolder holder = m_PuzzlePieceHolderController.GetClosestHolder(currentSelectedPiece.Position,out float distance);

            if (distance <= m_RangePlace)
            {
                holder.TryPlace(currentSelectedPiece);
            }
            else
            {
                currentSelectedPiece.ReleaseHolder();
            }
        }
    }
}