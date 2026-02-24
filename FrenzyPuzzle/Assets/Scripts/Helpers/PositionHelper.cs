namespace PuzzleFrenzy.Scripts.Helpers
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface IPosition
    {
        Vector3 Position { get; }
    }
    
    public static class PositionHelper
    {
        public static int GetClosestViaId(this IList<IPosition> positions, Vector3 point, out float minDistance)
        {
            int result = 0;
            minDistance = Vector3.Distance(positions[0].Position, point);

            float distance = 0;
            for (int i = 1; i < positions.Count; i++)
            {
                distance = Vector3.Distance(positions[i].Position, point);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }

            return result;
        }
    }
}