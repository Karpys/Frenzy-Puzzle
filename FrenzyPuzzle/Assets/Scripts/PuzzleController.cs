namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzleController : MonoBehaviour
    {
        [SerializeField] private PuzzleCreator m_PuzzleCreator = null;
        
        private PuzzlePieceHolder[] m_Holders = null;

        private void Start()
        {
            m_PuzzleCreator.Create();
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
            if(CheckIsValid())
                Debug.Log("Win");
        }
        
        public bool CheckIsValid()
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