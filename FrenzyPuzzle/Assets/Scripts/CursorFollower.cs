namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class CursorFollower : MonoBehaviour
    {
        [SerializeField] private Camera m_InputCamera = null;
        private void Update()
        {
            Vector3 pos = m_InputCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.y = transform.position.y;
            transform.position = pos;
        }
    }
}