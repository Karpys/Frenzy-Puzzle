namespace PuzzleFrenzy.Scripts
{
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class GlobalVariableSetter : MonoBehaviour
    {
        [SerializeField] private float m_PuzzlePiecePlaceSpeed = 0.2f;
        [SerializeField] private Ease m_PuzzlePiecePlaceEase = Ease.LINEAR;
        [SerializeField] private float m_ButtonScaleDownDuration = 0.2f;
        [SerializeField] private Ease m_ButtonScaleDownEase = Ease.LINEAR;

        private void Awake()
        {
            Set();
        }

        private void Set()
        {
            GlobalVariables.PuzzlePiecePlaceSpeed = m_PuzzlePiecePlaceSpeed;
            GlobalVariables.PuzzlePiecePlaceEase = m_PuzzlePiecePlaceEase;
            GlobalVariables.ButtonScaleDownDuration = m_ButtonScaleDownDuration;
            GlobalVariables.ButtonScaleDownEase = m_ButtonScaleDownEase;
        }

        [ContextMenu("ResetValues")]
        public void ResetValues()
        {
            Set();
        }
    }
}