namespace PuzzleFrenzy.Scripts
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class PieceVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_Renderer = null;
        [SerializeField] private MeshFilter m_MeshFilter = null;
        public Mesh Mesh => m_MeshFilter.mesh;

        public void SetUp(Vector2Int pos,float size)
        {
            Mesh pieceMesh = Instantiate(m_MeshFilter.mesh);

            Vector2[] uv = new Vector2[pieceMesh.vertices.Length];

            for (int i = 0; i < pieceMesh.vertices.Length; i++)
            {
                pieceMesh.vertices[i].Log("Position");
                Vector2 globalPos = pos + new Vector2(pieceMesh.vertices[i].x,pieceMesh.vertices[i].z);

                float u = globalPos.x / size;
                float v = globalPos.y / size;

                uv[i] = new Vector2(u, v);
            }

            pieceMesh.uv = uv;
            m_MeshFilter.mesh = pieceMesh;
        }
    }
}