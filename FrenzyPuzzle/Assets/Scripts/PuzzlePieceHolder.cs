namespace PuzzleFrenzy.Scripts
{
    using System;
    using Helpers;
    using UnityEngine;

    public class PuzzlePieceHolder : MonoBehaviour,IPosition
    {
        [SerializeField] private Vector2Int m_Position = Vector2Int.zero;
        [SerializeField] private PuzzlePiece m_PuzzlePiece = null;

        private bool m_IsValid = false;

        public Vector3 Position => transform.position;
        public bool IsValid => m_IsValid;
        public Action A_OnPlaceNewPiece = null;

        public void Initialize(Vector2Int position)
        {
            m_Position = position;
        }

        public void TryPlace(PuzzlePiece puzzlePiece)
        {
            if (m_PuzzlePiece == null)
            {
                if (puzzlePiece.Holder != null)
                    puzzlePiece.ReleaseHolder();
                Place(puzzlePiece);
            }
            else
            {
                if (puzzlePiece.Holder == null)
                {
                    m_PuzzlePiece.ReleaseToDefaultPosition();
                    m_PuzzlePiece.ReleaseHolder();
                    Place(puzzlePiece);
                }
                else
                {
                    Swap(puzzlePiece, m_PuzzlePiece);
                }
            }
        }

        private void Swap(PuzzlePiece puzzlePiece1, PuzzlePiece puzzlePiece2)
        {
            PuzzlePieceHolder otherHolder = puzzlePiece1.Holder;
            otherHolder.Place(puzzlePiece2);
            Place(puzzlePiece1);
        }

        private void Place(PuzzlePiece puzzlePiece)
        {
            m_PuzzlePiece = puzzlePiece;
            m_PuzzlePiece.transform.position = transform.position;
            puzzlePiece.Place(this,m_Position);
            m_IsValid = m_PuzzlePiece.TargetPosition == m_Position;
            
            A_OnPlaceNewPiece?.Invoke();
        }

        public void Clear()
        {
            m_PuzzlePiece = null;
        }
    }
}