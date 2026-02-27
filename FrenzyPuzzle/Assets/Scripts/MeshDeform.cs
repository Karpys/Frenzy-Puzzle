namespace PuzzleFrenzy.Scripts
{
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class MeshDeform : MonoBehaviour
    {
        [SerializeField] private float m_Force = 1;
        [SerializeField] private float m_ForceMiddle = 0.5f;
        [SerializeField] private float m_ForceLow = 0.25f;
        
        private static readonly int[] TOP_VERTICES = { 9, 10, 13, 15, 17};
        private static readonly int[] RIGHT_VERTICES = { 30, 34, 38, 42, 46};
        private static readonly int[] BOTTOM_VERTICES = { 59, 61, 63, 65, 67};
        private static readonly int[] LEFT_VERTICES = { 11, 28, 32, 36, 40};
        
        public void ApplyDeform(int[] deform, Mesh mesh)
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
                
                for (int j = 0; j < 3; j++)
                {
                    switch (j)
                    {
                        case 0:
                            vertices[verticesApply[2]].x += m_Force * xMult;
                            vertices[verticesApply[2]].z += m_Force * zMult;
                            break;
                        case 1:
                            vertices[verticesApply[1]].x += m_ForceMiddle * xMult;
                            vertices[verticesApply[1]].z += m_ForceMiddle * zMult;
                            vertices[verticesApply[3]].x += m_ForceMiddle * xMult;
                            vertices[verticesApply[3]].z += m_ForceMiddle * zMult;
                            break;
                        case 2:
                            vertices[verticesApply[0]].x += m_ForceLow * xMult;
                            vertices[verticesApply[0]].z += m_ForceLow * zMult;
                            vertices[verticesApply[4]].x += m_ForceLow * xMult;
                            vertices[verticesApply[4]].z += m_ForceLow * zMult;
                            break;
                    }
                }
            }
            
            mesh.vertices = vertices;
        }
    }
}