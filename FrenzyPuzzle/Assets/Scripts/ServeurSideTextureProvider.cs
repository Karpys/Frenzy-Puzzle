namespace PuzzleFrenzy.Scripts
{
    using System;
    using System.Net.Http;
    using Api;
    using UnityEngine;

    public class ServeurSideTextureProvider : ITextureProvider
    {
        public Texture Texture { get; }
        public bool IsAsync => true;

        public void GetTexture(Action<Texture> Result)
        {
            DailyPuzzleApiRequest.RequestPuzzle(new HttpClient(), Result);
        }
    }
}