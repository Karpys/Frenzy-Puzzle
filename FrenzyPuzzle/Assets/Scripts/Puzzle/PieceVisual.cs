namespace PuzzleFrenzy.Scripts.Puzzle
{
    using KarpysDev.KarpysUtils;
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class PieceVisual : MonoBehaviour
    {
        [SerializeField] private Transform m_ScaleParent = null;
        [SerializeField] private MeshRenderer m_Renderer = null;
        [SerializeField] private MeshRenderer m_Outline = null;
        [SerializeField] private MeshFilter m_MeshFilter = null;
        public Mesh Mesh => m_MeshFilter.mesh;

        public void SetUp(Vector2Int pos, Vector2Int size, float scaleSize, Texture puzzleTexture)
        {
            Mesh pieceMesh = Instantiate(m_MeshFilter.mesh);

            Vector2[] uv = new Vector2[pieceMesh.vertices.Length];

            for (int i = 0; i < pieceMesh.vertices.Length; i++)
            {
                Vector2 globalPos = pos + new Vector2(pieceMesh.vertices[i].x,pieceMesh.vertices[i].z);

                float u = globalPos.x / size.x;
                float v = globalPos.y / size.y;

                uv[i] = new Vector2(u, v);
            }

            pieceMesh.uv = uv;
            m_MeshFilter.mesh = pieceMesh;

            m_ScaleParent.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
            
            m_Renderer.material.SetTexture("_BaseMap",puzzleTexture);
        }

        public void Hide()
        {
            m_Outline.material.DoShaderValue("_Alpha", 1, 0, 0.25f);
            m_Renderer.material.DoShaderValue("_Alpha", 1, 0, 0.25f);
        }

        public void Display()
        {
            m_Outline.material.DoShaderValue("_Alpha", 0, 1, 0.25f);
            m_Renderer.material.DoShaderValue("_Alpha", 0, 1, 0.25f);
        }
    }
}