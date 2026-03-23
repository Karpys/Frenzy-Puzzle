namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzleSelectionController : MonoBehaviour
    {
        [SerializeField] private Transform m_UI = null;
        [SerializeField] private PuzzleCreator m_PuzzleCreator = null;
        [SerializeField] private Transform m_PuzzleFrame = null;

        public void Load(int puzzleIndex)
        {
            ITextureProvider textureProvider = new ServeurSideTextureProvider(puzzleIndex);

            Vector2Int puzzleSize = Vector2Int.zero;
            
            switch (puzzleIndex)
            {
                case 0:
                    puzzleSize = Vector2Int.one * 4;
                    break;
                case 1:
                    puzzleSize = Vector2Int.one * 6;
                    break;
                case 2:
                    puzzleSize = Vector2Int.one * 8;
                    break;
            }

            m_PuzzleCreator.Create(textureProvider, puzzleSize);
            m_UI.gameObject.SetActive(false);
            m_PuzzleFrame.gameObject.SetActive(true);
        }
    }
}