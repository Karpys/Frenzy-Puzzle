namespace PuzzleFrenzy.Scripts.UI
{
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIWorldController : MonoBehaviour
    {
        [SerializeField] private Transform[] m_ButtonsTransforms = null;
        [SerializeField] private Button[] m_Buttons = null;
        [SerializeField] private Ease m_DisplayEase = Ease.LINEAR;
        [SerializeField] private Ease m_HideEase = Ease.LINEAR;

        public void Display()
        {
            for (int i = 0; i < m_ButtonsTransforms.Length; i++)
            {
                Transform button = m_ButtonsTransforms[i];
                button.DoScale(Vector3.one, 0.25f).SetEase(m_DisplayEase);
                m_Buttons[i].interactable = true;
            }
        }

        public void Hide()
        {
            for (int i = 0; i < m_ButtonsTransforms.Length; i++)
            {
                Transform button = m_ButtonsTransforms[i];
                button.DoScale(Vector3.zero, 0.25f).SetEase(m_HideEase);
                m_Buttons[i].interactable = false;
            }
        }
    }
}