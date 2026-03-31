namespace PuzzleFrenzy.Scripts.Utils
{
    using UnityEngine;

    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class HorizontalFOVLock : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] private bool m_UpdateInEditor = false;
        #endif
        [SerializeField] private bool m_UseReferenceRatio = false;
        [SerializeField] private float m_HorizontalSize = 10f;
        [SerializeField] private Vector2 m_ReferenceRatio = new Vector2(1080, 1920);

        public float HorizontalSize { get => m_HorizontalSize; set { m_HorizontalSize = value; Refresh(); } }

        private void Awake()
        {
            Refresh();
        }

        
        public void Refresh()
        {
            Camera c = GetComponent<Camera>();
            float ratio = c.aspect;

            float referenceRatio = m_ReferenceRatio.x / m_ReferenceRatio.y;

            if (m_UseReferenceRatio && ratio > referenceRatio)
            {
                c.orthographicSize = HorizontalSize / referenceRatio;
            }
            else
            {
                c.orthographicSize = HorizontalSize / c.aspect;
            }
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if(m_UpdateInEditor)
                Refresh();
        }
        #endif
    }
}