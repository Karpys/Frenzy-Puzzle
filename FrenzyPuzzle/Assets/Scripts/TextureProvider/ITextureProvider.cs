namespace PuzzleFrenzy.Scripts.TextureProvider
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine;

    public interface ITextureProvider
    {
        public Texture Texture { get; }
        public bool IsAsync { get; }
        public bool IsCompleted { get; }
        void GetTexture(Action<Texture> result);
    }
}