namespace PuzzleFrenzy.Scripts
{
    using System;
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;
    using UnityEngine.UI;

    public class Loading : MonoBehaviour
    {
        [SerializeField] private Image[] m_Images = null;
        [SerializeField] private Transform m_Holder = null;
        [SerializeField] private Transform[] m_OutPlaces = null;
        [SerializeField] private Transform[] m_InPlaces = null;
        [SerializeField] private Transform[] m_Holders = null;

        [SerializeField] private float m_RotateDuration = 0.25f;
        [SerializeField] private Ease m_RotateEase = Ease.LINEAR;

        [SerializeField] private float m_InitialOutDelay = 0f;
        [SerializeField] private float m_OutDuration = 0.5f;
        [SerializeField] private float m_AdditionalOutDelay = 0.1f;
        [SerializeField] private Ease m_OutEase = Ease.LINEAR;
        
        [SerializeField] private float m_InitialInDelay = 0f;
        [SerializeField] private float m_InDuration = 0.5f;
        [SerializeField] private float m_AdditionalInDelay = 0.1f;
        [SerializeField] private Ease m_InEase = Ease.LINEAR;

        private void LaunchLoading()
        {
            Init();
            MoveOut(0);

            foreach (var image in m_Images)
            {
                image.color = Color.white.setAlpha(0);

                image.DoColor(Color.white, 0.25f);
            }
        }

        private void StopLoading()
        {
            transform.DoKill();

            foreach (Transform holder in m_Holders)
            {
                holder.DoKill();
            }
        }

        private void Init()
        {
            for (int i = 0; i < m_Holders.Length; i++)
            {
                m_Holders[i].transform.position = m_InPlaces[i].transform.position;
            }
        }
        
        
        private void MoveOut(float delay)
        {
            for (int i = 0; i < m_Holders.Length; i++)
            {
                if (i == m_Holders.Length - 1)
                {
                    m_Holders[i].transform.DoMove(m_OutPlaces[i].transform.position, m_OutDuration).SetEase(m_OutEase)
                        .SetDelay(delay).OnComplete(Rotate);
                }
                else
                {
                    m_Holders[i].transform.DoMove(m_OutPlaces[i].transform.position, m_OutDuration).SetEase(m_OutEase)
                        .SetDelay(delay);
                }
                
                delay += m_AdditionalOutDelay;
            }
        }

        private void MoveIn(float delay)
        {
            for (int i = 0; i < m_Holders.Length; i++)
            {
                if (i == m_Holders.Length - 1)
                {
                    m_Holders[i].transform.DoMove(m_InPlaces[i].transform.position, m_InDuration).SetEase(m_InEase)
                        .SetDelay(delay).OnComplete(() => MoveOut(m_InitialOutDelay));
                }
                else
                {
                    m_Holders[i].transform.DoMove(m_InPlaces[i].transform.position, m_InDuration).SetEase(m_InEase)
                        .SetDelay(delay);
                }
                
                delay += m_AdditionalInDelay;
            }
        }

        private void Rotate()
        {
            m_Holder.DoRotate(new Vector3(0, 0, -90), m_RotateDuration).SetEase(m_RotateEase)
                .OnComplete(() => MoveIn(m_InitialInDelay)).SetMode(TweenMode.ADDITIVE);
        }
    }
}