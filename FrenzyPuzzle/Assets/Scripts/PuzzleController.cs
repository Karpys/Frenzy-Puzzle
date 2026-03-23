namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzleController : MonoBehaviour
    {
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
            if(CheckIsValid())
                Debug.Log("Win");
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