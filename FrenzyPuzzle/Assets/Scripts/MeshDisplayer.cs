namespace PuzzleFrenzy.Scripts
{
    using System;
    using UnityEngine;

    public class MeshDisplayer : MonoBehaviour
    {
        [SerializeField] private MeshFilter m_MeshFilter = null;
        [SerializeField] private MeshDeform m_MeshDeform = null;
        [SerializeField] private TextDisplay m_TextDisplayPrefab = null;
        [SerializeField] private int[] m_Deform = null;

        [ContextMenu("Generate")]
        private void Generate()
        {
            for (int i = 0; i < m_MeshFilter.mesh.vertices.Length; i++)
            {
                TextDisplay display = Instantiate(m_TextDisplayPrefab, transform);
                display.transform.localPosition = m_MeshFilter.sharedMesh.vertices[i];
                display.Initialize(i.ToString());
            }
        }

        [ContextMenu("Deform")]
        public void Deform()
        {
            m_MeshDeform.ApplyDeform(m_Deform,m_MeshFilter.mesh);
        }

    }
}