namespace PuzzleFrenzy.Scripts
{
    using System.Collections;
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class PuzzleCreationTransitionController : MonoBehaviour
    {
        [SerializeField] private PuzzleCreator m_PuzzleCreator = null;
        [SerializeField] private Transform m_PuzzleFrame = null;
        [SerializeField] private SpriteRenderer m_FrameRenderer = null;

        [SerializeField] private Transform[] m_Buttons = null;
        
        public IEnumerator CreatePuzzleScene(int index, ITextureProvider textureProvider, Vector2Int puzzleSize)
        {
            float delay = 0;

            for (int i = 0; i < m_Buttons.Length; i++)
            {
                m_Buttons[i].DoScale(Vector3.zero, GlobalVariables.ButtonScaleDownDuration)
                    .SetEase(GlobalVariables.ButtonScaleDownEase).SetDelay(delay);
                delay += 0.1f;
            }
            
            yield return new WaitForSeconds(2f);
            m_PuzzleCreator.Create(textureProvider, puzzleSize);
            m_PuzzleFrame.gameObject.SetActive(true);
            m_FrameRenderer.color = Color.white.setAlpha(0);
            m_FrameRenderer.DoColor(Color.white, 0.5f);
        } 
    }
}