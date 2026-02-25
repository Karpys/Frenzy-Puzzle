namespace PuzzleFrenzy.Scripts
{
    using Helpers;
    using UnityEngine;

    public class PuzzlePieceHolderController : MonoBehaviour
    {
        private PuzzlePieceHolder[] m_Holders = null;

        public void SetHolders(PuzzlePieceHolder[] holders)
        {
            m_Holders = holders;
        }

        public PuzzlePieceHolder GetClosestHolder(Vector3 position,out float distance)
        {
            return m_Holders[m_Holders.GetClosestViaId(position,out distance)];
        }
    }
}