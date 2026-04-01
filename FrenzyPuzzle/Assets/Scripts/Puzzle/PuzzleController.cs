namespace PuzzleFrenzy.Scripts.Puzzle
{
    using KarpysDev.KarpysUtils.TweenCustom;
    using UI;
    using UnityEngine;

    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private UIWorldController m_UIWorldController = null;
        [SerializeField] private PuzzlePieceSelector m_Selector = null;
        [SerializeField] private WinController m_WinController = null;
        [SerializeField] private MeshRenderer m_PuzzleRenderer = null;
        
        private PuzzlePieceHolder[] m_Holders = null;
        private PuzzlePiece[] m_Pieces = null;

        public PuzzlePieceSelector Selector => m_Selector;

        public void SetPuzzleTexture(Texture texture)
        {
            m_PuzzleRenderer.material.SetTexture("_MainTex",texture);
        }
        
        public void AssignHolders(PuzzlePieceHolder[] holders)
        {
            m_Holders = holders;

            foreach (PuzzlePieceHolder pieceHolder in m_Holders)
            {
                pieceHolder.A_OnPlaceNewPiece += CheckForWin;
            }
        }

        private void CheckForWin()
        {
            if (CheckIsValid())
            {
                m_UIWorldController.Hide();
                m_Selector.SetSelect(false);
                StartCoroutine(m_WinController.TriggerWin());
            }
        }

        private bool CheckIsValid()
        {
            foreach (PuzzlePieceHolder puzzlePieceHolder in m_Holders)
            {
                if (!puzzlePieceHolder.IsValid)
                    return false;
            }

            return true;
        }

        public void SetPieces(PuzzlePiece[] pieces)
        {
            m_Pieces = pieces;
        }

        public void HideNoEdge()
        {
            if(m_Pieces == null)
                return;
            
            foreach (PuzzlePiece puzzlePiece in m_Pieces)
            {
                if (!puzzlePiece.IsEdge)
                {
                    puzzlePiece.TryHide();
                }
            }
        }

        public void DisplayNoEdge()
        {
            if(m_Pieces == null)
                return;
            
            foreach (PuzzlePiece puzzlePiece in m_Pieces)
            {
                if (!puzzlePiece.IsEdge)
                {
                    puzzlePiece.TryDisplay();
                }
            }
        }

        public void TryDisplayAllPiece()
        {
            foreach (PuzzlePiece puzzlePiece in m_Pieces)
            {
                puzzlePiece.TryDisplay();
            }
        }

        public void HideAllPiece()
        {
            foreach (PuzzlePiece puzzlePiece in m_Pieces)
            {
                puzzlePiece.TryHide();
            }
        }

        public void DisplayPuzzle()
        {
            m_PuzzleRenderer.material.DoShaderValue("_Alpha", 0, 1, 0.25f);
        }

        public void HidePuzzle()
        {
            m_PuzzleRenderer.material.DoShaderValue("_Alpha", 1, 0, 0.25f);
        }

        public void SetSelect(bool canSelect)
        {
            m_Selector.SetSelect(canSelect);
        }
    }
}