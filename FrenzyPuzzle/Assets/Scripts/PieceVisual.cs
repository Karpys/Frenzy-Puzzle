namespace PuzzleFrenzy.Scripts
{
    using KarpysDev.KarpysUtils.TweenCustom;
    using UnityEngine;

    public class PieceVisual : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_Renderer = null;

        public void SetUp(Vector2Int pos,float size)
        {
            m_Renderer.material.SetFloat("_Size",size);
            m_Renderer.material.SetVector("_Position",new Vector4(pos.x,pos.y,0,0));
        }
    }
}