namespace PuzzleFrenzy.Scripts.MeshDeform
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class MeshDeformOutline : BaseDeform
    {
        [SerializeField] private float m_Radius = 1;
        [SerializeField] private float m_StartRadius = 240;
        [SerializeField] private float m_TotalRadius = 300;
        [SerializeField] private float m_BumpOffset = 0.1f;
        [SerializeField] private float m_HoleOffset = 0.1f;
        [SerializeField] private int m_VerticeRemoveHole = 0;
        [SerializeField] private int m_VerticeRemoveBump = 0;
        
        private static readonly int[] TOP_VERTICES = {27,29,31,35,37,40,42,45,47,49,51,53,57,59,61,63,66};
        private static readonly int[] RIGHT_VERTICES = {91,95,99,103,107,111,115,119,123,127,131,135,139,143,147,151,155};
        private static readonly int[] BOTTOM_VERTICES = {180,182,184,186,189,190,192,194,196,198,200,202,205,206,208,210,212};
        private static readonly int[] LEFT_VERTICES = {85,89,93,97,101,105,109,113,117,121,125,129,133,137,141,145,149};
        
        public override void ApplyDeform(int[] deform, Mesh mesh)
        {
            Vector3[] vertices = mesh.vertices;
            int verticesRemove = 0;
            
            for (int i = 0; i < deform.Length; i++)
            {
                if(deform[i] == 0)
                    continue;
                int xMult = 0;
                int zMult = 0;
                float xOffset = 0;
                float zOffset = 0;
                int[] verticesApply = null;
                
                switch (i)
                {
                    case 0:
                        zMult = deform[i] > 0  ? 1 : -1;
                        verticesApply = TOP_VERTICES;
                        zOffset = zMult == 1 ? m_BumpOffset : m_HoleOffset;
                        break;
                    case 1:
                        xMult = deform[i] > 0  ? 1 : -1;
                        verticesApply = RIGHT_VERTICES;
                        xOffset = xMult == 1 ? m_BumpOffset : m_HoleOffset;
                        break;
                    case 2:
                        zMult = deform[i] > 0  ? -1 : 1;
                        verticesApply = BOTTOM_VERTICES;
                        zOffset = zMult == -1 ? -m_BumpOffset : -m_HoleOffset;
                        break;
                    case 3:
                        xMult = deform[i] > 0  ? -1 : 1;
                        verticesApply = LEFT_VERTICES;
                        xOffset = xMult == -1 ? -m_BumpOffset : -m_HoleOffset;
                        break;
                }

                verticesRemove = deform[i] == -1 ? m_VerticeRemoveHole : m_VerticeRemoveBump;
                float radius = m_StartRadius;
                float incr = m_TotalRadius / (verticesApply.Length - 1 - verticesRemove *2);
                
                for (int j = verticesRemove; j < verticesApply.Length - verticesRemove; j++)
                {
                    vertices[verticesApply[j]].z += Mathf.Sin(Mathf.Deg2Rad * radius) * m_Radius * zMult + zOffset;
                    vertices[verticesApply[j]].x += Mathf.Sin(Mathf.Deg2Rad * radius) * m_Radius * xMult + xOffset;
                    radius -= incr;
                }
            }
            
            mesh.vertices = vertices;
        }
    }
}