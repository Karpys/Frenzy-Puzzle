namespace PuzzleFrenzy.Scripts
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine;

    public class PuzzleTextureProvider : ITextureProvider
    {
        private Sprite m_Sprite = null;
        public Texture Texture => m_Sprite.texture;
        public bool IsAsync => false;
        public bool IsCompleted => true;

        public void GetTexture(Action<Texture> result)
        {
            result?.Invoke(Texture);
        }

        public PuzzleTextureProvider(Sprite sprite)
        {
            m_Sprite = sprite;
        }
    }
}