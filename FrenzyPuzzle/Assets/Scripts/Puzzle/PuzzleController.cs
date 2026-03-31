namespace PuzzleFrenzy.Scripts.Puzzle
{
    using UnityEngine;

    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private PuzzlePieceSelector m_Selector = null;
        [SerializeField] private WinController m_WinController = null;
        
        private PuzzlePieceHolder[] m_Holders = null;
        private PuzzlePiece[] m_Pieces = null;

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
                    puzzlePiece.Hide();
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
                    puzzlePiece.Display();
                }
            }
        }
    }
}