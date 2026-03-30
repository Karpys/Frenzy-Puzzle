namespace PuzzleFrenzy.Scripts
{
    using System;
    using System.Collections;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;
    using UnityEngine.Networking;
    using Random = UnityEngine.Random;

    public class PuzzleCreator : MonoBehaviour
    {
        [SerializeField] private Loading m_Loading = null;
        [SerializeField] private PuzzleController m_PuzzleController = null;
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
        [SerializeField] private Bound[] m_Bounds = null;
        private PuzzleGenerator m_PuzzleGenerator = null;

        private IEnumerator Load()
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(m_Url);
            yield return webRequest.SendWebRequest();
            Texture texture = DownloadHandlerTexture.GetContent(webRequest);
            CreatePuzzle(texture);
        }

        public void Create(ITextureProvider textureProvider, Vector2Int puzzleSize)
        {
            m_PuzzleSize = puzzleSize;
            CreatePuzzleAsync(textureProvider,CreatePuzzle);
            m_Loading.LaunchLoading();
        }

        private void CreatePuzzleAsync(ITextureProvider textureProvider, Action<Texture> Result)
        {
            textureProvider.GetTexture(Result);
        }
        
        private void CreatePuzzle(Texture puzzleTexture)
        {
            StartCoroutine(CO_CreatePuzzle(puzzleTexture));
        }

        private IEnumerator CO_CreatePuzzle(Texture puzzleTexture)
        {
            m_Loading.Stop();

            while (!m_Loading.HasStopLoading)
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            
            m_PuzzleGenerator = new PuzzleGenerator(m_PuzzleSize);
            CreateAndAssignPieces(m_PuzzleGenerator.PuzzleData,puzzleTexture);
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
            m_PuzzleController.AssignHolders(holders);
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

            float timeAppear = 1f;
            float delay = 0f;
            float additionalDelay = timeAppear / pieces.Length;
            
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].transform.localScale = Vector3.zero;

                if (i == pieces.Length - 1)
                {
                    pieces[i].transform.DoScale(Vector3.one, 0.25f).SetDelay(delay).OnComplete(OnLastPieceCreated);
                }
                else
                {
                    pieces[i].transform.DoScale(Vector3.one, 0.25f).SetDelay(delay);
                }
                delay += additionalDelay;
            }
        }

        private void OnLastPieceCreated()
        {
            m_PuzzlePieceSelector.SetSelect(true);
        }

        private void AssignPiece(PuzzlePiece[] pieces)
        {
            m_PuzzlePieceSelector.SetPieces(pieces);
        }

        private Vector3 GetBoundPosition()
        {
            Bound bound = m_Bounds[Random.Range(0, m_Bounds.Length)];
            return bound.SelectPointInside();
        }
    }
}