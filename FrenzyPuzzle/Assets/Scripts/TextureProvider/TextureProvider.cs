namespace PuzzleFrenzy.Scripts.TextureProvider
{
    using System;
    using UnityEngine;

    public class TextureProvider : MonoBehaviour
    {
        [SerializeField] private TextureProviderType m_Type = TextureProviderType.Sprite;

        [Header("Provider Type")]
        [SerializeField] private int m_PuzzleId = 0;
        [SerializeField] private Sprite m_DefaultSprite = null;

        private ITextureProvider m_TextureProvider = null;

        private void Awake()
        {
            switch (m_Type)
            {
                case TextureProviderType.Sprite:
                    m_TextureProvider = new PuzzleTextureProvider(m_DefaultSprite);
                    break;
                case TextureProviderType.ServerSide:
                    m_TextureProvider = new ServeurSideTextureProvider(m_PuzzleId);
                    break;
            }
        }

        public void CreatePuzzle(Action<Texture> Result)
        {
            m_TextureProvider.GetTexture(Result);
        }
    }
}