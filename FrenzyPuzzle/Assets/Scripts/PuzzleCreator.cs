namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzleCreator : MonoBehaviour
    {
        [SerializeField] private Vector2Int m_PuzzleSize = Vector2Int.zero;
        [SerializeField] private PuzzlePiece m_PuzzlePiecePrefab = null;
        [SerializeField] private Transform m_PlacesHolder = null;
        [SerializeField] private Transform m_HolderPrefab = null;

        [Header("Places")]
        [SerializeField] private Transform m_PiecesHolder = null;
        [SerializeField] private Transform m_MinBound = null;
        [SerializeField] private Transform m_MaxBound = null;
        
        private void Awake()
        {
            CreatePieces();
            CreateHolders();
        }

        private void CreateHolders()
        {
            for (int x = 0; x < m_PuzzleSize.x; x++)
            {
                for (int y = 0; y < m_PuzzleSize.y; y++)
                {
                    Vector3 pos = GetBoundPosition();
                    Transform holder = Instantiate(m_HolderPrefab, pos, Quaternion.identity, m_PlacesHolder);
                    holder.transform.localPosition = m_PlacesHolder.localPosition + new Vector3(x,0,y);
                }
            }
        }

        private void CreatePieces()
        {
            for (int x = 0; x < m_PuzzleSize.x; x++)
            {
                for (int y = 0; y < m_PuzzleSize.y; y++)
                {
                    Vector3 pos = GetBoundPosition();
                    PuzzlePiece piece = Instantiate(m_PuzzlePiecePrefab, pos, Quaternion.identity, m_PiecesHolder);
                    piece.transform.localPosition = pos;
                    piece.Initialize(new Vector2Int(x,y),m_PuzzleSize.x);
                }
            }
        }

        private Vector3 GetBoundPosition()
        {
            return new Vector3(Random.Range(m_MinBound.position.x,m_MaxBound.position.x), 0, Random.Range(m_MinBound.position.z,m_MaxBound.position.z));
        }
    }
}