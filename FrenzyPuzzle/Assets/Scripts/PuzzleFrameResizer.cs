namespace PuzzleFrenzy.Scripts
{
    using System;
    using KarpysDev.KarpysUtils;
    using UnityEngine;

    public class PuzzleFrameResizer : MonoBehaviour
    {
        [SerializeField] private Transform m_ParentScale = null;
        [SerializeField] private Transform m_Place = null;
        
        public void Resize(float baseScale,Vector2Int puzzleSize)
        {
            float size = baseScale / Math.Max(puzzleSize.x, puzzleSize.y);
            m_ParentScale.localScale = new Vector3(size, size, size);

            float max = Math.Max(puzzleSize.x, puzzleSize.y);
            Vector3 place = m_Place.localPosition.SetX(-max / 2 + 0.5f);
            m_Place.localPosition = place;
        }
    }
}