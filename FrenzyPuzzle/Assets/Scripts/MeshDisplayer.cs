namespace PuzzleFrenzy.Scripts
{
    using System;
    using UnityEngine;

    public class MeshDisplayer : MonoBehaviour
    {
        [SerializeField] private MeshFilter m_MeshFilter = null;
        // [SerializeField] private MeshDeform m_MeshDeform = null;
        [SerializeField] private MeshDeformV2 m_MeshDeformV2 = null;
        [SerializeField] private TextDisplay m_TextDisplayPrefab = null;
        [SerializeField] private int[] m_Deform = null;
        [SerializeField] private Transform m_TextParent = null;
        [SerializeField] private Mesh m_BaseMesh = null;

        [ContextMenu("Generate")]
        private void Generate()
        {
            int childCount = m_TextParent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(m_TextParent.GetChild(0).transform.gameObject);                
            }
            
            for (int i = 0; i < m_MeshFilter.mesh.vertices.Length; i++)
            {
                TextDisplay display = Instantiate(m_TextDisplayPrefab, m_TextParent);
                display.transform.localPosition = m_MeshFilter.sharedMesh.vertices[i];
                display.Initialize(i.ToString());
            }
        }

        [ContextMenu("Deform")]
        public void Deform()
        {
            m_MeshFilter.mesh = m_BaseMesh;
            m_MeshDeformV2.ApplyDeform(m_Deform,m_MeshFilter.mesh);
        }

    }
}