namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private PuzzlePieceSelector m_Selector = null;
        [SerializeField] private WinController m_WinController = null;
        
        private PuzzlePieceHolder[] m_Holders = null;

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
    }
}