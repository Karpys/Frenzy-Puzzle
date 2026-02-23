
namespace PuzzleFrenzy.Scripts
{
    using System;
    using UnityEngine;

    public class PuzzlePiece : MonoBehaviour
    {
        [SerializeField] private PieceVisual m_Visual = null;
        [SerializeField] private MeshDeform m_Deform = null;
        
        private Vector2Int m_PuzzleFramePlace = Vector2Int.zero;
        private Vector2Int m_CurrentPlace = Vector2Int.zero;

        public void Initialize(Vector2Int framePlace,float size,int[] deformData)
        {
            m_PuzzleFramePlace = framePlace;
            name = framePlace.ToString();
            m_Deform.ApplyDeform(deformData,m_Visual.Mesh);
            m_Visual.SetUp(framePlace,size);
        }
    }
}
