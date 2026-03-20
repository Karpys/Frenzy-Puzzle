namespace PuzzleFrenzy.Scripts
{
    using System;
    using Helpers;
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class PuzzlePieceSelector : MonoBehaviour
    {
        [SerializeField] private Transform m_CursorFollower = null;
        [SerializeField] private PuzzlePiecePlacer m_PuzzlePiecePlacer = null;
        [SerializeField] private Camera m_InputCamera = null;
        [SerializeField] private float m_RangeSelect = 1.0f;

        private Transform m_OldParent = null;
        private PuzzlePiece m_CurrentSelectedPiece = null;
        private PuzzlePiece[] m_Pieces = null;

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(m_CurrentSelectedPiece == null)
                    TrySelectPiece();
            }else if (Input.GetMouseButtonUp(0))
            {
                ReleasePiece();
            }

            if (m_CurrentSelectedPiece != null)
            {
                // Vector3 inputPosition = m_InputCamera.ScreenToWorldPoint(Input.mousePosition);
                // inputPosition.y = 2;
                // m_CurrentSelectedPiece.transform.position = inputPosition;
            }
        }

        private void ReleasePiece()
        {
            Debug.Log("Release Piece");
            if (m_CurrentSelectedPiece)
            {
                m_CurrentSelectedPiece.transform.DoKill();
                m_CurrentSelectedPiece.transform.parent = m_OldParent;
                TryPlace(m_CurrentSelectedPiece);
            }
            m_CurrentSelectedPiece = null;
        }

        private void TryPlace(PuzzlePiece currentSelectedPiece)
        {
            m_CurrentSelectedPiece.transform.position = m_CurrentSelectedPiece.transform.position.SetY(0);
            m_PuzzlePiecePlacer.TryPlace(currentSelectedPiece);
        }

        public void SetPieces(PuzzlePiece[] pieces)
        {
            m_Pieces = pieces;
        }

        private void TrySelectPiece()
        {
            if(m_Pieces == null || m_Pieces.Length == 0)
                return;
            
            Vector3 inputPosition = m_InputCamera.ScreenToWorldPoint(Input.mousePosition);
            inputPosition.y = 0;

            int closest = m_Pieces.GetClosestViaId(inputPosition,out float distance);

            if (distance <= m_RangeSelect)
                SelectPiece(closest);
        }

        private void SelectPiece(int id)
        {
            m_CurrentSelectedPiece = m_Pieces[id];
            m_CurrentSelectedPiece.transform.DoKill();
            m_OldParent = m_CurrentSelectedPiece.transform.parent;
            m_CurrentSelectedPiece.transform.parent = m_CursorFollower;
            m_CurrentSelectedPiece.transform.DoLocalMove(new Vector3(0,-2,0), GlobalVariables.PuzzlePiecePlaceSpeed).SetEase(GlobalVariables.PuzzlePiecePlaceEase).SetMode(TweenMode.SPEED);
        }
    }
}