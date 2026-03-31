namespace PuzzleFrenzy.Scripts.Puzzle
{
    using TextureProvider;
    using UnityEngine;

    public class PuzzleSelectionController : MonoBehaviour
    {
        [SerializeField] private PuzzleCreationTransitionController m_PuzzleCreationTransitionController = null;

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


            StartCoroutine(m_PuzzleCreationTransitionController.CreatePuzzleScene(puzzleIndex,textureProvider, puzzleSize));
        }
    }
}