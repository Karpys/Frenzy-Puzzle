
namespace PuzzleFrenzy.Scripts
{
    using System;
    using Helpers;
    using UnityEngine;

    public class PuzzlePiece : MonoBehaviour,IPosition
    {
        [SerializeField] private PieceVisual m_Visual = null;
        [SerializeField] private MeshDeform m_Deform = null;

        private Vector2Int m_PuzzleFramePlace = Vector2Int.zero;
        private Vector2Int m_CurrentPlace = Vector2Int.zero;
        private Vector3 m_DefaultPosition = Vector3.zero;
        private PuzzlePieceHolder m_PuzzlePieceHolder = null;

        public Vector3 Position => transform.position;
        public PuzzlePieceHolder Holder => m_PuzzlePieceHolder;
        
        public void Initialize(Vector2Int framePlace, float scaleSize, Vector2Int size, int[] deformData,
            Texture puzzleTexture)
        {
            m_PuzzleFramePlace = framePlace;
            name = framePlace.ToString();
            m_Deform.ApplyDeform(deformData,m_Visual.Mesh);
            m_Visual.SetUp(framePlace,size,scaleSize,puzzleTexture);
            m_DefaultPosition = transform.position;
        }

        public void Place(PuzzlePieceHolder holder, Vector2Int holderPlace)
        {
            m_PuzzlePieceHolder = holder;
            m_CurrentPlace = holderPlace;
        }

        public void ReleaseToDefaultPosition()
        {
            transform.position = m_DefaultPosition;
        }

        public void ReleaseHolder()
        {
            if (m_PuzzlePieceHolder)
            {
                m_PuzzlePieceHolder.Clear();
                m_PuzzlePieceHolder = null;
            }
        }
    }
}
