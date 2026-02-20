
namespace PuzzleFrenzy.Scripts
{
    using System;
    using UnityEngine;

    public class PuzzlePiece : MonoBehaviour
    {
        [SerializeField] private PieceVisual m_Visual = null;
        
        private Vector2Int m_PuzzleFramePlace = Vector2Int.zero;
        private Vector2Int m_CurrentPlace = Vector2Int.zero;

        public void Initialize(Vector2Int framePlace,float size)
        {
            m_PuzzleFramePlace = framePlace;
            name = framePlace.ToString();
            m_Visual.SetUp(framePlace,size);
        }
    }
}
