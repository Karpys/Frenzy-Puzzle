namespace PuzzleFrenzy.Scripts
{
    using System;
    using UnityEngine;

    public interface ITextureProvider
    {
        public Texture Texture { get; }
        public bool IsAsync { get; }
        void GetTexture(Action<Texture> result);
    }
}