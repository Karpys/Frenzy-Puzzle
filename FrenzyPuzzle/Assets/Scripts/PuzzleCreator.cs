namespace PuzzleFrenzy.Scripts
{
    using System;
    using System.Collections;
    using System.Net.Http;
    using Api;
    using UnityEngine;
    using UnityEngine.Networking;
    using Random = UnityEngine.Random;

    public class PuzzleCreator : MonoBehaviour
    {
        [SerializeField] private string m_Url = String.Empty;
        [SerializeField] private Vector2Int m_PuzzleSize = Vector2Int.zero;
        [SerializeField] private PuzzleFrameResizer puzzleFrameResizer = null;
        [SerializeField] private PuzzlePiece m_PuzzlePiecePrefab = null;
        [SerializeField] private Transform m_PlacesHolder = null;
        [SerializeField] private PuzzlePieceHolder m_HolderPrefab = null;
        [SerializeField] private bool m_InBound = true;
        [SerializeField] private PuzzlePieceSelector m_PuzzlePieceSelector = null;
        [SerializeField] private PuzzlePieceHolderController m_PuzzlePieceHolderController = null;

        [Header("Places")]
        [SerializeField] private float m_BaseSize = 4;
        [SerializeField] private Transform m_PiecesHolder = null;
        [SerializeField] private Transform m_MinBound = null;
        [SerializeField] private Transform m_MaxBound = null;

        private PuzzleGenerator m_PuzzleGenerator = null;
        private void Awake()
        {
            DailyPuzzleApiRequest.RequestPuzzle(new HttpClient(),CreatePuzzle);
        }

        private IEnumerator Load()
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(m_Url);
            yield return webRequest.SendWebRequest();
            Texture texture = DownloadHandlerTexture.GetContent(webRequest);
            CreatePuzzle(texture);
        }

        private void CreatePuzzle(Texture puzzleSprite)
        {
            Debug.Log("Try Create Puzzle");
            m_PuzzleGenerator = new PuzzleGenerator(m_PuzzleSize);
            CreateAndAssignPieces(m_PuzzleGenerator.PuzzleData,puzzleSprite);
            CreateAndAssignHolders();
            puzzleFrameResizer.Resize(m_BaseSize,m_PuzzleSize);
        }

        private void CreateAndAssignHolders()
        {
            PuzzlePieceHolder[] holders = new PuzzlePieceHolder[m_PuzzleSize.x * m_PuzzleSize.y];

            int count = 0;
            for (int x = 0; x < m_PuzzleSize.x; x++)
            {
                for (int y = 0; y < m_PuzzleSize.y; y++)
                {
                    PuzzlePieceHolder holder = Instantiate(m_HolderPrefab, Vector3.zero, Quaternion.identity, m_PlacesHolder);
                    holder.transform.localPosition = new Vector3(x,0,y);
                    holder.Initialize(new Vector2Int(x,y));
                    holders[count] = holder;
                    count++;
                }
            }

            AssignHolders(holders);
        }

        private void AssignHolders(PuzzlePieceHolder[] holders)
        {
            m_PuzzlePieceHolderController.SetHolders(holders);
        }

        private void CreateAndAssignPieces(int[][][] puzzleGeneratorPuzzleData, Texture puzzleSprite)
        {
            PuzzlePiece[] pieces = new PuzzlePiece[m_PuzzleSize.x * m_PuzzleSize.y];
            int count = 0;
            
            float size = m_BaseSize / Math.Max(m_PuzzleSize.x, m_PuzzleSize.y);

            for (int x = 0; x < m_PuzzleSize.x; x++)
            {
                for (int y = 0; y < m_PuzzleSize.y; y++)
                {
                    Vector3 pos = new Vector3(x, 0, y);

                    if (m_InBound)
                        pos = GetBoundPosition();
                    
                    PuzzlePiece piece = Instantiate(m_PuzzlePiecePrefab, pos, Quaternion.identity, m_PiecesHolder);
                    piece.transform.localPosition = pos;
                    piece.Initialize(new Vector2Int(x,y),size,m_PuzzleSize,puzzleGeneratorPuzzleData[x][y],puzzleSprite);
                    pieces[count] = piece;
                    count++;
                }
            }

            AssignPiece(pieces);
        }

        private void AssignPiece(PuzzlePiece[] pieces)
        {
            m_PuzzlePieceSelector.SetPieces(pieces);
        }

        private Vector3 GetBoundPosition()
        {
            return new Vector3(Random.Range(m_MinBound.position.x,m_MaxBound.position.x), 0, Random.Range(m_MinBound.position.z,m_MaxBound.position.z));
        }
    }
}