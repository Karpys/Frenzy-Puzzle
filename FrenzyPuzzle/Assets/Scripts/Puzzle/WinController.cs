namespace PuzzleFrenzy.Scripts.Puzzle
{
    using System.Collections;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class WinController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera = null;
        [SerializeField] private float m_WinSize = 7.5f;
        [SerializeField] private float m_OrthoSizeWinDuration = 0.25f;
        [SerializeField] private Ease m_OrthoSizeWinEase = Ease.LINEAR;
        [SerializeField] private ParticleSystem m_ParticleSystem = null;
        public IEnumerator TriggerWin()
        {
            yield return new WaitForSeconds(1f);
            m_Camera.transform.DoFloatValue(v => m_Camera.orthographicSize = v, m_Camera.orthographicSize, 
                m_WinSize, m_OrthoSizeWinDuration).SetEase(m_OrthoSizeWinEase);
            m_ParticleSystem.Play();
        }
    }
}