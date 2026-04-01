namespace PuzzleFrenzy.Scripts.UI
{
    using System;
    using Puzzle;
    using UnityEngine;

    public class ShowPuzzleButton : MonoBehaviour
    {
        [SerializeField] private Transform m_Rotator = null;
        [SerializeField] private PuzzleController m_Controller = null;
        
        private bool m_IsActive = false;

        private void Awake()
        {
            m_Controller.Selector.A_OnTrySelectWhileLocked += TryUnlock;
        }

        private void TryUnlock()
        {
            if (m_IsActive)
            {
                OnClick();
                m_Controller.SetSelect(true);
            }
        }

        public void OnClick()
        {
            if (m_IsActive)
            {
                HideFrame();
            }
            else
            {
                DisplayFrame();
            }
            
            m_IsActive = !m_IsActive;
        }

        private void DisplayFrame()
        {
            m_Controller.SetSelect(false);
            m_Controller.DisplayPuzzle();
            m_Rotator.gameObject.SetActive(true);
        }

        private void HideFrame()
        {
            m_Controller.SetSelect(true);
            m_Controller.HidePuzzle();
            // m_Controller.HideAllPiece();
            m_Rotator.gameObject.SetActive(false);
        }
    }
}