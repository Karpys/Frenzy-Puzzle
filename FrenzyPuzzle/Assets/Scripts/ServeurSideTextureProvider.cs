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
        private int m_RequestId = 0;

        public ServeurSideTextureProvider(int requestId)
        {
            m_RequestId = requestId;
        }

        public void GetTexture(Action<Texture> Result)
        {
            DailyPuzzleApiRequest.RequestPuzzle(new HttpClient(),m_RequestId, Result);
        }
    }
}