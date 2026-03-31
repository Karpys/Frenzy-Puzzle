namespace PuzzleFrenzy.Scripts.Puzzle
{
    using System;
    using Helpers;
    using KarpysDev.KarpysUtils.TweenCustom;
    using Managers;
    using MeshDeform;
    using UnityEngine;

    public class PuzzlePiece : MonoBehaviour,IPosition
    {
        [SerializeField] private PieceVisual m_Visual = null;
        [SerializeField] private BaseDeform m_Deform = null;
        
        [Header("Outline")]
        [SerializeField] private MeshFilter m_OutlineMeshFilter = null;
        [SerializeField] private BaseDeform m_Outline = null;

        private bool m_CanBeSelected = true;
        private bool m_IsEdge = false;
        private Vector2Int m_PuzzleFramePlace = Vector2Int.zero;
        private Vector2Int m_CurrentPlace = Vector2Int.zero;
        private Vector3 m_DefaultPosition = Vector3.zero;
        private PuzzlePieceHolder m_PuzzlePieceHolder = null;

        public Vector3 Position => transform.position;
        public PuzzlePieceHolder Holder => m_PuzzlePieceHolder;
        public Vector2Int TargetPosition => m_PuzzleFramePlace;
        public bool IsEdge => m_IsEdge;
        public bool CanBeSelected => m_CanBeSelected;

        public void Initialize(Vector2Int framePlace, float scaleSize, Vector2Int size, int[] deformData,
            Texture puzzleTexture, bool isEdge)
        {
            m_PuzzleFramePlace = framePlace;
            name = framePlace.ToString();
            m_Deform.ApplyDeform(deformData,m_Visual.Mesh);
            m_Outline.ApplyDeform(deformData,m_OutlineMeshFilter.mesh);
            m_Visual.SetUp(framePlace,size,scaleSize,puzzleTexture);
            m_DefaultPosition = transform.position;
            m_IsEdge = isEdge;
        }

        public void Place(PuzzlePieceHolder holder, Vector2Int holderPlace)
        {
            m_PuzzlePieceHolder = holder;
            m_CurrentPlace = holderPlace;
        }

        public void ReleaseToDefaultPosition()
        {
            transform.DoMove(m_DefaultPosition, GlobalVariables.PuzzlePiecePlaceSpeed).SetEase(GlobalVariables.PuzzlePiecePlaceEase).SetMode(TweenMode.SPEED);
        }

        public void ReleaseHolder()
        {
            if (m_PuzzlePieceHolder)
            {
                m_PuzzlePieceHolder.Clear();
                m_PuzzlePieceHolder = null;
            }
        }

        
        public void Display()
        {
            m_Visual.Display();
            m_CanBeSelected = true;
        }
        
        public void Hide()
        {
            m_Visual.Hide();
            m_CanBeSelected = false;
        }
    }
}
