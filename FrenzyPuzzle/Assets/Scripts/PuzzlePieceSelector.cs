namespace PuzzleFrenzy.Scripts
{
    using System.Collections.Generic;
    using Helpers;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class PuzzlePieceSelector : MonoBehaviour
    {
        [SerializeField] private Camera m_InputCamera = null;
        [SerializeField] private float m_RangeSelect = 1.0f;
        
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
                Vector3 inputPosition = m_InputCamera.ScreenToWorldPoint(Input.mousePosition);
                inputPosition.y = 0;
                m_CurrentSelectedPiece.transform.position = inputPosition;
            }
        }

        private void ReleasePiece()
        {
            //Todo : ReleaseOnPuzzleFrame / Swap / Release in bound
            m_CurrentSelectedPiece = null;
        }

        public void Initialize(PuzzlePiece[] pieces)
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
            m_CurrentSelectedPiece.name.Log("Select");
        }
    }
}