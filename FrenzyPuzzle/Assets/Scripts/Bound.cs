namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    [System.Serializable]
    public struct Bound
    {
        [SerializeField] private Transform m_MinBound;
        [SerializeField] private Transform m_MaxBound;

        public Transform MinBound => m_MinBound;
        public Transform MaxBound => m_MaxBound;


        public Vector3 SelectPointInside()
        {
            return new Vector3(Random.Range(m_MinBound.position.x,m_MaxBound.position.x), 0, Random.Range(m_MinBound.position.z,m_MaxBound.position.z));
        }
    }
}