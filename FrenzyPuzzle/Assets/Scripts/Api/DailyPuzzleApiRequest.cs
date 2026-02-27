namespace PuzzleFrenzy.Scripts.Api
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class DailyPuzzleApiRequest
    {
        public static async Task RequestPuzzle(HttpClient client,Action<Texture> OnPuzzleFound)
        {
            string request = "https://dailypuzzleapi.onrender.com/images/PuzzleDemo.png";
            HttpResponseMessage response = await client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                byte[] imageBytes = await client.ReadFile(request);
                OnPuzzleFound?.Invoke(ByteImageToSprite(imageBytes));
            }
            else
            {
                Debug.Log("Fail");
            }
        }
        
        public static async Task<byte[]> ReadFile(this HttpClient client,string url)
        {
            byte[] fileData = await client.GetByteArrayAsync(new Uri(url));
            return fileData;
        }
        
        public static Texture2D ByteImageToSprite(this byte[] imageData, int width = 256, int height = 256)
        {
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(imageData);
            return texture;
        }
    }
}