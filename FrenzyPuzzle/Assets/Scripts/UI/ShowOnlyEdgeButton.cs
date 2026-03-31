namespace PuzzleFrenzy.Scripts.UI
{
    using Puzzle;
    using UnityEngine;

    public class ShowOnlyEdgeButton : MonoBehaviour
    {
        [SerializeField] private Transform m_Rotator = null;
        [SerializeField] private PuzzleController m_Controller = null;

        private bool m_IsActive = false;

        public void OnClick()
        {
            if (m_IsActive)
            {
                DisplayNoEdge();
            }
            else
            {
                HideNoEdge();
            }
            
            m_IsActive = !m_IsActive;
        }

        private void DisplayNoEdge()
        {
            m_Rotator.gameObject.SetActive(false);
            m_Controller.DisplayNoEdge();
        }

        private void HideNoEdge()
        {
            m_Rotator.gameObject.SetActive(true);
            m_Controller.HideNoEdge();
        }
    }
}