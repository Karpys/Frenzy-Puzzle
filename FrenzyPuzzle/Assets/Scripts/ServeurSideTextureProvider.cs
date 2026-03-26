namespace PuzzleFrenzy.Scripts
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Api;
    using UnityEngine;

    public class ServeurSideTextureProvider : ITextureProvider
    {
        public Texture Texture { get; }
        public bool IsAsync => true;
        public bool IsCompleted => m_IsCompleted;

        private int m_RequestId = 0;
        private Task m_CurrentTask = null;
        private bool m_IsCompleted = false;

        public ServeurSideTextureProvider(int requestId)
        {
            m_RequestId = requestId;
        }

        public void GetTexture(Action<Texture> Result)
        {
            Result += _ => CompleteTask();
            m_CurrentTask = DailyPuzzleApiRequest.RequestPuzzle(new HttpClient(),m_RequestId, Result);
        }


        private void CompleteTask()
        {
            m_IsCompleted = true;
        }
    }
}