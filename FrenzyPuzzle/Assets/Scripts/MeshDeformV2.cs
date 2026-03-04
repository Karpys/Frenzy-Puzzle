namespace PuzzleFrenzy.Scripts
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class MeshDeformV2 : BaseDeform
    {
        [SerializeField] private float m_Radius = 1;
        [SerializeField] private float m_StartRadius = 240;
        [SerializeField] private float m_TotalRadius = 300;
        
        private static readonly int[] TOP_VERTICES = {27,29,31,35,37,40,42,45,47,49,51,53,57,59,61,63,66};
        private static readonly int[] RIGHT_VERTICES = {91,95,99,103,107,111,115,119,123,127,131,135,139,143,147,151,155};
        private static readonly int[] BOTTOM_VERTICES = {180,182,184,186,189,190,192,194,196,198,200,202,205,206,208,210,212};
        private static readonly int[] LEFT_VERTICES = {85,89,93,97,101,105,109,113,117,121,125,129,133,137,141,145,149};
        
        public override void ApplyDeform(int[] deform, Mesh mesh)
        {
            Vector3[] vertices = mesh.vertices;
            
            for (int i = 0; i < deform.Length; i++)
            {
                if(deform[i] == 0)
                    continue;
                int xMult = 0;
                int zMult = 0;
                int[] verticesApply = null;
                
                switch (i)
                {
                    case 0:
                        zMult = deform[i] > 0  ? 1 : -1;
                        verticesApply = TOP_VERTICES;
                        break;
                    case 1:
                        xMult = deform[i] > 0  ? 1 : -1;
                        verticesApply = RIGHT_VERTICES;
                        break;
                    case 2:
                        zMult = deform[i] > 0  ? -1 : 1;
                        verticesApply = BOTTOM_VERTICES;
                        break;
                    case 3:
                        xMult = deform[i] > 0  ? -1 : 1;
                        verticesApply = LEFT_VERTICES;
                        break;
                    
                }
                
                float radius = m_StartRadius;
                float incr = m_TotalRadius / (verticesApply.Length - 1);
                
                verticesApply.Length.Log("Length");
                for (int j = 0; j < verticesApply.Length; j++)
                {
                    vertices[verticesApply[j]].z += Mathf.Sin(Mathf.Deg2Rad * radius) * m_Radius * zMult;
                    vertices[verticesApply[j]].x += Mathf.Sin(Mathf.Deg2Rad * radius) * m_Radius * xMult;
                    radius -= incr;
                }
            }
            
            mesh.vertices = vertices;
        }

    }
}